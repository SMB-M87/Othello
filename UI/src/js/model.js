Game.Model = (function () {
  // config
  let configMap = {
    apiUrl: "",
  };

  // private functions
  const _init = function () {
    console.log("Model module started from url: " + configMap.apiUrl);
  };

  const _getGameState = function () {
    let result = Game.Data.get()
      .then((data) => {
        if (data[0].data === 0) {
          console.log("It's nobody's turn apparently.");
        } else if (data[0].data === 1) {
          console.log("White's turn.");
        } else if (data[0].data === 2) {
          console.log("Black's turn.");
        } else {
          throw new Error("Unknown game state: " + data[0].data + ".");
        }
        return data[0];
      })
      .catch((e) => {
        console.log(e.message);
        return { data: null, error: e.message };
      });

    return result;
  };

  // public functions
  const init = (url) => {
    configMap.apiUrl = url + "model";
    _init();
  };

  // return object
  return {
    init: init,
    getGameState: _getGameState,
  };
})();
