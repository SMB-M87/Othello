Game.Model = (function () {
  // config
  let configMap = {
    redirectUrl: null,
  };

  let stateMap = {
    firstLoad: true,
    turnReload: true,
    lastLoad: true,
    endLoad: false,
    opponent: "",
    playerColor: "",
    board: null,
  };

  // private functions
  const _init = function (url) {
    configMap.redirectUrl = url;
  };

  const _getGameState = function () {
    let dataPromise = stateMap.firstLoad
      ? Game.Data.get()
      : Game.Data.getPartial();

    return dataPromise
      .then((data) => {
        // Get View
        if (stateMap.firstLoad) {
          if (data.partial.playersTurn === 0) {
            window.location.href = `${configMap.redirectUrl}Result`;
            return;
          }
          stateMap.opponent = data.opponent;
          stateMap.playerColor = data.color;
          stateMap.board = data.partial.board;
          stateMap.firstLoad = false;

          _updateGameInfo(stateMap.opponent, stateMap.playerColor);
          _toggleButtons(
            data.partial.isPlayersTurn,
            data.partial.possibleMove,
            data.partial.playersTurn
          );
          _updateTimer(
            data.partial.isPlayersTurn,
            data.partial.time,
            data.partial.playersTurn
          );

          // Get Partial
          // Player's turn
        } else if (data.playersTurn !== 0 && data.isPlayersTurn) {
          if (stateMap.turnReload) {
            Game.Othello.updateBoard(
              data.board,
              data.isPlayersTurn,
              stateMap.playerColor
            );
            Game.Othello.highlightChanges(data.board);
            _toggleButtons(
              data.isPlayersTurn,
              data.possibleMove,
              data.playersTurn
            );
            stateMap.turnReload = false;
            stateMap.opponentReload = true;
          }
          _updateTimer(data.isPlayersTurn, data.time, data.playersTurn);
          stateMap.board = data.board;

          // Opponent's turn
        } else if (data.playersTurn !== 0 && !data.isPlayersTurn) {
          if (!stateMap.turnReload) {
            Game.Othello.updateBoard(
              data.board,
              data.isPlayersTurn,
              stateMap.playerColor
            );

            Game.Othello.makeMove(data.board);
            _toggleButtons(
              data.isPlayersTurn,
              data.possibleMove,
              data.playersTurn
            );
            stateMap.turnReload = true;
          }
          _updateTimer(data.isPlayersTurn, data.time, data.playersTurn);
          stateMap.board = data.board;

          // Last Board Update
        } else if (data.playersTurn === 0 && data.inGame && stateMap.lastLoad) {
          Game.Othello.updateBoard(
            data.board,
            data.isPlayersTurn,
            stateMap.playerColor
          );
          Game.Othello.highlightChanges(data.board);
          _toggleButtons(
            data.isPlayersTurn,
            data.possibleMove,
            data.playersTurn
          );
          _updateTimer(data.isPlayersTurn, data.time, data.playersTurn);
          stateMap.board = data.board;
          stateMap.lastLoad = false;

          // Game Finished
        } else if (!data.inGame && !stateMap.endLoad) {
          if (stateMap.lastLoad) {
            Game.Othello.updateBoard(
              stateMap.board,
              false,
              stateMap.playerColor
            );
            _toggleButtons(
              data.isPlayersTurn,
              data.possibleMove,
              data.playersTurn
            );
            _updateTimer(data.isPlayersTurn, data.time, data.playersTurn);
            stateMap.lastLoad = false;
          }
          configMap.endLoad = true;
        }
        return data;
      })
      .catch((e) => {
        console.log(e.message);
        return { data: null, error: e.message };
      });
  };

  const _updateGameInfo = function (opponent, playerColor) {
    const title = document.getElementById("player-info");
    document.getElementById("opponent-name").textContent = opponent;
    title.classList.add("flex", "fade-in");

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

    document.getElementById("score-display").classList.add("flex", "fade-in");

    document.getElementById("player-score").id =
      playerColor === 1 ? "white-score" : "black-score";
    document.getElementById("opponent-score").id =
      playerColor === 1 ? "black-score" : "white-score";
  };

  const _updateTimer = function (isPlayersTurn, timeLeft, playersTurn) {
    const timer = document.getElementById("timer-color-indicator");

    if (playersTurn !== 0) {
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
    } else {
      timer.classList.remove("green", "red");
      timer.classList.add("hidden");
    }
  };

  const _toggleButtons = function (isPlayersTurn, possibleMove, playersTurn) {
    const feedbackWidget = document.getElementById("feedback-widget");
    const buttonContainer = document.getElementById("button-container");
    const passButton = document.getElementById("pass-button");
    const forfeitButton = document.getElementById("forfeit-button");
    const rematchButton = document.getElementById("rematch-button");

    if (playersTurn === 0) {
      buttonContainer.classList.add("flex");
      buttonContainer.classList.remove("hidden");

      passButton.classList.add("hidden");
      passButton.classList.remove("inline-block", "fade-in-wobble");

      forfeitButton.classList.add("hidden");
      forfeitButton.classList.remove("inline-block", "fade-in");

      rematchButton.classList.add("inline-block", "fade-in");
      rematchButton.classList.remove("hidden");
    } else {
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
    }
  };

  const _getBoard = function () {
    return stateMap.board;
  };

  // public functions
  const init = (url) => {
    return _init(url);
  };

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

