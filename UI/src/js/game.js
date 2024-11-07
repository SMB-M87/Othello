const Game = (function (url) {
  // config
  let configMap = {
    apiUrl: url,
  };

  let stateMap = {
    gameState: "",
  };

  // private functions
  const _init = function () {
    console.log("Game module started from url: " + configMap.apiUrl);
  };

  const _getCurrentGameState = function () {
    setInterval(function () {
      let result = Game.Model.getGameState()
        .then((data) => {
          stateMap.gameState = data.data;
          return data;
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
    Game.Data.init(configMap.apiUrl, "development");
    Game.Model.init(configMap.apiUrl);
    Game.Othello.init(configMap.apiUrl);
    callback();
  };

  // return object
  return {
    init: init,
  };
})("https://www.s1164087/api/game/");

$(() => {
  function afterInit() {
    console.log("Game init completed.");
  }

  Game.init(afterInit);
});
