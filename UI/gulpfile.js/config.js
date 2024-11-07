const serverProjectPath = "../MVC/MVC/wwwroot";

const files = {
  jsOrder: [
    "js/feedbackWidget.js",
    "js/game.js",
    "js/data.js",
    "js/model.js",
    "js/othello.js",
  ],
  js: ["src/js/*.js"],
  css: ["src/css/*.scss"],
  html: ["src/index.html"],
};
const name = "SMB-M87";

module.exports = { serverProjectPath, files, name };