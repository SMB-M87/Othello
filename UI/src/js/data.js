Game.Data = (function () {
  let configMap = {
    apiUrl: "",
    apiKey: "",
    mock: [
      {
        url: "api/game/view/<token>",
        key: "key",
        data: 0,
      },
    ],
  };

  let stateMap = {
    environment: "production"
  };

  // private functions
  const _init = function () {
    console.log(
      "Data module started from url: " + configMap.apiUrl
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
      return $.get(configMap.apiUrl + "view/" + configMap.apiKey)
        .then((result) => {
          return result;
        })
        .catch((e) => {
          console.log(e.message);
        });
    } else if (stateMap.environment == "development"){
      return _getMockData();
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  const _getPartial = function () {
    if (stateMap.environment == "production") {
      return $.get(configMap.apiUrl + "partial/" + configMap.apiKey)
        .then((result) => {
          return result;
        })
        .catch((e) => {
          console.log(e.message);
        });
    } else if (stateMap.environment == "development"){
      return _getMockData();
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  // public functions
  const init = (url, key) => {
    if (stateMap.environment == "production") {
      configMap.apiUrl = url ;
      configMap.apiKey = key;
      _init();
    } else if (stateMap.environment == "development") {
      configMap.mock.url = url + "view/";
      configMap.mock.key = key;
      _init();
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  // return object
  return {
    init: init,
    get: _get,
    getPartial: _getPartial,
    setMockData: _setMockData
  };
})();