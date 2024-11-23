import gulp from "gulp";
const { src, dest } = gulp;

import order from "gulp-order";
import concat from "gulp-concat";
import babel from "gulp-babel";
import uglify from "gulp-uglifyjs";

const js = function (backendPath, files, filesOrder) {
  return function () {
    return src(files)
      .pipe(order(filesOrder, { base: "./" }))
      .pipe(concat("app.js"))
      .pipe(
        babel({
          presets: ["@babel/preset-env"],
        })
      )
      .pipe(uglify({ compress: true }))
      .pipe(dest(backendPath + "/js"));
  };
};

export default { js };
