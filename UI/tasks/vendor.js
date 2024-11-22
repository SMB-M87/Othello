import gulp from "gulp";
const { src, dest } = gulp;

import concat from "gulp-concat";

const vendor = function (backendPath, filesJs) {
  return function () {
    return src(filesJs)
    .pipe(concat("vendor.js"))
    .pipe(dest(backendPath + "/js"));
  };
};

export default { vendor };