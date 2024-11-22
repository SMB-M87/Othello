Game.Model = (function () {
  // config
  let stateMap = {
    firstLoad: true,
    turnReload: true,
    opponent: "",
    playerColor: "",
    board: null,
  };

  // private functions
  const _getGameState = function () {
    let dataPromise = stateMap.firstLoad
      ? Game.Data.get()
      : Game.Data.getPartial();

    return dataPromise
      .then((data) => {
        if (stateMap.firstLoad) {
          stateMap.opponent = data.opponent;
          stateMap.playerColor = data.color;
          stateMap.board = data.partial.board;
          stateMap.firstLoad = false;

          document.getElementById("opponent-name").textContent = data.opponent;
          _updateColorIndicators(stateMap.playerColor);
          Game.Othello.updateBoard(
            data.partial.board,
            data.partial.isPlayersTurn,
            stateMap.playerColor
          );
          _toggleButtons(data.partial.isPlayersTurn, data.partial.possibleMove);
          _updateTimer(data.partial.isPlayersTurn, data.partial.time);
        } else if (data.isPlayersTurn) {
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
          _updateTimer(data.isPlayersTurn, data.time);
          stateMap.board = data.board;
        } else {
          Game.Othello.updateBoard(
            data.board,
            data.isPlayersTurn,
            stateMap.playerColor
          );
          if (!stateMap.turnReload) {
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
        passButton.classList.add("inline-block", "pass-wobble");
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
      passButton.classList.remove("inline-block", "pass-wobble");

      forfeitButton.classList.add("hidden");
      forfeitButton.classList.remove("inline-block", "fade-in");
    }
  };

  const _getBoard = function () {
    return stateMap.board;
  };

  // public functions
  const getGameState = () => {
    return _getGameState();
  };

  // return object
  return {
    getGameState: getGameState,
    getBoard: _getBoard,
  };
})();
