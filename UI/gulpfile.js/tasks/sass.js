const { src, dest } = require("gulp");

const gulpSass = require("gulp-sass")(require("sass"));

const sass = function (serverProjectPath, files_sass) {
  return function () {
    return src(files_sass)
      .pipe(gulpSass().on("error", gulpSass.logError))
      .pipe(dest(serverProjectPath + "/css"));
  };
};

module.exports = { sass };