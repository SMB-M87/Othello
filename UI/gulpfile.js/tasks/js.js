const { src, dest } = require("gulp");

const order = require("gulp-order"); //npm i gulp-order --save-dev
const concat = require("gulp-concat"); //npm i gulp-concat --save-dev
const babel = require("gulp-babel"); //npm i --save-dev gulp-babel @babel/core @babel/preset-env

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
      .pipe(dest(backendPath + "/js"));
  };
};

module.exports = { js }