var myApp = {
  init: function () {
    return true;
  },
};

describe("Game.Model Module", function () {
  let feedbackWidget;

  beforeAll(function () {
    feedbackWidget = {
      show: jasmine.createSpy("show"),
    };
    spyOn(FeedbackSingleton, "getInstance").and.returnValue(feedbackWidget);
  });

  beforeEach(function () {
    Game.Model = (function () {
      return {
        getGameState: getGameState,
        setBoard: setBoard,
        getBoard: getBoard,
        getOpponent: getOpponent,
      };
    })();

    spyOn(Game.Data, "get").and.returnValue(
      Promise.resolve({
        opponent: "OpponentName",
        color: 1,
        partial: {
          time: "10",
          playersturn: 1,
          board: [
          ],
        },
      })
    );
    spyOn(Game.Data, "getPartial").and.returnValue(
      Promise.resolve({
        playersTurn: 1,
        time: "10",
        board: [
        ],
      })
    );
    spyOn(Game.Data, "getRematch").and.returnValue(Promise.resolve(null));
    spyOn(Game.Data, "getResult").and.returnValue(
      Promise.resolve({
        winner: "Player",
        draw: false,
        forfeit: false,
      })
    );    
    
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
  });

  it("should be defined", function () {
    expect(Game.Model).toBeDefined();
  });

  it("should have a getGameState function", function () {
    expect(Game.Model.getGameState).toBeDefined();
    expect(typeof Game.Model.getGameState).toBe("function");
  });

  it("should handle first load correctly", function (done) {
    spyOn(Game.Othello, "updateBoard").and.callFake(() => {});
    spyOn(document, "getElementById").and.returnValue({
      textContent: "",
      classList: {
        add: jasmine.createSpy("add"),
        remove: jasmine.createSpy("remove"),
      },
    });

    Game.Model.getGameState().then(function () {
      expect(Game.Othello.updateBoard).toHaveBeenCalled();
      expect(Game.Model.getOpponent()).toBe("OpponentName");
      done();
    });
  });

  it("should handle game finish correctly", function (done) {
    Game.Model.getGameState().then(function () {
      Game.Model.getGameState().then(function () {
        expect(document.getElementById("game-status").textContent).toBe("Won");
        done();
      });
    });
  });

  it("should show rematch request when appropriate", function (done) {
    Game.Data.getRematch.and.returnValue(Promise.resolve(true));

    Game.Model.getGameState().then(function () {
      expect(feedbackWidget.show).toHaveBeenCalled();
      done();
    });
  });
});