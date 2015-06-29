//
AlPo.LocalStrings.Rus = (function (__) {
    "use strict";

    // constructor

    function constructor(args) {

        var self = __.createApiObject();

        var api = self.api;

        api.main = {
            success: 'Все хорошо',
            error: 'Ошибка...',
            notFound: 'Не найдено',
            updateReady: 'Новые изменения доступны (требуется обновить страницу)',
            submitChanges: 'Изменения были сохранены',
            pages:
            {
                loading: {
                    loading: "Александр Пожидаев"
                }
            }
        };

        return api;
    }

    return constructor;

})(AlPo);