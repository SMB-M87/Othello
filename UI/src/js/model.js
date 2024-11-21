Game.Model = (function () {
  // config
  let configMap = {
    apiUrl: "",
    apiKey: "",
  };

  let stateMap = {
    firstLoad: true,
    opponent: '',
    playerColor: '',
    previousData: null,
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

      return dataPromise
        .then((data) => {
          stateMap.opponent = data.opponent;
          stateMap.playerColor = data.color;
          stateMap.firstLoad = false;

          document.getElementById('opponent-name').textContent = data.opponent;
          _updateColorIndicators(stateMap.playerColor);

          Game.Othello.update(data.partial, stateMap.playerColor);
          _updateTimer(data.partial.isPlayersTurn, data.partial.time);
          _toggleButtons(data.partial.isPlayersTurn, data.partial.possibleMove);
          stateMap.previousData = data;

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
          Game.Othello.update(data, stateMap.playerColor);
          _updateTimer(data.isPlayersTurn, data.time);
          _toggleButtons(data.isPlayersTurn, data.possibleMove);
          stateMap.previousData.partial = data;
          return data;
        })
        .catch((e) => {
          console.log(e.message);
          return { data: null, error: e.message };
        });
    }
  };

  const _updateColorIndicators = function (playerColor) {
    const playerColorIndicator = document.getElementById('player-color-indicator');
    const opponentColorIndicator = document.getElementById('opponent-color-indicator');
  
    let playerColorHex = playerColor === 1 ? '#FFFFFF' : '#000000';
    let opponentColorHex = playerColor === 1 ? '#000000' : '#FFFFFF';
  
    playerColorIndicator.style.backgroundColor = playerColorHex;
    opponentColorIndicator.style.backgroundColor = opponentColorHex;

    playerColorIndicator.style.color = opponentColorHex;
    opponentColorIndicator.style.color = playerColorHex;
  
    document.getElementById('player-score').id = playerColor === 1 ? 'white-score' : 'black-score';
    document.getElementById('opponent-score').id = playerColor === 1 ? 'black-score' : 'white-score';
  };

  const _updateTimer = function (isPlayersTurn, timeLeft) {
    const timer = document.getElementById('timer-color-indicator');
    
    timer.style.backgroundColor = isPlayersTurn ? 'lightgreen' : 'lightcoral';
    timer.style.color = isPlayersTurn ? "#006400" : "#8B0000";

    document.getElementById('time-remaining').textContent = timeLeft;
  };

  const _toggleButtons = function (isPlayersTurn, possibleMove) {
    const feedbackWidget = document.getElementById("feedback-widget");
    const buttonContainer = document.getElementById("button-container");
    const passButton = document.getElementById("pass-button");
    const forfeitButton = document.getElementById("forfeit-button");
  
    if (isPlayersTurn) {
      feedbackWidget.style.display = "block";
      buttonContainer.style.display = "flex";
  
      if (!possibleMove) {
        passButton.style.display = "inline-block";
      } else {
        passButton.style.display = "none";
      }
  
      forfeitButton.style.display = "inline-block";
    } else {
      feedbackWidget.style.display = "none";
      buttonContainer.style.display = "none";
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
        _getGameState();
      })
      .catch((error) => {
        feedbackWidget.show(
          "Move failed: " +
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
