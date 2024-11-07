const { src, dest } = require("gulp");
const htmlReplace = require("gulp-html-replace");

const html = function (serverProjectPath, files_html) {
  return function () {
    return src(files_html)
      .pipe(
        htmlReplace({
          js: '<script src="js/app.js"></script>',
        })
      )
      .pipe(dest(serverProjectPath));
  };
};

module.exports = { html };
