import gulp from "gulp";
const { src, dest } = gulp;

import htmlReplace from "gulp-html-replace";
import htmlmin from "gulp-htmlmin";
import rename from "gulp-rename";

const html = function (backendPath, files_html) {
  return function () {
    return src(files_html)
      .pipe(
        htmlReplace({
          js: '<script src="js/app.js"></script>',
        })
      )
      .pipe(
        htmlmin({
          collapseWhitespace: true,
          minifyJS: true,
          minifyCSS: true,
          removeComments: true,
        })
      )
      .pipe(
        rename({
          dirname: "/",
          basename: "index",
          extname: ".html",
        })
      )
      .pipe(dest(backendPath));
  };
};

export default { html };
