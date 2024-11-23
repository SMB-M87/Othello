import gulp from "gulp";
const { src, dest } = gulp;

import concat from "gulp-concat";

const vendor = function (backendPath, files) {
  return function () {
    return src(files)
    .pipe(concat("vendor.js"))
    .pipe(dest(backendPath + "/js"));
  };
};

export default { vendor };