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
        td.className = "board-cell";
        td.dataset.row = row;
        td.dataset.col = col;

        td.style.backgroundColor =
          (row + col) % 2 === 0 ? "lawngreen" : "limegreen";

        const cellValue = board[row][col];
        const cellDiv = document.createElement("div");
        cellDiv.style.display = "flex";
        cellDiv.style.justifyContent = "center";
        cellDiv.style.alignItems = "center";
        cellDiv.style.width = "100%";
        cellDiv.style.height = "100%";

        if (cellValue === 1) {
          const piece = document.createElement("i");
          piece.className = "fa fa-circle white-piece";
          piece.style.fontSize = "30px";
          piece.style.color = "white";
          cellDiv.appendChild(piece);
        } else if (cellValue === 2) {
          const piece = document.createElement("i");
          piece.className = "fa fa-circle black-piece";
          piece.style.fontSize = "30px";
          piece.style.color = "black";
          cellDiv.appendChild(piece);
        } else if (cellValue === 3) {
          if (isPlayersTurn) {
            const moveIndicator = document.createElement("div");
            moveIndicator.className = "possible-move";
            moveIndicator.style.border = `2px solid ${
              playerColor === 1 ? "white" : "black"
            }`;
            moveIndicator.style.borderRadius = "50%";
            moveIndicator.style.backgroundColor = "transparent";
            moveIndicator.style.boxShadow = "0 0 5px rgba(0, 0, 0, 0.5)";
            cellDiv.appendChild(moveIndicator);
            td.classList.add("wobble");
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

  // public functions
  const updateBoard = function (board, isPlayersTurn, playerColor) {
    _updateBoard(board, isPlayersTurn, playerColor);
  };

  // return object
  return {
    updateBoard: updateBoard
  };
})();
