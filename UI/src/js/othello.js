Game.Othello = (function () {
  // private functions
  const _updateBoard = function (board, isPlayersTurn, playerColor) {
    const boardContainer = document.getElementById("game-board-container");
    boardContainer.innerHTML = "";

    const table = document.createElement("table");
    table.className = "othello-board";

    for (let row = 0; row < 8; row++) {
      const tr = document.createElement("tr");
      for (let col = 0; col < 8; col++) {
        const td = document.createElement("td");
        td.dataset.row = row;
        td.dataset.col = col;
        td.classList.add("board-cell");
        td.classList.add((row + col) % 2 === 0 ? "even" : "odd");

        const cellValue = board[row][col];
        const cellDiv = document.createElement("div");
        cellDiv.classList.add("cell-div");

        if (cellValue === 1) {
          const piece = document.createElement("i");
          piece.className = "fa fa-circle white-piece";
          cellDiv.appendChild(piece);
        } else if (cellValue === 2) {
          const piece = document.createElement("i");
          piece.className = "fa fa-circle black-piece";
          cellDiv.appendChild(piece);
        } else if (cellValue === 3) {
          if (isPlayersTurn) {
            const moveIndicator = document.createElement("div");
            moveIndicator.className = "possible-move";
            moveIndicator.classList.add(
              playerColor === 1 ? "white-border" : "black-border"
            );
            cellDiv.appendChild(moveIndicator);
          }
        }
        td.appendChild(cellDiv);
        tr.appendChild(td);
      }
      table.appendChild(tr);
    }
    boardContainer.appendChild(table);
    _calculateScores();
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

  const _highlightChanges = function (board) {
    const previousBoard = Game.Model.getBoard() || [];
    const boardCells = document.querySelectorAll("#game-board-container td");

    boardCells.forEach((cell) => {
      const row = parseInt(cell.dataset.row, 10);
      const col = parseInt(cell.dataset.col, 10);
      const currentValue = board[row][col];
      const previousValue = previousBoard[row]?.[col] || 0;

      if (
        currentValue !== previousValue &&
        (currentValue === 1 || currentValue === 2)
      ) {
        const piece = cell.querySelector("i");
        if (piece) {
          piece.classList.add("highlight");
        }
      }
    });
  };

  // public functions
  const updateBoard = function (board, isPlayersTurn, playerColor) {
    _updateBoard(board, isPlayersTurn, playerColor);
  };

  const highlightChanges = function (board) {
    _highlightChanges(board);
  };

  // return object
  return {
    updateBoard: updateBoard,
    highlightChanges: highlightChanges,
  };
})();
