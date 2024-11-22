import gulp from "gulp";
const { src, dest } = gulp;

import order from "gulp-order"; //npm i gulp-order --save-dev
import concat from "gulp-concat"; //npm i gulp-concat --save-dev
import babel from "gulp-babel"; //npm i --save-dev gulp-babel @babel/core @babel/preset-env
import uglify from "gulp-uglifyjs";

const js = function (backendPath, filesJs, filesJsOrder) {
  return function () {
    return src(filesJs)
      .pipe(order(filesJsOrder, { base: "./" }))
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
