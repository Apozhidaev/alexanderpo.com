// base structure for knockout application
(function (__, win, $, ko, undefined) {
    "use strict";

    // ----- utils -----

    __.createApiObject = function (api) {
        api = api || {};
        var obj = Object.create(api, { api: { value: api } });
        if (__.debug) {
            if (api.__self) { obj.__self = api.__self; }
            api.__self = function() {
                return obj;
            };
        }
        return obj;
    };

    // ----- knockout bindings -----

    ko.bindingHandlers.render = {
        init: function () {
            return { controlsDescendantBindings: true };
        },
        update: function (element, valueAccessor) {
            __.app.render(element, ko.unwrap(valueAccessor()));
        }
    };

    // ----- enums -----

    __.Notices = {
        Success: 0,
        Error: 1,
        NotFound: 2,
        UpdateReady: 3,
        SubmitChanges: 4
    };

    // ----- base types -----

    var Router = (function () {

        // private  

        function canNavigate(route) {
            var self = this;

            return !self.frame.canNavigate || self.frame.canNavigate(route);
        }

        // public

        function go(segments) {
            var self = this;

            if (segments && segments.length && self.routeMap[segments[0]] && self.canNavigate(segments[0])) {

                self.routeMap[segments[0]](segments.slice(1));
            } else {
                __.app.notice(__.Notices.NotFound);
            }
        }

        function add(route, action) {
            var self = this;

            self.routeMap[route] = action;
            self.routes.push(route);
        }

        function first() {
            var self = this;

            for (var i = 0; i < self.routes.length; i++) {
                if (self.canNavigate(self.routes[i])) {
                    return self.routes[i];
                }
            }
            return null;
        }

        function contains(route) {
            var self = this;

            return !!self.routeMap[route];
        }

        // constructor

        function constructor(args) {

            var self = __.createApiObject();

            self.routes = [];
            self.routeMap = {};
            self.frame = args.frame;
            self.canNavigate = canNavigate;

            var api = self.api;
            api.go = go.bind(self);
            api.add = add.bind(self);
            api.first = first.bind(self);
            api.contains = contains.bind(self);

            return api;
        }

        return constructor;

    })();


    __.Page = (function () {

        // private

        // public

        function gotoRoot(args) {
            var self = this;

            args = args || {};
            __.app.navigate({
                url: args.urn ? self.root() + "/" + args.urn : self.root(),
                replace: args.replace
            });
        }

        function gotoUrl(args) {
            var self = this;

            args = args || {};
            __.app.navigate({
                url: args.urn ? self.url() + "/" + args.urn : self.url(),
                replace: args.replace
            });
        }

        // constructor

        function constructor(args) {

            var self = __.createApiObject(args.extend);

            self.frame = args.frame;
            self.hasParams = args.hasParams;
            self.ignoreFrameParams = args.ignoreFrameParams;

            var api = self.api;
            api.local = args.local;
            api.config = args.config;
            api.isViewLoading = ko.observable(false);
            api.gotoRoot = gotoRoot.bind(self);
            api.gotoUrl = gotoUrl.bind(self);

            api.isActive = ko.pureComputed(function () {
                return self.frame.content() == api;
            });

            if (self.hasParams) {
                api.params = ko.observable();
            }

            if (api.config.route && !self.frame.disableRouting) {
                self.frame.router.add(api.config.route, function (segments) {
                    var e = { handled: false };
                    if (self.hasParams) {
                        if (segments && segments.length && (!api.router || !api.router.contains(segments[0]))) {
                            e.params = segments[0];
                            segments = segments.slice(1);
                        } else {
                            e.params = undefined;
                        }
                    }
                    e.segments = segments;
                    e.navigate = function () {
                        if (self.hasParams) api.params(e.params);
                        if ((api.config.viewUrl || api.config.view)
                            && self.frame.content() != api) self.frame.content(api);
                        if (api.navigate) api.navigate(e.segments);
                    };
                    if (api.beforeNavigate) api.beforeNavigate(e);
                    if (!e.handled) e.navigate();
                });
            }

            if (api.config.route) {
                api.root = ko.pureComputed(function () {
                    if (self.ignoreFrameParams) {
                        if (self.frame.root) {
                            return self.frame.root() + "/" + api.config.route;
                        }
                    } else {
                        if (self.frame.url) {
                            return self.frame.url() + "/" + api.config.route;
                        }
                    }
                    return api.config.route;

                });
                api.url = ko.pureComputed(function () {
                    if (self.hasParams) {
                        return api.root() + "/" + api.params();
                    }
                    return api.root();
                });
            }

            return api;
        }

        return constructor;

    })();


    __.Frame = (function () {

        // public

        function navigate(segments) {
            var self = this;

            if (!segments || !segments.length) {
                if (self.enableDefaultRouting) {
                    var route = self.router.first();
                    if (route) {
                        win.location.href = self.url() + "/" + route;
                    }
                }
                return;
            }

            self.router.go(segments);
        }

        // constructor

        function constructor(args) {

            args = args || {};
            var self = __.createApiObject(args.extend);

            var api = self.api;
            api.config = args.config || api.config;
            api.content = args.contentObservable || ko.observable().extend({ rateLimit: 1 });
            api.isPageLoading = ko.pureComputed(function () {
                return api.content() && api.content().isViewLoading();
            });

            api.disableRouting = args.disableRouting;

            if (!api.disableRouting) {
                api.enableDefaultRouting = args.enableDefaultRouting;
                api.router = Router({ frame: api });
                api.navigate = navigate.bind(self);
            }

            return api;
        }

        return constructor;

    })();


    __.App = (function () {

        // private

        function loadView(config) {
            var self = this;

            var url = "";
            if (self.viewFolder) {
                url += self.viewFolder;
            }
            url += config.viewUrl;
            return $.ajax({ url: url, dataType: "html" })
                .done(function (view) {
                    config.view = view;
                });
        }

        function getView(model) {
            var self = this;

            if (model.config.view !== undefined) {
                var defrr = $.Deferred();
                defrr.resolve(model.config.view);
                return defrr.promise();
            }
            if (model.config.viewUrl !== undefined) {
                if (model.isViewLoading) model.isViewLoading(true);
                return self.loadView(model.config)
                    .always(function () {
                        if (model.isViewLoading) model.isViewLoading(false);
                    });
            }
            throw new win.Error("model.config.view is not found");
        }

        // public

        function render(element, model) {
            var self = this;

            var defrr = $.Deferred();
            if (!model) {
                ko.utils.setHtml(element, "");
                defrr.resolve();
            } else {
                win.setTimeout(function () {
                    self.getView(model)
                        .done(function (view) {
                            ko.utils.setHtml(element, view);
                            ko.applyBindingsToDescendants(model, element);
                            if (model.afterRender) {
                                win.setTimeout(function () { model.afterRender(); }, 0);
                            }
                        })
                        .fail(function () {
                            ko.utils.setHtml(element, "Network error.");
                        })
                        .always(function () {
                            defrr.resolve();
                        });
                }, 0);

            }
            return defrr.promise();
        }

        function navigate(args) {
            var self = this;

            if (args) {
                if (args.replace) {
                    win.location.replace(args.url);
                } else {
                    win.location.href = args.url;
                }

            } else {
                var url = win.location.hash;
                if (url) {
                    var segments = url.split("/");
                    self.model.navigate(segments.slice(1));
                } else {
                    self.model.navigate();
                }
            }
        }

        function run() {
            var self = this;
            
            var cache = win.applicationCache;
            if (cache) {
                var update = function () {
                    cache.swapCache();
                    if (!__.debug) {
                        __.app.notice(__.Notices.UpdateReady);
                    } else {
                        win.location.reload();
                    }

                };
                if (cache.status == 4) {
                    update();
                } else {
                    $(cache).bind('updateready', update);
                }
            }

            self.render(win.document.getElementById(self.appId), self.model)
                .done(function () {
                    $(win).bind("hashchange", function () {
                        self.navigate();
                    });
                    self.navigate();
                });
        }

        function notice(type, message) {
            var self = this;

            self.model.notice(type, message);
        }

        // constructor

        function constructor(args) {

            if (__.app) return __.app;

            var self = __.createApiObject(args.extend);

            self.appId = args.appId;
            self.getView = getView;
            self.loadView = loadView;
            self.viewFolder = args.viewFolder;

            var api = self.api;
            api.model = args.model || api.model;
            api.run = run.bind(self);
            api.navigate = navigate.bind(self);
            api.render = render.bind(self);
            api.notice = notice.bind(self);

            return __.app = api;
        }

        return constructor;

    })();

})(AlPo, window, jQuery, ko);
