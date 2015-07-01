//
__.LocalStrings.Eng = (function (__) {
    "use strict";

    // constructor

    function constructor(args) {

        var self = __.createApiObject();

        var api = self.api;
       

        api.main = {
            success: 'Well done',
            error: 'Sorry...',
            notFound: 'Not found',
            updateReady: 'New updates are available (please refresh the page)',
            submitChanges: 'Changes have been done',
            pages:
            {
                home: {
                    firstName: "Alexander",
                    secondName: "Pozhidaev"
                }
            }
        };

        return api;
    }

    return constructor;

})(__);