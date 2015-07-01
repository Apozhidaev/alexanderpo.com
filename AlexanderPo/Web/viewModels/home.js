//
__.ViewModels.HomeModel = (function (__, win, $, ko) {
    "use strict";

    // using

    // static

    // private

    // public
    
    function signOut() {
        var self = this;

        __.app.signOut();
    }

    // constructor

    function constructor(args) {

        var self = __.createApiObject(__.Page(args));

        var api = self.api;
        api.loadingFailed = ko.observable(false);
        api.signOut = signOut.bind(self);
        return self.api;
    }

    return constructor;

})(__, window, jQuery, ko);
