Game.Data = (function () {
  let configMap = {
    apiUrl: "",
    apiKey: "",
    mock: [
      {
        url: "api/game/turn/<token>",
        data: 0,
      },
    ],
  };

  let stateMap = {
    environment: "development"
  };

  // private functions
  const _init = function () {
    console.log(
      "Data module started from url: " + configMap.apiUrl + configMap.apiKey
    );
  };

  const _setMockData = function (mockData) {
    configMap.mock = mockData;
  };

  const _getMockData = function () {
    const mockData = configMap.mock;

    return new Promise((resolve, reject) => {
      resolve(mockData);
    });
  };

  const _get = function () {
    if (stateMap.environment == "production") {
      return $.get(configMap.apiUrl + "/" + configMap.apiKey)
        .then((result) => {
          return result;
        })
        .catch((e) => {
          console.log(e.message);
        });
    } else {
      return _getMockData();
    }
  };

  // public functions
  const init = (url, environment) => {
    if (environment == "development" || environment == "production") {
      configMap.apiUrl = url + "data";
      stateMap.environment = environment;
      _init();
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  // return object
  return {
    init: init,
    get: _get,
    setMockData: _setMockData
  };
})();