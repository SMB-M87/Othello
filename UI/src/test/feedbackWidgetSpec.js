var myApp = {
  init: function () {
    return true;
  },
};

jasmine.DEFAULT_TIMEOUT_INTERVAL = 8000; // Increase timeout to 20 seconds

describe("FeedbackWidget", function () {
  let feedbackWidget;

  beforeAll(function () {
    // Spy on window.alert to suppress actual alerts in tests
    spyOn(window, "alert").and.stub();

    // Add DOM elements dynamically for testing
    $("body").append('<div id="feedback-widget" role="alert"></div>');
    feedbackWidget = new FeedbackWidget("feedback-widget");
  });

  afterEach(function () {
    // Reset display style and content after each test
    feedbackWidget.hide();
    localStorage.removeItem("feedback_widget"); // Clear logs between tests
  });

  // 1. DOM Element Check
  it("should define the feedback widget DOM element", function () {
    expect($("#feedback-widget")).toBeDefined();
  });

  // 2. Show the feedback widget with success alert and auto-dismiss
  it("should display the feedback widget with a success alert and auto-dismiss", function (done) {
    feedbackWidget.show("Fusion is near.", "Success", true);
    expect($("#feedback-widget").css("display")).toBe("block");
    expect($("#feedback-widget").hasClass("alert-success")).toBe(true);
    expect($("#feedback-widget span").text()).toBe("Success - Fusion is near.");

    setTimeout(() => {
      try {
        expect($("#feedback-widget").css("display")).toBe("none");
        done(); // Signal completion of the test
      } catch (error) {
        done.fail(error); // Fail the test if an error occurs
      }
    }, 7000); // 15 seconds + a small buffer
  });

  // 3. Display the feedback widget with a danger alert
  it("should show the feedback widget with a danger alert", function () {
    feedbackWidget.show("Subcritical reactor!!!", "Danger");
    expect($("#feedback-widget").css("display")).toBe("block");
    expect($("#feedback-widget").hasClass("alert-danger")).toBe(true);
    expect($("#feedback-widget span").text()).toBe("Danger - Subcritical reactor!!!");
  });

  // 4. Hide the feedback widget when close button is clicked
  it("should hide the feedback widget when the close button is clicked", function (done) {
    feedbackWidget.show("Some message", "Info");
    $(".feedback-widget__close").trigger("click");

    setTimeout(() => {
      try {
        expect($("#feedback-widget").css("display")).toBe("none");
        done(); // Signal completion of the test
      } catch (error) {
        done.fail(error); // Fail the test if an error occurs
      }
    }, 2000);
  });

  // 5. Log messages to localStorage
  it("should log a message to localStorage", function () {
    feedbackWidget.show("Fusion is near.", "Success");
    let storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData.messages.length).toBe(1);
    expect(storedData.messages[0].type).toBe("Success");
    expect(storedData.messages[0].message).toBe("Fusion is near.");
  });

  // 6. Log only the 10 most recent messages
  it("should store only the last 10 messages in the log", function () {
    for (let i = 1; i <= 12; i++) {
      feedbackWidget.show("Message " + i, "Success");
    }
    let storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData.messages.length).toBe(10);
    expect(storedData.messages[0].message).toBe("Message 12"); // Most recent
    expect(storedData.messages[9].message).toBe("Message 3"); // Oldest
  });

  // 7. Remove all logs from localStorage
  it("should clear all logs from localStorage when removeLog is called", function () {
    feedbackWidget.show("Fusion is near.", "Success");
    feedbackWidget.removeLog();
    expect(localStorage.getItem("feedback_widget")).toBe(null);
  });

  // 8. Verify history output in console
  it("should output the history of messages in the console", function () {
    spyOn(console, "log");
    feedbackWidget.show("Fusion is near.", "Success");
    feedbackWidget.show("Subcritical reactor!!!", "Danger");
    feedbackWidget.history();

    // Use an array comparison for flexibility in line breaks
    const expectedHistory = [
      "Danger - Subcritical reactor!!!",
      "Success - Fusion is near.",
    ].join("\n");

    expect(console.log).toHaveBeenCalledWith(expectedHistory);
  });

  // 9. Check alert class assignment on show
  it("should assign appropriate alert classes based on the alert type", function () {
    feedbackWidget.show("Testing alert types.", "Info");
    expect($("#feedback-widget").hasClass("alert-info")).toBe(true);

    feedbackWidget.show("Testing alert types.", "Warning");
    expect($("#feedback-widget").hasClass("alert-warning")).toBe(true);
  });

  // 10. Check localStorage is not updated when hiding
  it("should not add to localStorage log when hiding the widget", function () {
    feedbackWidget.hide(); // Hide the widget
    let storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    expect(storedData).toEqual(null); // No log should be stored
  });
});
