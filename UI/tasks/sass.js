import gulp from "gulp";
const { src, dest } = gulp;

import * as sass from "sass";
import gulpSass from "gulp-sass";
const compileSass = gulpSass(sass);

import concat from "gulp-concat";
import sourcemaps from "gulp-sourcemaps";
import cleanCSS from "gulp-clean-css";
import autoprefixer from "gulp-autoprefixer";
import rename from "gulp-rename";

const compileStyles  = function (backendPath, files) {
  return function () {
    return src(files)
      .pipe(sourcemaps.init())
      .pipe(compileSass().on("error", compileSass.logError))
      .pipe(concat("style.css"))
      .pipe(autoprefixer())
      .pipe(cleanCSS())
      .pipe(rename("style.min.css"))
      .pipe(sourcemaps.write("."))
      .pipe(dest(backendPath + "/css"));
  };
};

export default { sass: compileStyles };
