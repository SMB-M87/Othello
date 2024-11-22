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
  sass: ["src/css/*.scss"],
  html: ["src/index.html"],
  vendor: ["src/vendor/*.js"]
};

export { serverProjectPath, files };
