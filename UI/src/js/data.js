Game.Data = (function () {
  let configMap = {
    apiUrl: null,
    apiToken: null,
    mock: [
      {
        url: null,
        key: null,
      },
    ],
  };

  let stateMap = {
    environment: "",
  };

  // private functions
  const _init = function (url, token, environment) {
    if (environment == "production") {
      configMap.apiUrl = url;
      configMap.apiToken = token;
      stateMap.environment = environment;
    } else if (environment == "development") {
      configMap.mock.url = url;
      configMap.mock.key = token;
      stateMap.environment = environment;
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  const _get = function () {
    if (stateMap.environment == "production") {
      return $.ajax({
        url: configMap.apiUrl + "game/view/" + configMap.apiToken,
        method: "GET",
        crossDomain: true,
        xhrFields: {
          withCredentials: true,
        },
      })
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
      return $.ajax({
        url: configMap.apiUrl + "game/partial/" + configMap.apiToken,
        method: "GET",
        crossDomain: true,
        xhrFields: {
          withCredentials: true,
        },
      })
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

  const _getResult = function (delay) {
    if (stateMap.environment == "production") {
      return new Promise((resolve, reject) => {
        setTimeout(() => {
          $.ajax({
            url: configMap.apiUrl + "result/last/" + configMap.apiToken,
            method: "GET",
            crossDomain: true,
            xhrFields: {
              withCredentials: true,
            },
          })
            .then((result) => resolve(result))
            .catch((e) => {
              console.log(e.message);
              reject(e);
            });
        }, delay);
      });
    } else if (stateMap.environment == "development") {
      return _getMockData();
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  const _getRematch = function () {
    const username = Game.Model.getOpponent();

    const config = `${username} ${configMap.apiToken}`;

    const encoded = encodeURIComponent(config);

    return $.ajax({
      url: `${configMap.apiUrl}player/rematch/${encoded}`,
      method: "GET",
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
    })
      .then((jqXHR) => {
        if (jqXHR.status === 200) {
          return true;
        } else {
          return false;
        }
      })
      .catch((error) => {
        return false;
      });
  };

  const _sendMove = function (row, col) {
    const token = configMap.apiToken;

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
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
    }).then((response) => {
      Game.Model.getGameState();
    });
  };

  const _passGame = function () {
    const token = configMap.apiToken;

    return $.ajax({
      url: configMap.apiUrl + "game/pass",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ token: token }),
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
    }).then((response) => {
      Game.Model.getGameState();
    });
  };

  const _forfeitGame = function () {
    const token = configMap.apiToken;

    return $.ajax({
      url: configMap.apiUrl + "game/forfeit",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ token: token }),
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
    }).then((response) => {
      Game.Model.getGameState();
    });
  };

  const _rematchGame = function () {
    const username = Game.Model.getOpponent();

    const config = {
      PlayerToken: configMap.apiToken,
      Description: `Rematch against ${username}`,
      Rematch: username,
    };

    return $.ajax({
      url: configMap.apiUrl + "game/create",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify(config),
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
    });
  };

  const _acceptGame = function () {
    const username = Game.Model.getOpponent();

    const config = {
      ReceiverUsername: username,
      SenderToken: configMap.apiToken,
    };

    return $.ajax({
      url: configMap.apiUrl + "player/request/game/accept",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify(config),
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
    }).then((response) => {});
  };

  const _declineGame = function () {
    const username = Game.Model.getOpponent();

    const config = {
      ReceiverUsername: username,
      SenderToken: configMap.apiToken,
    };

    return $.ajax({
      url: configMap.apiUrl + "player/request/game/decline",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify(config),
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
    }).then((response) => {});
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
  const init = (url, token, environment) => {
    _init(url, token, environment);
  };

  const get = () => {
    try {
      return _get();
    } catch (e) {
      return null;
    }
  };

  const getPartial = () => {
    try {
      return _getPartial();
    } catch (e) {
      return null;
    }
  };

  const getResult = (delay = 50) => {
    try {
      return _getResult(delay);
    } catch (e) {
      return null;
    }
  };

  const getRematch = () => {
    try {
      return _getRematch();
    } catch (e) {
      return null;
    }
  };

  const sendMove = function (row, col) {
    try {
      return _sendMove(row, col);
    } catch (e) {
      return null;
    }
  };

  const passGame = function () {
    try {
      return _passGame();
    } catch (e) {
      return null;
    }
  };

  const forfeitGame = function () {
    try {
      return _forfeitGame();
    } catch (e) {
      return null;
    }
  };

  const rematchGame = function () {
    try {
      return _rematchGame();
    } catch (e) {
      return null;
    }
  };

  const acceptGame = function () {
    try {
      return _acceptGame();
    } catch (e) {
      return null;
    }
  };

  const declineGame = function () {
    try {
      return _declineGame();
    } catch (e) {
      return null;
    }
  };

  const setMockData = (mockData) => {
    _setMockData(mockData);
  };

  // return object
  return {
    init: init,
    get: get,
    getPartial: getPartial,
    getResult: getResult,
    getRematch: getRematch,
    sendMove: sendMove,
    passGame: passGame,
    forfeitGame: forfeitGame,
    rematchGame: rematchGame,
    acceptGame: acceptGame,
    declineGame: declineGame,
    setMockData: setMockData,
  };
})();
