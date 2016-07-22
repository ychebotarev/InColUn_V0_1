'use strict';
var gulp = require('gulp');
var mocha = require('gulp-mocha');
var del = require('del');
var ts = require("gulp-typescript");
var sourcemaps = require('gulp-sourcemaps');
var webpack = require('webpack');
var less = require('gulp-less');
var minifyCSS = require('gulp-clean-css');
var gutil = require('gulp-util');

gulp.task('build styles - main', function () {
  return gulp.src('./styles/main.less')
    .pipe(less())
    //.pipe(minifyCSS())
    .pipe(gulp.dest('./wwwroot/app'));
});

gulp.task('build styles - login', function () {
  return gulp.src('./styles/login.less')
    .pipe(less())
    //.pipe(minifyCSS())
    .pipe(gulp.dest('./wwwroot/app'));
});

gulp.task('clean-ts', function (cb) {
    var typeScriptGenFiles = [
                                './build/**/*.js',   
                                './build/**/*.js.map',   
                                './wwwroot/app/build/*.js',
                                './wwwroot/app/build/*.js.map',
                                './test/**/*.js',
                                './test/**/*.js.map'
                            ];
    del(typeScriptGenFiles, cb).then(paths => {console.log('Deleted files and folders:\n', paths.join('\n'))});
});

var tsconfig_FE = ts.createProject('src/tsconfig.json',
    { typescript: require('typescript') });
gulp.task("build frontend code", function () {
    var tsResult = tsconfig_FE.src()
        .pipe(sourcemaps.init())
        .pipe(ts(tsconfig_FE));
    return tsResult.pipe(sourcemaps.write('.')).pipe(gulp.dest('./build/frontend'));
});

gulp.task('run test - frontend', function () {
    return gulp.src(['build/frontend/tests/**/*.spec.js'], { read: false })
        .pipe(mocha({ reporter: 'spec' }))
        .on('error', gutil.log);
});