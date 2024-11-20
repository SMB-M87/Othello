const Game = (function (url, key) {
  // config
  let configMap = {
    apiUrl: url,
    apiKey: key
  };

  let stateMap = {
    gameState: "",
  };

  // private functions
  const _init = function () {
    console.log("Game module started from url: " + configMap.apiUrl);
    Game.Data.init(configMap.apiUrl, configMap.apiKey);
    Game.Model.init(configMap.apiUrl);
    Game.Othello.init(configMap.apiUrl);
  };

  const _getCurrentGameState = function () {
    setInterval(function () {
      let result = Game.Model.getGameState()
        .then((data) => {
          stateMap.gameState = data;
        })
        .catch((e) => {
          console.log(e.message);
        });
    }, 2000);
  };

  // public functions
  const init = (callback) => {
    _init();
    _getCurrentGameState();
    if(callback) callback();
  };

  // return object
  return {
    init: init,
  };
})("https://localhost:7023/api/game/", "salie");

$(() => {
  function afterInit() {
    console.log("Game init completed.");
  }

  Game.init(afterInit);
});
