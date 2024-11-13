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

    // Display the widget and start the fade-in effect
    widgetElement.css("display", "block");
    setTimeout(() => {
      widgetElement.css("opacity", "1");
    }, 10); // Slight delay to trigger CSS transition

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
    const widgetElement = $("#" + this._elementId);

    // Add the hide-animation class to start the slide-up animation
    widgetElement.addClass("hide-animation");

    // After the animation ends, hide the widget completely
    setTimeout(() => {
      widgetElement
        .css("display", "none")
        .removeClass("hide-animation")
        .empty()
        .attr("class", "");
    }, 500); // Match this duration with the CSS animation duration

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
    let history =
      storedData?.messages
        .map((msg) => `${msg.type} - ${msg.message}`)
        .join("\n") || "No history.";
    console.clear();
    console.log(history);
  }
}

$(function () {
  const feedbackWidget = new FeedbackWidget("feedback-widget");
  let wobbleInterval;

  $("#move-button").on("click", function () {
    feedbackWidget.show(
      "You have successfully made a move, wait on your opponent to make his.",
      "Success"
    );
    feedbackWidget.history();
    resetWobble();
  });

  $("#pass-button").on("click", function () {
    feedbackWidget.show(
      "You have successfully passed your turn, wait on your opponent to make a move.",
      "Success"
    );
    feedbackWidget.history();
    resetWobble();
  });

  $("#forfeit-button").on("click", function () {
    feedbackWidget.show("You have successfully forfeited the game.", "Success");
    feedbackWidget.history();
    resetWobble();
  });

  // Start wobble animation after 2 seconds
  function startWobble() {
    wobbleInterval = setInterval(() => {
      $("#move-button").addClass("wobble");
      setTimeout(() => {
        $("#move-button").removeClass("wobble");
      }, 500); // Duration of the wobble animation
    }, 2000); // Repeat every 2 seconds
  }

  // Reset wobble animation
  function resetWobble() {
    clearInterval(wobbleInterval);
    $("#move-button").removeClass("wobble");
    setTimeout(startWobble, 2000); // Restart after 2 seconds of inactivity
  }
  
  // Start the wobble animation after initial 2 seconds
  setTimeout(startWobble, 2000);
});
