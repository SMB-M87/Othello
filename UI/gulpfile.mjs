import gulp from "gulp";
const { series, watch } = gulp;

import { serverProjectPath, files } from "./config.js";

import jsTask from "./tasks/js.js";
import sassTask from "./tasks/sass.js";
import htmlTask from "./tasks/html.js";

const js = jsTask.js(serverProjectPath, files.js, files.jsOrder);
js.displayName = "js";

const sass = sassTask.sass(serverProjectPath, files.sass);
sass.displayName = "sass";

const html = htmlTask.html(serverProjectPath, files.html);
html.displayName = "html";

const watchFiles = () => {
  watch(files.sass, sass);
  watch(files.js, js);
  watch(files.html, html);
};

export default series(html, sass, js);

export const build = series(html, sass, js);
export const sucks = watchFiles;
