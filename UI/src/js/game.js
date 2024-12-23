const Game = (function (config) {
  if (!config || !config.apiUrl || !config.userToken) {
    throw new Error(
      "Game module initialization failed: Missing config properties."
    );
  }

  let configMap = {
    apiUrl: config.apiUrl,
    apiToken: config.userToken
  };

  // private functions
  const _init = function () {
    Game.Handlebar.renderBody();
    Game.Handlebar.renderBoard(null, null, null);
    Game.Handlebar.attachEventListeners();
    Game.Data.init(configMap.apiUrl, configMap.apiToken, "production");
    Game.Api.init();
    Game.Stat.init("stats-chart");
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
})(config);

$(() => {
  function afterInit() {}
  Game.init(afterInit);
});
