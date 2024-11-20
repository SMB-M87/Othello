Game.Model = (function () {
  // config
  let configMap = {
    apiUrl: "",
    apiKey: "",
  };

  let stateMap = {
    firstLoad: true,
  };

  // private functions
  const _init = function (url, key) {
    configMap.apiUrl = url;
    configMap.apiKey = key;
    console.log("Model module started from url: " + configMap.apiUrl);
  };

  const _getGameState = function () {
    let dataPromise;
    if (stateMap.firstLoad) {
      dataPromise = Game.Data.get();
      stateMap.firstLoad = false;

      return dataPromise
        .then((data) => {
          Game.Othello.updateBoard(data.partial.board);
          return data;
        })
        .catch((e) => {
          console.log(e.message);
          return { data: null, error: e.message };
        });
    } else {
      dataPromise = Game.Data.getPartial();

      return dataPromise
        .then((data) => {
          Game.Othello.updateBoard(data.board);
          return data;
        })
        .catch((e) => {
          console.log(e.message);
          return { data: null, error: e.message };
        });
    }
  };

  const _sendMove = function (row, col) {
    const token = configMap.apiKey;

    const moveData = {
      playerToken: token,
      row: row,
      column: col,
    };

    $.ajax({
      url: configMap.apiUrl + "move",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify(moveData),
    })
      .then((response) => {
        feedbackWidget.show(
          "You have successfully made a move, wait on your opponent to make his.",
          "Success"
        );
        _getGameState();
      })
      .catch((error) => {
        feedbackWidget.show(
          "Move failed... Try again or make a different move." +
            error.responseText,
          "Danger"
        );
      });
  };

  const _passGame = function () {
    const token = configMap.apiKey;

    return $.ajax({
      url: configMap.apiUrl + "pass",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ token: token }),
    }).then((response) => {
      _getGameState();
    });
  };

  const _forfeitGame = function () {
    const token = configMap.apiKey;

    return $.ajax({
      url: configMap.apiUrl + "forfeit",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ token: token }),
    }).then((response) => {
      _getGameState();
    });
  };

  // public functions
  const init = (url, key) => {
    _init(url, key);
  };

  const sendMove = function (row, col) {
    _sendMove(row, col);
  };

  const passGame = function () {
    return _passGame();
  };

  const forfeitGame = function () {
    return _forfeitGame();
  };

  // return object
  return {
    init: init,
    getGameState: _getGameState,
    sendMove: sendMove,
    passGame: passGame,
    forfeitGame: forfeitGame,
  };
})();
