Game.Data = (function () {
  let configMap = {
    apiUrl: "",
    apiKey: "",
    mock: [
      {
        url: "api",
        key: "key",
        data: 0,
      },
    ],
  };

  let stateMap = {
    environment: "",
  };

  // private functions
  const _init = function (url, key, environment) {
    if (environment == "production") {
      configMap.apiUrl = url;
      configMap.apiKey = key;
      stateMap.environment = environment;
    } else if (environment == "development") {
      configMap.mock.url = url + "data";
      configMap.mock.key = key;
      stateMap.environment = environment;
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  const _get = function () {
    if (stateMap.environment == "production") {
      return $.get(configMap.apiUrl + "game/view/" + configMap.apiKey)
        .then((result) => {
          return result;
        })
        .catch((e) => {
          console.log(e.message);
        });
    } else if (stateMap.environment == "development") {
      return _getMockData();
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  const _getPartial = function () {
    if (stateMap.environment == "production") {
      return $.get(configMap.apiUrl + "game/partial/" + configMap.apiKey)
        .then((result) => {
          return result;
        })
        .catch((e) => {
          console.log(e.message);
        });
    } else if (stateMap.environment == "development") {
      return _getMockData();
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  const _sendMove = function (row, col) {
    const token = configMap.apiKey;

    const moveData = {
      playerToken: token,
      row: row,
      column: col,
    };

    return $.ajax({
      url: configMap.apiUrl + "game/move",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify(moveData),
    }).then((response) => {
      Game.Model.getGameState();
    });
  };

  const _passGame = function () {
    const token = configMap.apiKey;

    return $.ajax({
      url: configMap.apiUrl + "game/pass",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ token: token }),
    }).then((response) => {
      Game.Model.getGameState();
    });
  };

  const _forfeitGame = function () {
    const token = configMap.apiKey;

    return $.ajax({
      url: configMap.apiUrl + "game/forfeit",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ token: token }),
    }).then((response) => {
      Game.Model.getGameState();
    });
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

  // public functions
  const init = (url, key, environment) => {
    _init(url, key, environment);
  };

  const get = () => {
    return _get();
  };

  const getPartial = () => {
    return _getPartial();
  };

  const sendMove = function (row, col) {
    return _sendMove(row, col);
  };

  const passGame = function () {
    return _passGame();
  };

  const forfeitGame = function () {
    return _forfeitGame();
  };

  const setMockData = (mockData) => {
    _setMockData(mockData);
  };

  // return object
  return {
    init: init,
    get: get,
    getPartial: getPartial,
    sendMove: sendMove,
    passGame: passGame,
    forfeitGame: forfeitGame,
    setMockData: setMockData,
  };
})();
