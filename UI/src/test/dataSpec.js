var myApp = {
  init: function () {
    return true;
  },
};

describe("Game.Data Module", function () {
  let mockConfig;

  beforeAll(function () {
    mockConfig = {
      apiUrl: "https://mock-api-url/",
      userToken: "mock-token",
    };

    Game.Data.init(mockConfig.apiUrl, mockConfig.userToken, "development");
  });

  beforeEach(function () {
    Game.Data.setMockData([
      {
        url: "api/game/turn/<token>",
        data: 0,
      },
    ]);

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
    expect(Game.Data).toBeDefined();
  });

  it("should have required methods", function () {
    expect(Game.Data.get).toBeDefined();
    expect(Game.Data.getPartial).toBeDefined();
    expect(Game.Data.sendMove).toBeDefined();
    expect(Game.Data.passGame).toBeDefined();
    expect(Game.Data.forfeitGame).toBeDefined();
    expect(Game.Data.rematchGame).toBeDefined();
    expect(Game.Data.acceptGame).toBeDefined();
    expect(Game.Data.declineGame).toBeDefined();
  });

  it("should return mock data in development environment", function (done) {
    Game.Data.get().then(function (data) {
      expect(data).toEqual([
        {
          url: "api/game/turn/<token>",
          data: 0,
        },
      ]);
      done();
    });
  });

  it("should throw an error for unknown environment", function () {
    expect(function () {
      Game.Data.init("https://www.s1164087/game/", "mock-token", "staging");
    }).toThrowError("This environment is unknown.");
  });

  it("should set and get mock data correctly", function (done) {
    const mockData = { test: "value" };
    Game.Data.setMockData(mockData);

    Game.Data.get().then(function (data) {
      expect(data).toEqual(mockData);
      done();
    });
  });

  it("should handle API calls correctly in production", function (done) {
    Game.Data.init(mockConfig.apiUrl, mockConfig.userToken, "production");

    spyOn($, "ajax").and.returnValue(Promise.resolve({ success: true }));

    Game.Data.sendMove(3, 2).then(function (response) {
      expect($.ajax).toHaveBeenCalledWith({
        url: mockConfig.apiUrl + "game/move",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
          playerToken: mockConfig.userToken,
          row: 3,
          column: 2,
        }),
      });
      expect(response).toEqual({ success: true });
      done();
    });
  });
});
