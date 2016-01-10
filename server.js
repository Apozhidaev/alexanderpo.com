
var express = require('express')
  , http = require('http')
  , path = require('path')
  , fs = require('fs');

var app = express();

app.set('port', process.env.PORT || 3020);
app.use(express.static(path.join(__dirname, 'src')));


app.get('/',  function(req, res){
  res.redirect('index.html');
});
app.get('/cache.appcache', function(req, res){

  fs.readFile(path.join(__dirname, '../cache.appcache'), 'utf8', function (err, data) {
    if (err) {
      return console.log(err);
    }
    res.contentType('text/cache-manifest');
    res.send(200, data)
  });

});

http.createServer(app).listen(app.get('port'), function(){
  console.log("Express server listening on port " + app.get('port'));
});
