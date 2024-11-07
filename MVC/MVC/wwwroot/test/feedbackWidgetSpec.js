var myApp = {
  init: function () {
    return true;
  },
};

describe("FeedbackWidget", function () {
  let feedbackWidget;

  beforeAll(function (done) {
    // Add DOM elements dynamically for testing
    $("body").append(
      '<div id="feedback-widget" class="" role="alert" style="display: none;"></div>'
    );
    $("body").append(
      '<button id="succes" type="button" value="Fusion is near.">Good Luck</button>'
    );
    $("body").append(
      '<button id="hide" type="button" value="For the fallout...">Hide</button>'
    );
    $("body").append(
      '<button id="danger" type="button" value="Subcritical reactor!!!">Danger</button>'
    );

    feedbackWidget = new FeedbackWidget("feedback-widget");
    done();
  });

  afterEach(function () {
    // Reset display style and class after each test
    $("#feedback-widget").css("display", "none");
    $("#feedback-widget").attr("class", "");
    $("#feedback-widget").text("");
    feedbackWidget.removeLog(); // Clear logs between tests
  });

  // 1. DOM Element Check
  it("should define the feedback widget DOM element", function () {
    expect($("#feedback-widget")).toBeDefined();
  });

  // 2. Display the feedback widget with success alert
  it("should show the feedback widget with success alert", function () {
    feedbackWidget.show("Fusion is near.", "Good Luck");
    expect($("#feedback-widget").css("display")).toBe("block");
    expect($("#feedback-widget").attr("class")).toBe("alert alert-success");
    expect($("#feedback-widget").text()).toBe("Good Luck - Fusion is near.");
  });

  // 3. Display the feedback widget with danger alert
  it("should show the feedback widget with danger alert", function () {
    feedbackWidget.show("Subcritical reactor!!!", "Danger");
    expect($("#feedback-widget").css("display")).toBe("block");
    expect($("#feedback-widget").attr("class")).toBe("alert alert-danger");
    expect($("#feedback-widget").text()).toBe(
      "Danger - Subcritical reactor!!!"
    );
  });

  // 4. Hide the feedback widget (when show is called with empty params)
  it("should hide the feedback widget", function () {
    feedbackWidget.show("", ""); // Hide the widget
    expect($("#feedback-widget").css("display")).toBe("none");
    expect($("#feedback-widget").text()).toBe(""); // Widget text should be cleared
  });

  // 5. Logging functionality
  it("should log a message to localStorage", function () {
    feedbackWidget.show("Fusion is near.", "Good Luck");
    let storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData.messages.length).toBe(1);
    expect(storedData.messages[0].type).toBe("Good Luck");
    expect(storedData.messages[0].message).toBe("Fusion is near.");
  });

  // 6. Log only the 10 most recent messages
  it("should store only the last 10 messages in the log", function () {
    for (let i = 1; i <= 12; i++) {
      feedbackWidget.show("Message " + i, "Good Luck");
    }
    let storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData.messages.length).toBe(10);
    expect(storedData.messages[0].message).toBe("Message 12"); // Most recent
    expect(storedData.messages[9].message).toBe("Message 3"); // Oldest
  });

  // 7. Remove all logs from localStorage
  it("should clear all logs from localStorage when removeLog is called", function () {
    feedbackWidget.show("Fusion is near.", "Good Luck");
    feedbackWidget.removeLog();
    expect(localStorage.getItem("feedback_widget")).toBe(null);
  });

  // 8. Verify history output in console (requires console.log mock for accurate testing)
  it("should output history of messages in the console", function () {
    spyOn(console, "log");
    feedbackWidget.show("Fusion is near.", "Good Luck");
    feedbackWidget.show("Subcritical reactor!!!", "Danger");
    feedbackWidget.history();
    expect(console.log).toHaveBeenCalledWith(
      "Danger - Subcritical reactor!!! \nGood Luck - Fusion is near. \n"
    );
  });

  // 9. Preserve CSS display state after hiding (reset behavior)
  it("should reset widget display style to none after hiding", function () {
    feedbackWidget.show("", ""); // Call to hide
    expect($("#feedback-widget").css("display")).toBe("none");
  });

  // 10. Check alert class assignment on show
  it("should assign alert-reset class when reset", function () {
    feedbackWidget.show("", "");
    expect($("#feedback-widget").attr("class")).toBe("alert alert-reset");
  });

  // 11. Check that localStorage is not updated when hiding
  it("should not add to localStorage log when hiding the widget", function () {
    feedbackWidget.show("", ""); // Hide the widget
    let storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData).toEqual(null); // No log should be stored
  });
});
