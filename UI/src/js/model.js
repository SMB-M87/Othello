Game.Model = (function () {
  // config
  let configMap = {
    redirectUrl: null
  };

  let stateMap = {
    firstLoad: true,
    turnReload: true,
    opponent: "",
    playerColor: "",
    board: null,
  };

  // private functions
  const _init = function (url) {
    configMap.redirectUrl = url;
  }

  const _getGameState = function () {
    let dataPromise = stateMap.firstLoad
      ? Game.Data.get()
      : Game.Data.getPartial();

    return dataPromise
      .then((data) => {
        // Get View
        if (stateMap.firstLoad) {
          // Not In Game or Game Finished => Redirect to game result view of MVC
          if (!data.partial.inGame && data.partial.finished) {
            window.location.href = configMap.redirectUrl;
            return;
          }
          stateMap.opponent = data.opponent;
          stateMap.playerColor = data.color;
          stateMap.board = data.partial.board;
          stateMap.firstLoad = false;

          // Show opponents name & set color indicators for the score 
          document.getElementById("opponent-name").textContent = data.opponent;
          _updateColorIndicators(stateMap.playerColor);

          Game.Othello.updateBoard(
            data.partial.board,
            data.partial.isPlayersTurn,
            stateMap.playerColor
          );
          _toggleButtons(data.partial.isPlayersTurn, data.partial.possibleMove);
          _updateTimer(data.partial.isPlayersTurn, data.partial.time);

          // Get Partial
          // Not In Game or Game Finished => Redirect to game result view of MVC
        } else if (!data.inGame && data.finished) {
          window.location.href = configMap.redirectUrl;
          return;

          // Player's turn
        } else if (data.isPlayersTurn) {
          // Update board & buttons once
          if (stateMap.turnReload) {
            Game.Othello.updateBoard(
              data.board,
              data.isPlayersTurn,
              stateMap.playerColor
            );
            Game.Othello.highlightChanges(data.board);
            _toggleButtons(data.isPlayersTurn, data.possibleMove);
            stateMap.turnReload = false;
            stateMap.opponentReload = true;
          }
          // Countdown continuous update
          _updateTimer(data.isPlayersTurn, data.time);
          stateMap.board = data.board;

          // Opponent's turn
        } else {
          if (!stateMap.turnReload) {
            Game.Othello.updateBoard(
              data.board,
              data.isPlayersTurn,
              stateMap.playerColor
            );

            Game.Othello.makeMove(data.board);
            stateMap.turnReload = true;
          }
          _toggleButtons(data.isPlayersTurn, data.possibleMove);
          _updateTimer(data.isPlayersTurn, data.time);
          stateMap.board = data.board;
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

    playerColorIndicator.classList.remove("white", "black");
    opponentColorIndicator.classList.remove("white", "black");

    if (playerColor === 1) {
      playerColorIndicator.classList.add("white");
      opponentColorIndicator.classList.add("black");
    } else {
      playerColorIndicator.classList.add("black");
      opponentColorIndicator.classList.add("white");
    }

    document.getElementById("player-score").id =
      playerColor === 1 ? "white-score" : "black-score";
    document.getElementById("opponent-score").id =
      playerColor === 1 ? "black-score" : "white-score";
  };

  const _updateTimer = function (isPlayersTurn, timeLeft) {
    const timer = document.getElementById("timer-color-indicator");

    if (isPlayersTurn) {
      timer.classList.remove("red");
      timer.classList.add("green");

      if (
        timeLeft.endsWith("0") ||
        timeLeft.endsWith("5") ||
        timeLeft === "4" ||
        timeLeft === "3" ||
        timeLeft === "2" ||
        timeLeft === "1"
      ) {
        timer.classList.add("wobble");
      } else {
        timer.classList.remove("wobble");
      }
    } else {
      timer.classList.remove("green");
      timer.classList.add("red");
    }
    document.getElementById("time-remaining").textContent = timeLeft;
  };

  const _toggleButtons = function (isPlayersTurn, possibleMove) {
    const feedbackWidget = document.getElementById("feedback-widget");
    const buttonContainer = document.getElementById("button-container");
    const passButton = document.getElementById("pass-button");
    const forfeitButton = document.getElementById("forfeit-button");

    if (isPlayersTurn) {
      feedbackWidget.classList.add("visible");
      feedbackWidget.classList.remove("hidden");

      buttonContainer.classList.add("flex");
      buttonContainer.classList.remove("hidden");

      if (!possibleMove) {
        passButton.classList.add("inline-block", "fade-in-wobble");
        passButton.classList.remove("hidden");
      } else {
        passButton.classList.add("hidden");
        passButton.classList.remove("inline-block");
      }

      forfeitButton.classList.add("inline-block", "fade-in");
      forfeitButton.classList.remove("hidden");
    } else {
      feedbackWidget.classList.add("hidden");
      feedbackWidget.classList.remove("visible");

      buttonContainer.classList.add("hidden");
      buttonContainer.classList.remove("flex");

      passButton.classList.add("hidden");
      passButton.classList.remove("inline-block", "fade-in-wobble");

      forfeitButton.classList.add("hidden");
      forfeitButton.classList.remove("inline-block", "fade-in");
    }
  };

  const _getBoard = function () {
    return stateMap.board;
  };

  // public functions
  const init = (url) => {
    return _init(url);
  }

  const getGameState = () => {
    return _getGameState();
  };

  // return object
  return {
    init: init,
    getGameState: getGameState,
    getBoard: _getBoard,
  };
})();
