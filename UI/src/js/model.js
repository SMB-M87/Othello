Game.Model = (function () {
  // config
  let stateMap = {
    firstLoad: true,
    turnReload: true,
    lastLoad: true,
    endLoad: true,
    rematchLoad: true,
    opponent: "",
    playerColor: "",
    board: null,
  };

  // private functions
  const _getGameState = function () {
    let dataPromise = !stateMap.endLoad
      ? Game.Data.getRematch()
      : !stateMap.lastLoad 
      ? Game.Data.getResult()
      : stateMap.firstLoad
      ? Game.Data.get()
      : Game.Data.getPartial();

    return dataPromise
      .then((data) => {
        // Rematch
        if (!stateMap.endLoad) {
          if (data === stateMap.opponent && stateMap.rematchLoad) {
            const feedbackWidget = FeedbackSingleton.getInstance();

            feedbackWidget.show(
              `${stateMap.opponent} wants a rematch, do you accept?`,
              "info",
              45000,
              true,
              [
                {
                  icon: "✓",
                  class: "feedback-icon feedback-icon--success",
                  callback: () => {
                    Game.Data.acceptGame();
                    feedbackWidget.hide();
                    window.location.reload();
                  },
                },
                {
                  icon: "✕",
                  class: "feedback-icon feedback-icon--danger",
                  callback: () => {
                    Game.Data.declineGame();
                    feedbackWidget.hide();
                  },
                },
              ]
            );
            stateMap.rematchLoad = false;
          }

          // Game Finished
        } else if (!stateMap.lastLoad && stateMap.endLoad) {
          _renderResult(data, stateMap.opponent);
          stateMap.endLoad = false;

          // Get View
        } else if (stateMap.firstLoad) {
          _renderView(
            data.opponent,
            data.color,
            data.partial.time,
            data.partial.playersturn,
            data.partial.board
          );
          stateMap.opponent = data.opponent;
          stateMap.playerColor = data.color;
          stateMap.firstLoad = false;

          // Get Partial
          // Player's turn
        } else if (
          data.playersTurn !== 0 &&
          data.playersTurn === stateMap.playerColor
        ) {
          _updateTimer(stateMap.playerColor, data.time, data.playersTurn);
          if (stateMap.turnReload) {
            _renderPartial(stateMap.playerColor, data.playersTurn, data.board);
            stateMap.turnReload = false;
          }

          // Opponent's turn
        } else if (
          data.playersTurn !== 0 &&
          data.playersTurn !== stateMap.playerColor
        ) {
          _updateTimer(stateMap.playerColor, data.time, data.playersTurn);
          if (!stateMap.turnReload) {
            _renderPartial(stateMap.playerColor, data.playersTurn, data.board);
            stateMap.turnReload = true;
          }

          // Last Board Update
        } else if ((!data.inGame || (data.playersTurn === 0 && data.inGame)) && stateMap.lastLoad) {
          _updateTimer(stateMap.playerColor, 0, data.playersTurn);
          _renderPartial(stateMap.playerColor, 0, data.board);
          stateMap.lastLoad = false;
        } 
        return data;
      })
      .catch((e) => {
        console.log(e.message);
        return { data: null, error: e.message };
      });
  };

  const _renderResult = function (data, opponent) {
    document.getElementById("game-status").textContent =
    data.draw
      ? "Drew"
      : data.winner === opponent
      ? "Lost"
      : "Won";

      if (data.forfeit) {
        document.getElementById("forfeit-title").textContent = "by forfeit"
      } 

      Game.Othello.updateBoard(data.board, 0, stateMap.playerColor);
  }

  const _renderView = function (
    opponent,
    playerColor,
    countdown,
    playersTurn,
    board
  ) {
    _updateGameInfo(opponent, playerColor);
    _updateTimer(playerColor, countdown, playersTurn);
    Game.Othello.updateBoard(board, playersTurn, playerColor);
    _toggleButtons(
      playerColor,
      board.some((row) => row.includes(3)),
      playersTurn
    );
  };

  const _renderPartial = function (playerColor, playersTurn, board) {
    Game.Othello.updateBoard(
      board,
      playersTurn,
      playerColor
    );
    _toggleButtons(
      playerColor,
      board ? board.some((row) => row.includes(3)) : false,
      playersTurn
    );
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

  const _updateTimer = function (playerColor, timeLeft, playersTurn) {
    const timer = document.getElementById("timer-color-indicator");

    if (playersTurn !== 0) {
      if (playerColor === playersTurn) {
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

  const _toggleButtons = function (playerColor, possibleMove, playersTurn) {
    const feedbackWidget = document.getElementById("feedback-widget");
    const buttonContainer = document.getElementById("button-container");
    const passButton = document.getElementById("pass-button");
    const forfeitButton = document.getElementById("forfeit-button");

    if (playersTurn === 0) {
      const rematchButton = document.getElementById("rematch-button");

      buttonContainer.classList.add("flex");
      buttonContainer.classList.remove("hidden");

      passButton.classList.add("hidden");
      passButton.classList.remove("inline-block", "fade-in-wobble");

      forfeitButton.classList.add("hidden");
      forfeitButton.classList.remove("inline-block", "fade-in");

      rematchButton.classList.add("inline-block", "fade-in");
      rematchButton.classList.remove("hidden");
    } else {
      if (playerColor === playersTurn) {
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

  const _setBoard = function (board) {
    stateMap.board = board;
  };

  const _getBoard = function () {
    return stateMap.board;
  };

  const _getOpponent = function () {
    return stateMap.opponent;
  };

  // public functions
  const getGameState = () => {
    return _getGameState();
  };

  const setBoard = function (board) {
    return _setBoard(board);
  };

  const getBoard = function () {
    return _getBoard();
  };

  const getOpponent = function () {
    return _getOpponent();
  };

  // return object
  return {
    getGameState: getGameState,
    setBoard: setBoard,
    getBoard: getBoard,
    getOpponent: getOpponent,
  };
})();
