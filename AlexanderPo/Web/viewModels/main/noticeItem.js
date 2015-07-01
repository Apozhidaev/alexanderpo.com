//
__.ViewModels.Main.NoticeItemModel = (function (__, win, $, ko) {
    "use strict";

    // using

    // static

    // private

    // public

    function close() {
        var self = this;

        self.mainModel.notices.remove(self.api);
        win.clearTimeout(self.infoTimer);
    }

    // constructor

    function constructor(args) {

        var self = __.createApiObject();

        self.mainModel = args.mainModel;

        var local = args.mainModel.local;

        var api = self.api;
        api.close = close.bind(self);

        switch (args.type) {
            case __.Notices.Success:
                api.message = args.message || local.success;
                api.status = "alert-success";
                break;
            case __.Notices.Error:
                api.message = args.message || local.error;
                api.status = "alert-error";
                break;
            case __.Notices.NotFound:
                api.message = args.message || local.notFound;
                api.status = "alert-error";
                break;
            case __.Notices.UpdateReady:
                api.message = args.message || local.updateReady;
                api.status = "alert-warning";
                break;
            case __.Notices.SubmitChanges:
                api.message = args.message || local.submitChanges;
                api.status = "alert-success";
                break;
        }

        self.infoTimer = win.setTimeout(api.close, 5000);

        return api;
    }

    return constructor;

})(__, window, jQuery, ko);
