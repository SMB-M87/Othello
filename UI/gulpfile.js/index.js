const { series, watch } = require("gulp");
const { serverProjectPath, files, name } = require("./config");

const js = require("./tasks/js").js(serverProjectPath, files.js, files.jsOrder);
js.displayName = "js";

const sass = require("./tasks/sass").sass(serverProjectPath, files.sass);
sass.displayName = "sass";

const html = require("./tasks/html").html(serverProjectPath, files.html);
html.displayName = "html";

const hello = function (done) {
  console.log(`Greeting from ${name}!`);
  done();
};

const watchFiles = () => {
  watch(files.sass, sass);
  watch(files.js, js);
  watch(files.html, html);
};

const _default = series(hello, html, sass, js);
module.exports = {
  default: _default,
  js,
  sass,
  html,
  watch: watchFiles,
};
