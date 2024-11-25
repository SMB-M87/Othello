var myApp = {
  init: function () {
    return true;
  },
};

jasmine.DEFAULT_TIMEOUT_INTERVAL = 8000; // Increase timeout to 20 seconds

describe("FeedbackWidget", function () {
  let feedbackWidget;

  beforeAll(function () {
    // Spy on console.log and dynamically add DOM elements for testing
    spyOn(console, "log").and.callThrough();
    $("body").append(`
      <div id="feedback-section" class="hidden"></div>
      <div id="feedback-widget" role="alert"></div>
    `);
    feedbackWidget = new FeedbackWidget("feedback-widget");
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
    feedbackWidget.hide();
    localStorage.removeItem("feedback_widget");
  });

  it("should define the feedback widget DOM element", function () {
    expect($("#feedback-widget").length).toBe(1);
  });

  it("should display the widget with a success alert and auto-dismiss", function (done) {
    feedbackWidget.show("Fusion is near.", "Success", 1000);
    expect($("#feedback-widget").hasClass("alert-success")).toBe(true);
    expect($("#feedback-widget").text()).toContain("Fusion is near.");

    setTimeout(() => {
      try {
        expect($("#feedback-widget").hasClass("visible")).toBe(false);
        done();
      } catch (error) {
        done.fail(error);
      }
    }, 1500);
  });

  it("should display the widget with a danger alert", function () {
    feedbackWidget.show("Critical error!", "Danger", 6000, false);
    expect($("#feedback-widget").hasClass("alert-danger")).toBe(true);
    expect($("#feedback-widget").text()).toContain("Critical error!");
  });

  it("should hide the widget when the close button is clicked", function (done) {
    feedbackWidget.show("Close test", "Info");
    $(".feedback-widget__close").trigger("click");

    setTimeout(() => {
      try {
        expect($("#feedback-widget").hasClass("visible")).toBe(false);
        done();
      } catch (error) {
        done.fail(error);
      }
    }, 500);
  });

  it("should log a message to localStorage", function () {
    feedbackWidget.show("Fusion is near.", "Success");
    const storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData.messages.length).toBe(1);
    expect(storedData.messages[0]).toEqual({
      message: "Fusion is near.",
      type: "Success",
    });
  });

  it("should store only the last 10 messages in the log", function () {
    for (let i = 1; i <= 12; i++) {
      feedbackWidget.show(`Message ${i}`, "Info");
    }
    const storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData.messages.length).toBe(10);
    expect(storedData.messages[0].message).toBe("Message 12");
    expect(storedData.messages[9].message).toBe("Message 3");
  });

  it("should clear all logs from localStorage when removeLog is called", function () {
    feedbackWidget.show("Fusion is near.", "Success");
    feedbackWidget.removeLog();
    expect(localStorage.getItem("feedback_widget")).toBeNull();
  });

  it("should output the history of messages in the console", function () {
    feedbackWidget.show("Fusion is near.", "Success");
    feedbackWidget.show("Critical error!", "Danger");
    feedbackWidget.history();

    const expectedHistory = [
      "Danger - Critical error!",
      "Success - Fusion is near.",
    ].join("\n");

    expect(console.log).toHaveBeenCalledWith(expectedHistory);
  });

  it("should assign appropriate alert classes based on the alert type", function () {
    feedbackWidget.show("Test Info", "Info");
    expect($("#feedback-widget").hasClass("alert-info")).toBe(true);

    feedbackWidget.show("Test Warning", "Warning");
    expect($("#feedback-widget").hasClass("alert-warning")).toBe(true);
  });

  it("should not update localStorage when hiding the widget", function () {
    feedbackWidget.hide();
    const storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData).toBeNull();
  });
});