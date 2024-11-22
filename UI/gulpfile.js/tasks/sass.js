const { src, dest } = require("gulp");

const gulpSass = require("gulp-sass")(require("sass"));
const concat = require("gulp-concat");
const cleanCSS = require("gulp-clean-css");

const sass = function (serverProjectPath, files_sass) {
  return function () {
    return src(files_sass)
    .pipe(gulpSass().on("error", gulpSass.logError))
    .pipe(concat("style.css"))
    .pipe(cleanCSS())
    .pipe(dest(serverProjectPath + "/css"));
  };
};

module.exports = { sass };