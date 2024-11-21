const Game = (function (url, key) {
  // config
  let configMap = {
    apiUrl: url,
    apiKey: key,
  };

  // private functions
  const _init = function () {
    const feedbackWidget = FeedbackSingleton.getInstance();
    feedbackWidget.removeLog();
    Game.Data.init(configMap.apiUrl, configMap.apiKey, "production");
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
})("https://localhost:7023/api/game/", "test");

$(() => {
  function afterInit() {}

  Game.init(afterInit);
});
