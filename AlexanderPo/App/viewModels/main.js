//
AlPo.ViewModels.MainModel = (function (__, win, $, ko) {
    "use strict";

    // using

    var ViewModels = __.ViewModels;
    var Main = __.ViewModels.Main;

    // static

    // private

    // public

    function navigate(segments) {
        var self = this;
        
        var loading = ViewModels.LoadingModel({
            frame: self.api,
            local: self.local.pages.loading,
            config: self.config.pages.loading
        });
        self.content(loading);
    }

    function notice(type, message) {
        var self = this;

        self.notices.push(Main.NoticeItemModel({
            mainModel: self.api,
            message: message,
            type: type
        }));
    }
    
    function clear() {
        var self = this;
        
    }
   

    // constructor

    function constructor() {

        var self = __.createApiObject(__.Frame({
            config: __.config.main,
            disableRouting: true
        }));

        var api = self.api;
        api.navigate = navigate.bind(self);
        api.notice = notice.bind(self);
        api.clear = clear.bind(self);
        api.notices = ko.observableArray();
        api.local = __.app.local.main;


        return api;
    }

    return constructor;

})(AlPo, window, jQuery, ko);
