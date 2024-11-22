const { src, dest } = require("gulp");

const gulpSass = require("gulp-sass")(require("sass"));
const concat = require("gulp-concat");
const sourcemaps = require("gulp-sourcemaps");
const cleanCSS = require("gulp-clean-css");
const autoprefixer = require("gulp-autoprefixer");

const sass = function (serverProjectPath, files_sass) {
  return function () {
    return src("src/css/main.scss")
      .pipe(sourcemaps.init())
      .pipe(gulpSass().on("error", gulpSass.logError))
      .pipe(concat("style.css"))
      .pipe(autoprefixer())
      .pipe(cleanCSS())
      .pipe(sourcemaps.write("."))
      .pipe(dest(serverProjectPath + "/css"));
  };
};

module.exports = { sass };
