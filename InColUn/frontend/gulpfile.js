'use strict';
var gulp = require('gulp');
var mocha = require('gulp-mocha');
var del = require('del');
var ts = require("gulp-typescript");
var sourcemaps = require('gulp-sourcemaps');
var webpack = require('webpack');
var less = require('gulp-less');
var gutil = require('gulp-util');

gulp.task('build styles - boards', function () {
  return gulp.src('./styles/boards.less').pipe(less()).pipe(gulp.dest('../InColUn/wwwroot/css'));
});

gulp.task('build styles - login', function () {
  return gulp.src('./styles/login.less').pipe(less()).pipe(gulp.dest('../InColUn/wwwroot/css/'));
});

gulp.task('build styles', ['build styles - boards', 'build styles - login']);

gulp.task('clean', function (cb) {
    var typeScriptGenFiles = [
                                '../../out/frontend/**/*.js',   
                                '../../out/frontend/**/*.js.map'
                            ];
    del(typeScriptGenFiles, cb).then(paths => {console.log('Deleted files and folders:\n', paths.join('\n'))});
});

gulp.task("build", function () {
	var tsProject = ts.createProject('src/tsconfig.json');
    var tsResult = tsProject.src()
        .pipe(sourcemaps.init())
        .pipe(tsProject());

	return tsResult.js
		.pipe(sourcemaps.write('.'))
		.pipe(gulp.dest('../out/frontend'));
});

gulp.task('run test', function () {
    return gulp.src(['build/frontend/tests/**/*.spec.js'], { read: false })
        .pipe(mocha({ reporter: 'spec' }))
        .on('error', gutil.log);
});

gulp.task('copy UI libs', function(){
return gulp.src(['node_modules/jquery/dist/**/*', 'node_modules/medium-editor/dist/**/*'], {
            base: 'node_modules'
        }).pipe(gulp.dest('../backend/src/incolun/wwwroot/lib'));
});