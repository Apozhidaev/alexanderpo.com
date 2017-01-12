var gulp = require("gulp");
var insert = require('gulp-insert');
var handlebars = require('handlebars');
var rimraf = require("rimraf");
var fs = require('fs');

var paths = {
    build: './build/',
    src: './src/'
};

var titles = {
    "links.html": "links – alexander .pozhidaev"
};

gulp.task('clean', function (cb) {
  rimraf(paths.build, cb);
});

gulp.task('copy', ['clean'], function () {

    return gulp.src(['!' + paths.src + '**/*.{html,hbs}',
            paths.src + '**/*.*'])
        .pipe(gulp.dest(paths.build));

});

gulp.task('build', ['copy'], function () {

    var layout = fs.readFileSync(paths.src + 'layout.hbs', 'utf8');
    var template = handlebars.compile(layout);

    return gulp.src(paths.src + '*.html')
        .pipe(insert.transform(function(contents, file) {
            var context = {title: titles[file.relative] || 'alexander .pozhidaev'};
            context.body = handlebars.compile(contents)(context);
            return template(context);
        }))
        .pipe(gulp.dest(paths.build));


});
