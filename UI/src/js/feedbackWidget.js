class FeedbackWidget {
  constructor(elementId) {
    this._elementId = elementId;
    this._timeout = null;
  }

  get elementId() {
    return this._elementId;
  }

  show(message, type, autoDismiss = true, actions = []) {
    const widgetElement = $("#" + this._elementId);
    const closeButton = $('<button class="feedback-widget__close">×</button>');

    widgetElement.empty().append(closeButton).append(`<span>${message}</span>`);

    const actionsContainer = $('<div class="feedback-widget__actions"></div>');
    actions.forEach(action => {
      const iconElement = $(`<span>${action.icon}</span>`);
      iconElement.addClass(action.class);
      iconElement.on("click", action.callback);
      actionsContainer.append(iconElement);
    });

    widgetElement.append(actionsContainer);

    widgetElement.attr("class", `alert alert-${type.toLowerCase()}`);
    widgetElement.css("display", "block");

    widgetElement.css("display", "block");
    setTimeout(() => {
      widgetElement.css("opacity", "1");
    }, 10);

    if (autoDismiss) {
      if (this._timeout) clearTimeout(this._timeout);
      this._timeout = setTimeout(() => this.hide(), 6000);
    }

    closeButton.on("click", () => this.hide());

    this.log({ message, type });
  }

  hide() {
    const widgetElement = $("#" + this._elementId);

    widgetElement.addClass("hide-animation");

    setTimeout(() => {
      widgetElement
        .css("display", "none")
        .removeClass("hide-animation")
        .empty()
        .attr("class", "");
    }, 500);

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

const feedbackWidget = new FeedbackWidget("feedback-widget");

$(function () {
  $("#move-button").on("click", function () {
    feedbackWidget.show(
      "You have successfully made a move, wait on your opponent to make his.",
      "Success"
    );
    feedbackWidget.history();
  });

  $("#pass-button").on("click", function () {
    Game.Model.passGame()
    .then(() => {
      feedbackWidget.show("You have successfully passed your turn.", "Success");
      feedbackWidget.history();
    })
    .catch((error) => {
      feedbackWidget.show("Pass failed: " + error.responseText, "Danger");
    });
  });

  $("#forfeit-button").on("click", function () {
    feedbackWidget.show(
      "Are you sure you want to forfeit?",
      "info",
      true,
      [
        {
          icon: "✓",
          class: "feedback-icon feedback-icon--success",
          callback: () => {
            Game.Model.forfeitGame()
            .then(() => {
              feedbackWidget.show("You have forfeited the game.", "Success");
              feedbackWidget.removeLog();
            })
            .catch((error) => {
              feedbackWidget.show("Forfeit failed: " + error.responseText, "Danger");
            });
          },
        },
        {
          icon: "✕",
          class: "feedback-icon feedback-icon--danger",
          callback: () => {
            feedbackWidget.hide();
          },
        },
      ]
    );
  });

  function startWobble() {
    wobbleInterval = setInterval(() => {
      $("#move-button").addClass("wobble");
      setTimeout(() => {
        $("#move-button").removeClass("wobble");
      }, 500);
    }, 2000);
  }

  startWobble();
});
