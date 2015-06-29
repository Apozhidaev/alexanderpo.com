//
AlPo.Config = (function (__) {
    "use strict";

    // constructor

    function constructor(args) {

        if (__.config) return __.config;

        var self = __.createApiObject();

        var api = self.api;

        api.appId = "body";
        api.viewFolder = "views/";

        api.main = {
            viewUrl: "main.html",
            pages:
            {
                loading: {
                    viewUrl: "loading.html"
                }
            }
        };

        return __.config = api;
    }

    return constructor;

})(AlPo);