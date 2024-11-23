const Game = (function (config) {
  let configMap = { 
    apiUrl: config.apiUrl,
    apiKey: config.userToken,
    redirectUrl: config.redirectUrl
  };

  // private functions
  const _init = function () {
    _template();
    const feedbackWidget = FeedbackSingleton.getInstance();
    feedbackWidget.removeLog();
    Game.Data.init(configMap.apiUrl, configMap.apiKey, "production");
    Game.Model.init(configMap.redirectUrl);
  };

  const _template = function () {
    const html = spa_templates["body"]();
    document.getElementById("body").innerHTML = html;
  };

  const _getCurrentGameState = function () {
    setInterval(function () {
      Game.Model.getGameState();
    }, 1000);
  };

  // public functions
  const init = (callback) => {
    _init();
    _getCurrentGameState();
    if (callback) callback();
  };

  // return object
  return {
    init: init,
  };
})({ apiUrl:"https://localhost:7023/api/game/", userToken: "test", redirectUrl: "https://localhost:7069/Home/Result/" });

$(() => {
  function afterInit() {}
  if (spa_templates["body"]) {
    Game.init(afterInit);
  } else {
    console.error("Template 'body' is not loaded yet.");
  }
});
