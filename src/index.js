import './assets/site.less';
import 'bootstrap-webpack!../bootstrap.config.less!../bootstrap.config.js';

function addEventListener(node, event, listener) {
    if (node.addEventListener) {
        node.addEventListener(event, listener, false);
    } else {
        node.attachEvent('on' + event, listener);
    }
}

var cache = window.applicationCache;
if (cache) {
    var update = function () {
        window.location.reload();
    };
    if (cache.status == 4) {
        update();
    } else {
        addEventListener(cache, 'updateready', update);
    }
}