class FeedbackWidget {
  constructor(elementId) {
    this._elementId = elementId;
    this._timeout = null;
  }

  get elementId() {
    //getter, set keyword voor setter methode
    return this._elementId;
  }

  show(message, type, autoDismiss = true) {
    const widgetElement = $("#" + this._elementId);
    const closeButton = $('<button class="feedback-widget__close">×</button>');

    // Clear previous message and close button
    widgetElement
      .empty()
      .append(closeButton)
      .append(`<span>${type} - ${message}</span>`);
    widgetElement.attr("class", `alert alert-${type.toLowerCase()}`);
    widgetElement.css("display", "block");

    // Handle auto-dismiss after 15 seconds or clear any prior timeouts
    if (autoDismiss) {
      if (this._timeout) clearTimeout(this._timeout);
      this._timeout = setTimeout(() => this.hide(), 15000);
    }

    // Attach close button click event to hide the widget
    closeButton.on("click", () => this.hide());

    // Log the message
    this.log({ message, type });
  }

  // Hide the feedback widget
  hide() {
    $("#" + this._elementId)
      .css("display", "none")
      .empty()
      .attr("class", "");
    if (this._timeout) clearTimeout(this._timeout);
  }

  log(message) {
    const logData = JSON.parse(localStorage.getItem("feedback_widget")) || {
      messages: [],
    };
    logData.messages.unshift(message);
    if (logData.messages.length > 10) logData.messages.pop();
    localStorage.setItem("feedback_widget", JSON.stringify(logData));
  }

  removeLog() {
    localStorage.removeItem("feedback_widget");
    console.clear();
  }

  history() {
    const storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    let history = storedData?.messages.map((msg) => `${msg.type} - ${msg.message}`).join("\n") || "No history.";
    console.clear();
    console.log(history);
  }
}

$(function () {
    const feedbackWidget = new FeedbackWidget("feedback-widget");

    $("#move-button").on("click", function () {
      feedbackWidget.show("You have made a move, wait on your opponent to make his.", "Success");
      feedbackWidget.history();
    });
  
    $("#move-button-fail").on("click", function () {
      feedbackWidget.show("You can't make a move.", "Danger");
      feedbackWidget.history();
    });
  
    $("#pass-button").on("click", function () {
      feedbackWidget.show("You have successfully passed your turn, wait on your opponent to make a move.", "Success");
      feedbackWidget.history();
    });
  
    $("#pass-button-fail").on("click", function () {
      feedbackWidget.show("You can't pass your turn.", "Danger");
      feedbackWidget.history();
    });
  
    $("#forfeit-button").on("click", function () {
      feedbackWidget.show("You have successfully forfeited the game.", "Success");
      feedbackWidget.history();
    });
  
    $("#forfeit-button-fail").on("click", function () {
      feedbackWidget.show("It's not your turn, you can't forfeit yet. Wait on your opponent to make his move.", "Danger");
      feedbackWidget.history();
    });
});
