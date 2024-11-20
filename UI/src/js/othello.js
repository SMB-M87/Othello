Game.Othello = (function () {
  // config
  let configMap = {
    apiUrl: "",
  };

  // private functions
  const _init = function () {
    console.log("Othello module started from url: " + configMap.apiUrl);
  };

  const _updateBoard = function (boardData) {
    const boardContainer = document.getElementById('game-board-container');
    boardContainer.innerHTML = '';

    const table = document.createElement('table');
    table.className = 'othello-board';

    for (let row = 0; row < 8; row++) {
      const tr = document.createElement('tr');
      for (let col = 0; col < 8; col++) {
        const td = document.createElement('td');
        td.className = 'board-cell';
        td.dataset.row = row;
        td.dataset.col = col;

        td.addEventListener('click', function () {
          cellClicked(row, col);
        });

        const cellValue = boardData[row][col];
        if (cellValue === 1) {
          const piece = document.createElement('div');
          piece.className = 'white-piece';
          td.appendChild(piece);
        } else if (cellValue === 2) {
          const piece = document.createElement('div');
          piece.className = 'black-piece';
          td.appendChild(piece);
        } else if (cellValue === 3) {
          td.classList.add('possible-move');
        }

        tr.appendChild(td);
      }
      table.appendChild(tr);
    }

    boardContainer.appendChild(table);
  };

  const cellClicked = function (row, col) {
    Game.Model.sendMove(row, col);
  };  

  // public functions
  const init = (url) => {
    configMap.apiUrl = url;
    _init();
  };

  const updateBoard = function (boardData) {
    _updateBoard(boardData);
  };

  // return object
  return {
    init: init,
    updateBoard: updateBoard
  };
})();
