Game.Model = (function () {
  // config
  let stateMap = {
    firstLoad: true,
    turnReload: true,
    opponent: "",
    playerColor: "",
    previousData: null,
  };

  // private functions
  const _getGameState = function () {
    let dataPromise;

    if (stateMap.firstLoad) {
      dataPromise = Game.Data.get();
    } else {
      dataPromise = Game.Data.getPartial();
    }

    return dataPromise
      .then((data) => {
        if (stateMap.firstLoad) {
          stateMap.opponent = data.opponent;
          stateMap.playerColor = data.color;

          document.getElementById("opponent-name").textContent = data.opponent;
          _updateColorIndicators(stateMap.playerColor);
          Game.Othello.updateBoard(
            data.partial.board,
            data.partial.isPlayersTurn,
            stateMap.playerColor
          );
          _toggleButtons(data.partial.isPlayersTurn, data.partial.possibleMove);
          _updateTimer(data.partial.isPlayersTurn, data.partial.time);
          stateMap.previousData = data;
          stateMap.firstLoad = false;
        } else if (data.isPlayersTurn) {
          if (stateMap.turnReload) {
            Game.Othello.updateBoard(
              data.board,
              data.isPlayersTurn,
              stateMap.playerColor
            );
            _toggleButtons(data.isPlayersTurn, data.possibleMove);
            stateMap.turnReload = false;
          }
          _updateTimer(data.isPlayersTurn, data.time);
          stateMap.previousData.partial = data;
        } else {
          if (!stateMap.turnReload) {
            stateMap.turnReload = true;
          }
          Game.Othello.updateBoard(
            data.board,
            data.isPlayersTurn,
            stateMap.playerColor
          );
          _toggleButtons(data.isPlayersTurn, data.possibleMove);
          _updateTimer(data.isPlayersTurn, data.time);
          stateMap.previousData.partial = data;
        }
        return data;
      })
      .catch((e) => {
        console.log(e.message);
        return { data: null, error: e.message };
      });
  };

  const _updateColorIndicators = function (playerColor) {
    const playerColorIndicator = document.getElementById(
      "player-color-indicator"
    );
    const opponentColorIndicator = document.getElementById(
      "opponent-color-indicator"
    );

    let playerColorHex = playerColor === 1 ? "#FFFFFF" : "#000000";
    let opponentColorHex = playerColor === 1 ? "#000000" : "#FFFFFF";

    playerColorIndicator.style.backgroundColor = playerColorHex;
    opponentColorIndicator.style.backgroundColor = opponentColorHex;

    playerColorIndicator.style.color = opponentColorHex;
    opponentColorIndicator.style.color = playerColorHex;

    document.getElementById("player-score").id =
      playerColor === 1 ? "white-score" : "black-score";
    document.getElementById("opponent-score").id =
      playerColor === 1 ? "black-score" : "white-score";
  };

  const _updateTimer = function (isPlayersTurn, timeLeft) {
    const timer = document.getElementById("timer-color-indicator");

    timer.style.backgroundColor = isPlayersTurn ? "lightgreen" : "lightcoral";
    timer.style.color = isPlayersTurn ? "#006400" : "#8B0000";

    document.getElementById("time-remaining").textContent = timeLeft;
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

  // public functions
  const getGameState = () => {
    return _getGameState();
  };

  // return object
  return {
    getGameState: getGameState,
  };
})();
