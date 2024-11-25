Game.Othello = (function () {
  // private functions
  const _updateBoard = function (board, playersTurn, playerColor) {
    const previousBoard = Game.Model.getBoard() || [];
    const boardCells = document.querySelectorAll("#game-board-container td");

    boardCells.forEach((cell) => {
      const row = parseInt(cell.dataset.row, 10);
      const col = parseInt(cell.dataset.col, 10);
      const currentValue = board[row]?.[col] ?? 0;
      const previousValue = previousBoard[row]?.[col] || 0;

      const cellDiv = cell.querySelector(".cell-div");

      if (playersTurn !== playerColor && previousValue === 3) {
        cellDiv.innerHTML = "";
      }

      if (playersTurn === 0) {
        cellDiv.innerHTML = "";

        if (board && (currentValue === 1 || currentValue === 2)) {
          const piece = document.createElement("i");
          piece.className = `fa fa-circle ${
            currentValue === 1 ? "white-piece" : "black-piece"
          }`;
          cellDiv.appendChild(piece);
        } else if (previousValue === 1 || previousValue === 2) {
          const piece = document.createElement("i");
          piece.className = `fa fa-circle ${
            previousValue === 1 ? "white-piece" : "black-piece"
          }`;
          cellDiv.appendChild(piece);
        }
      } else if (playersTurn === playerColor && currentValue === 3) {
        cellDiv.innerHTML = "";
        const moveIndicator = document.createElement("div");
        moveIndicator.className = `possible-move ${
          playerColor === 1 ? "white-border" : "black-border"
        }`;
        cellDiv.appendChild(moveIndicator);
        cell.appendChild(cellDiv);
      } else if (
        currentValue !== previousValue &&
        (currentValue === 1 || currentValue === 2)
      ) {
        const piece = cell.querySelector("i");

        if (piece) {
          piece.classList.remove("highlight");
          piece.classList.add("flip");

          setTimeout(() => {
            piece.classList.remove("white-piece", "black-piece");
            piece.classList.add(
              currentValue === 1 ? "white-piece" : "black-piece"
            );
          }, 300);
        } else {
          cellDiv.innerHTML = "";
          const newPiece = document.createElement("i");
          newPiece.className = `fa fa-circle ${
            currentValue === 1 ? "white-piece" : "black-piece"
          } highlight`;
          cellDiv.appendChild(newPiece);
        }
      } else if (
        currentValue === previousValue &&
        (currentValue === 1 || currentValue === 2)
      ) {
        const piece = cell.querySelector("i");
        piece.classList.remove("highlight", "flip");
      }
    });

    _calculateScores();
    Game.Model.setBoard(board);
  };

  const _calculateScores = function () {
    let whiteScore = 0;
    let blackScore = 0;

    const boardCells = document.querySelectorAll("#game-board-container td");

    boardCells.forEach((cell) => {
      const piece = cell.querySelector("i");

      if (piece) {
        if (piece.classList.contains("white-piece")) {
          whiteScore++;
        } else if (piece.classList.contains("black-piece")) {
          blackScore++;
        }
      }
    });
    document.getElementById("white-score").textContent = whiteScore;
    document.getElementById("black-score").textContent = blackScore;
  };

  // public functions
  const updateBoard = function (board, playersTurn, playerColor) {
    _updateBoard(board, playersTurn, playerColor);
  };

  // return object
  return {
    updateBoard: updateBoard
  };
})();
