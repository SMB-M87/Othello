Game.Data = (function () {
  let configMap = {
    apiUrl: null,
    apiKey: null,
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
  const _init = function (url, key, environment) {
    if (environment == "production") {
      configMap.apiUrl = url;
      configMap.apiKey = key;
      stateMap.environment = environment;
    } else if (environment == "development") {
      configMap.mock.url = url;
      configMap.mock.key = key;
      stateMap.environment = environment;
    } else {
      throw new Error("This environment is unknown.");
    }
  };

  const _get = function () {
    if (stateMap.environment == "production") {
      return $.ajax({
        url: configMap.apiUrl + "game/view/" + configMap.apiKey,
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
        url: configMap.apiUrl + "game/partial/" + configMap.apiKey,
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
            url: configMap.apiUrl + "result/last/" + configMap.apiKey,
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

    const config = `${username} ${configMap.apiKey}`;

    const encoded = encodeURIComponent(config);

    return $.ajax({
      url: `${configMap.apiUrl}player/rematch/${encoded}`,
      method: "GET",
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
    })
      .then((response) => {
        if (response) {
          return response;
        } else {
          return null;
        }
      })
      .catch((error) => {
        return null;
      });
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
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
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
      crossDomain: true,
      xhrFields: {
        withCredentials: true,
      },
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
      PlayerToken: configMap.apiKey,
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
      SenderToken: configMap.apiKey,
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
      SenderToken: configMap.apiKey,
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

  const _redirectHome = () => {
    window.location.href = `${configMap.redirectUrl}Home/Index`;
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

  const getResult = (delay = 50) => {
    return _getResult(delay);
  };

  const getRematch = () => {
    return _getRematch();
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

  const rematchGame = function () {
    return _rematchGame();
  };

  const acceptGame = function () {
    return _acceptGame();
  };

  const declineGame = function () {
    return _declineGame();
  };

  const setMockData = (mockData) => {
    _setMockData(mockData);
  };

  const redirectHome = () => {
    _redirectHome();
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
    redirectHome: redirectHome,
    setMockData: setMockData,
  };
})();
