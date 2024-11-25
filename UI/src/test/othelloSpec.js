var myApp = {
  init: function () {
    return true;
  },
};

describe("Game.Othello Module", function () {
  beforeAll(function () {
    $("body").append(`
      <div id="game-board-container"></div>
      <span id="white-score"></span>
      <span id="black-score"></span>
    `);
  });

  beforeEach(function () {
    if (!window.spa_templates) {
      window.spa_templates = {};
    }
    window.spa_templates["body"] = jasmine.createSpy("body").and.returnValue(`
          <section id="player-info" class="player-info">
            <h2>
              <span id="game-status">Playing</span> against
              <span id="opponent-name">...</span>
              <span id="forfeit-title"></span>
            </h2>
          </section>
      
          <section id="score-display" class="score-display">
            <p>
              <span id="player-color-indicator" class="color-indicator">
                <strong><span id="player-score">2</span></strong>
              </span>
            </p>
            <p>
              <span id="timer-color-indicator" class="color-indicator">
                <strong><span id="time-remaining">30</span></strong>
              </span>
            </p>
            <p>
              <span id="opponent-color-indicator" class="color-indicator">
                <strong><span id="opponent-score">2</span></strong>
              </span>
            </p>
          </section>
      
          <section id="game-board-container"></section>
      
          <section id="button-container">
            <button id="pass-button" class="button button--success">Pass</button>
            <button id="forfeit-button" class="button button--danger">Forfeit</button>
            <button id="rematch-button" class="button button--info hidden">
              <i class="fas fa-redo"></i><span>Rematch</span>
            </button>
          </section>
      
          <section id="feedback-section" aria-label="Feedback Widget">
            <article id="feedback-widget" role="alert"></article>
          </section>
        `);

    document.getElementById("body").innerHTML = spa_templates["body"]();
  });

  afterEach(function () {
    document.getElementById("body").innerHTML = "";
    $("#game-board-container").empty();
  });

  it("should be defined", function () {
    expect(Game.Othello).toBeDefined();
  });

  it("should have a board function", function () {
    expect(Game.Othello.board).toBeDefined();
    expect(typeof Game.Othello.board).toBe("function");
  });

  it("should render the game board", function () {
    Game.Othello.board();

    const table = $("#game-board-container table");
    expect(table.length).toBe(1);

    const cells = $("#game-board-container td");
    expect(cells.length).toBe(64);
  });

  it("should update the board with pieces", function () {
    Game.Othello.board();

    const mockBoard = Array(8)
      .fill(null)
      .map(() => Array(8).fill(0));
    mockBoard[3][3] = 1;
    mockBoard[3][4] = 2;

    Game.Othello.updateBoard(mockBoard, 1, 1);

    const whitePieces = $(".white-piece");
    const blackPieces = $(".black-piece");

    expect(whitePieces.length).toBe(1);
    expect(blackPieces.length).toBe(1);
  });

  it("should calculate scores correctly", function () {
    Game.Othello.board();

    const mockBoard = Array(8)
      .fill(null)
      .map(() => Array(8).fill(0));
    mockBoard[3][3] = 1;
    mockBoard[3][4] = 2;
    mockBoard[4][4] = 1;

    Game.Othello.updateBoard(mockBoard, 1, 1);

    expect($("#white-score").text()).toBe("2");
    expect($("#black-score").text()).toBe("1");
  });
});