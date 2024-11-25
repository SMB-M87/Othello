var myApp = {
  init: function () {
    return true;
  },
};

describe("Game Module", function () {
  let mockConfig;
  let feedbackWidget;
  let originalConfig;

  beforeAll(function () {
    // Save the original config to restore later
    originalConfig = window.config;

    // Mock configuration for testing
    mockConfig = {
      apiUrl: "https://mock-api-url/",
      userToken: "mock-token",
      redirectUrl: "https://mock-redirect-url/",
    };

    // Mock the global config object
    window.config = mockConfig;

    // Mock DOM elements required for the Game module
    $("body").append(`
      <div id="body"></div>
      <div id="game-board-container"></div>
      <button id="pass-button"></button>
      <button id="forfeit-button"></button>
      <button id="rematch-button"></button>
      <div id="feedback-widget"></div>
    `);

    window.spa_templates = {
      body: jasmine.createSpy("body").and.returnValue("<div>Mock Template</div>"),
    };

    feedbackWidget = {
      removeLog: jasmine.createSpy("removeLog"),
      log: jasmine.createSpy("log"),
      show: jasmine.createSpy("show"),
      hide: jasmine.createSpy("hide"),
    };
    spyOn(FeedbackSingleton, "getInstance").and.returnValue(feedbackWidget);

    spyOn(Game.Data, "init").and.callFake(() => {});
    spyOn(Game.Data, "sendMove").and.returnValue(Promise.resolve());
    spyOn(Game.Data, "passGame").and.returnValue(Promise.resolve());
    spyOn(Game.Data, "forfeitGame").and.returnValue(Promise.resolve());
    spyOn(Game.Data, "rematchGame").and.returnValue(Promise.resolve());
    spyOn(Game.Model, "getGameState").and.callFake(() => {});
    spyOn(Game.Othello, "board").and.callFake(() => {});
  });

  afterEach(function () {
    // Clear DOM and localStorage after each test
    $("#body").empty();
    $("#game-board-container").empty();
    $("#pass-button").off();
    $("#forfeit-button").off();
    $("#rematch-button").off();
    $("#feedback-widget").empty();
    localStorage.clear();
  });

  afterAll(function () {
    // Restore the original config
    window.config = originalConfig;
  });

  it("should throw an error if config is missing required properties", function () {
    expect(function () {
      Game({});
    }).toThrowError("Game module initialization failed: Missing config properties.");
  });

  it("should initialize correctly with valid config", function (done) {
    // Initialize Game module with mock config
    Game.init(() => {
      expect(Game.Data.init).toHaveBeenCalledWith(
        mockConfig.apiUrl,
        mockConfig.userToken,
        "production"
      );
      expect(Game.Othello.board).toHaveBeenCalled();
      expect(Game.Model.getGameState).toHaveBeenCalled();
      done();
    });
  });

  it("should render the template and set the body content", function () {
    Game.init();

    expect(window.spa_templates.body).toHaveBeenCalled();
    expect($("#body").html()).toBe("<div>Mock Template</div>");
    const confirmCallback = feedbackWidget.show.calls.mostRecent().args[4][0].callback;
    confirmCallback();
    expect(Game.Data.forfeitGame).toHaveBeenCalled();

    // Simulate rematch
    $("#rematch-button").click();
    expect(Game.Data.rematchGame).toHaveBeenCalled();
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
  });

  it("should handle errors and log messages correctly", function (done) {
    Game.Data.sendMove.and.returnValue(Promise.reject({ responseText: "Mock error" }));

    Game.init();

    $("#game-board-container").append('<td data-row="3" data-col="2"><div class="possible-move"></div></td>');
    $(".possible-move").click();

    setTimeout(() => {
      expect(feedbackWidget.log).toHaveBeenCalledWith({
        message: "Move failed: Mock error",
        type: "Danger",
      });
      done();
    }, 100);
  });

  it("should update the game state at intervals", function (done) {
    spyOn(window, "setInterval").and.callThrough();

    Game.init();

    expect(setInterval).toHaveBeenCalledWith(jasmine.any(Function), 1000);
    done();
  });
});