//
AlPo.LocalStrings.Eng = (function (__) {
    "use strict";

    // constructor

    function constructor(args) {

        var self = __.createApiObject();

        var api = self.api;
       

        api.main = {
            success: 'Well done',
            error: 'Sorry...',
            notFound: 'Not found',
            updateReady: 'New changes available (requires a page refresh)',
            submitChanges: 'Changes have been done',
            pages:
            {
                loading: {
                    loading: "Alexander Pozhidaev"
                }
            }
        };

        return api;
    }

    return constructor;

})(AlPo);