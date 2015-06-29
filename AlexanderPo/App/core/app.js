//
AlPo.Application = (function (__, win, $) {
    "use strict";

    // using

    var LocalStrings = __.LocalStrings;
    var ViewModels = __.ViewModels;

    // static
    
    function getLocale() {
        if (win.navigator) {
            if (win.navigator.language) {
                return win.navigator.language;
            }
            else if (win.navigator.browserLanguage) {
                return win.navigator.browserLanguage;
            }
            else if (win.navigator.systemLanguage) {
                return win.navigator.systemLanguage;
            }
            else if (win.navigator.userLanguage) {
                return win.navigator.userLanguage;
            }
        }
    }
    

    function isMobile() {

        return win.isMobile.phone;
    }

    // private

    // public

    function showSuccess(message) {
        var self = this;
        var api = this.api;

        api.notice(__.Notices.Success, message);
    }

    function showError(message) {
        var self = this;
        var api = this.api;

        api.notice(__.Notices.Error, message);
    }

    // constructor

    function constructor(args) {

        if (__.app) return __.app;

        __.Config();

        var self = __.createApiObject(__.App({
            appId: __.config.appId,
            viewFolder: __.config.viewFolder
        }));

        var api = self.api;

        api.isMobile = isMobile;

        if (/ru/ig.test(getLocale())) {
            api.local = LocalStrings.Rus();
            api.lang = __.Lang.Rus;
        } else {
            api.local = LocalStrings.Eng();
            api.lang = __.Lang.Eng;
        }
        
        api.model = ViewModels.MainModel();
        api.showSuccess = showSuccess.bind(self);
        api.showError = showError.bind(self);

        return api;
    }

    return constructor;

})(AlPo, window, jQuery);