// for EcmaScript v3
if (typeof Object.create != 'function') {
    Object.create = (function () {
        var Temp = function () { };
        return function (prototype, properties) {
            if (typeof prototype != 'object') {
                throw TypeError('Argument must be an object');
            }
            Temp.prototype = prototype;
            var result = new Temp();
            Temp.prototype = null;
            if (properties) {
                for (var prop in properties) {
                    if (properties[prop].value !== undefined) {
                        result[prop] = properties[prop].value;
                    }
                }
            }
            return result;
        };
    })();
}