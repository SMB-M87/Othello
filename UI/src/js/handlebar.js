Game.Handlebar = (function () {
  // Private variables
  const templates = spa_templates || {};

  // Private methods
  const _render = function (templateName, data, containerId) {
    if (!templates[templateName]) {
      throw new Error(`Template '${templateName}' is not available.`);
    }

    const html = templates[templateName](data || {});
    const container = document.getElementById(containerId);
    if (!container) {
      throw new Error(`Container with ID '${containerId}' not found.`);
    }
    container.innerHTML = html;
  };

  const _prepareBoardData = function (board, playersTurn, playerColor) {
    const boardData = [];

    for (let row = 0; row < 8; row++) {
      const rowData = [];
      for (let col = 0; col < 8; col++) {
        const currentValue = board[row]?.[col] ?? 0;
        const isPossibleMove = playersTurn === playerColor && currentValue === 3;
        const hasPiece = currentValue === 1 || currentValue === 2;
        const pieceColorClass =
          currentValue === 1
            ? "white-piece"
            : currentValue === 2
            ? "black-piece"
            : "";
        rowData.push({
          row,
          col,
          cellClass: (row + col) % 2 === 0 ? "even" : "odd",
          randomX: Math.random(),
          randomY: Math.random(),
          randomRot: Math.random(),
          animationDelay: (row * 8 + col) * 0.02,
          isPossibleMove,
          hasPiece,
          pieceColorClass,
          playerColorClass: playerColor === 1 ? "white" : "black",
          highlight: false,
          flip: false,
        });
      }
      boardData.push(rowData);
    }

    return boardData;
  };  

  const _attachListeners = function (listeners) {
    listeners.forEach(({ selector, event, callback }) => {
      const element = document.querySelector(selector);
      if (element) {
        element.addEventListener(event, callback);
      }
    });
  };

  // Public methods
  const renderBody = function (data) {
    _render("body", data, "body");
  };

  const renderBoard = function (board, playersTurn, playerColor) {
    const boardData = _prepareBoardData(board, playersTurn, playerColor);
    _render("board", { boardRows: boardData }, "game-board-container");
  };

  const attachEventListeners = function () {
    const listeners = [
      {
        selector: "#game-board-container",
        event: "click",
        callback: (event) => {
          const cell = event.target.closest("td");
          if (!cell) return;

          const row = cell.dataset.row;
          const col = cell.dataset.col;

          Game.Data.sendMove(row, col)
            .then(() => {
              FeedbackSingleton.getInstance().log({
                message: `Move made on row ${row} and col ${col}.`,
                type: "Success",
              });
            })
            .catch((error) => {
              FeedbackSingleton.getInstance().log({
                message: `Move failed: ${error.responseText || error}`,
                type: "Danger",
              });
            });
        },
      },
      {
        selector: "#pass-button",
        event: "click",
        callback: () => {
          Game.Data.passGame()
            .then(() => {
              FeedbackSingleton.getInstance().log({
                message: `Turn passed.`,
                type: "Success",
              });
            })
            .catch((error) => {
              FeedbackSingleton.getInstance().log({
                message: `Pass failed: ${error.responseText || error}`,
                type: "Danger",
              });
            });
        },
      },
      {
        selector: "#forfeit-button",
        event: "click",
        callback: () => {
          FeedbackSingleton.getInstance().show(
            "Are you sure you want to forfeit?",
            "info",
            8000,
            true,
            [
              {
                icon: "✓",
                class: "feedback-icon feedback-icon--success",
                callback: () => {
                  Game.Data.forfeitGame()
                    .then(() => {
                      FeedbackSingleton.getInstance().removeLog();
                      FeedbackSingleton.getInstance().hide();
                    })
                    .catch((error) => {
                      FeedbackSingleton.getInstance().log({
                        message: `Forfeit failed: ${
                          error.responseText || error
                        }`,
                        type: "Danger",
                      });
                    });
                },
              },
              {
                icon: "✕",
                class: "feedback-icon feedback-icon--danger",
                callback: () => {
                  FeedbackSingleton.getInstance().hide();
                },
              },
            ]
          );
        },
      },
      {
        selector: "#rematch-button",
        event: "click",
        callback: () => {
          Game.Data.rematchGame()
            .then(() => {
              FeedbackSingleton.getInstance().removeLog();
              Game.Data.redirectHome();
            })
            .catch((error) => {
              FeedbackSingleton.getInstance().log({
                message: `Rematch failed: ${error.responseText || error}`,
                type: "Danger",
              });
            });
        },
      },
    ];

    _attachListeners(listeners);
  };

  return {
    renderBody,
    renderBoard,
    attachEventListeners,
  };
})();
