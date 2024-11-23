import gulp from "gulp";
const { series, watch } = gulp;

import { serverProjectPath, files } from "./config.js";

import jsTask from "./tasks/js.js";
import sassTask from "./tasks/sass.js";
import htmlTask from "./tasks/html.js";
import vendorTask from "./tasks/vendor.js";
import templateTask from "./tasks/template.js";

const js = jsTask.js(serverProjectPath, files.js, files.jsOrder);
js.displayName = "js";

const sass = sassTask.sass(serverProjectPath, files.sass);
sass.displayName = "sass";

const html = htmlTask.html(serverProjectPath, files.html);
html.displayName = "html";

const vendor = vendorTask.vendor(serverProjectPath, files.vendor);
vendor.displayName = "vendor";

const template = templateTask.template(serverProjectPath, files.template, files.partial);
template.displayName = "template";

const watchFiles = () => {
  watch(files.sassFiles, sass);
  watch(files.js, js);
  watch(files.html, html);
  watch(files.vendor, vendor);
  watch([...files.template, ...files.partial], template);
};

export default series(html, sass, js, vendor, template);

export const build = series(html, sass, js, vendor, template);
export const sucks = watchFiles;
