
var path = require('path');
var fs = require('fs')

exports.index = function(req, res){
  res.redirect('index.html');
};

exports.appcache = function(req, res){

  fs.readFile(path.join(__dirname, '../cache.appcache'), 'utf8', function (err, data) {
    if (err) {
      return console.log(err);
    }
    res.contentType('text/cache-manifest');
    res.send(200, data)
  });

};