const serverProjectPath = "../MVC/MVC/wwwroot";

const files = {
  jsOrder: [
    "src/js/feedbackSingleton.js",
    "src/js/feedbackWidget.js",
    "src/js/game.js",
    "src/js/data.js",
    "src/js/model.js",
    "src/js/othello.js",
  ],
  js: ["src/js/*.js"],
  css: ["src/css/*.scss"],
  html: ["src/index.html"],
};
const name = "SMB-M87";

module.exports = { serverProjectPath, files, name };