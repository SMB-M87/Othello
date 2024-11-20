Game.Model = (function () {
  // config
  let configMap = {
    apiUrl: "",
  };

  let stateMap = {
    firstLoad: true
  }

  // private functions
  const _init = function () {
    console.log("Model module started from url: " + configMap.apiUrl);
  };

  const _getGameState = function () {
    let dataPromise;
    if (stateMap.firstLoad) {
      dataPromise = Game.Data.get();
      stateMap.firstLoad = false;
      console.log("Received data from view API:");
    } else {
      dataPromise = Game.Data.getPartial();
      console.log("Received data from partial API:");
    }

    return dataPromise
      .then((data) => {
        console.log(data);
        return data;
      })
      .catch((e) => {
        console.log(e.message);
        return { data: null, error: e.message };
      });
  };

  // public functions
  const init = (url) => {
    configMap.apiUrl = url;
    _init();
  };

  // return object
  return {
    init: init,
    getGameState: _getGameState,
  };
})();
