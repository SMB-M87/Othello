class FeedbackWidget {
  constructor(elementId) {
    console.log("Initializing FeedbackWidget with elementId:", elementId);
    this._elementId = elementId;
    this._timeout = null;
  }

  get elementId() {
    return this._elementId;
  }

  show(message, type, autoDismiss = true, actions = []) {
    const widgetElement = $("#" + this._elementId);
    const feedbackSection = $("#feedback-section");
    const closeButton = $('<button class="feedback-widget__close">×</button>');

    feedbackSection.addClass("flex").removeClass("hidden");
    widgetElement.empty().append(closeButton).append(`<span>${message}</span>`);

    const actionsContainer = $('<div class="feedback-widget__actions"></div>');
    actions.forEach((action) => {
      const iconElement = $(`<span>${action.icon}</span>`);
      iconElement.addClass(action.class);
      iconElement.on("click", action.callback);
      actionsContainer.append(iconElement);
    });
    widgetElement.append(actionsContainer);

    widgetElement
      .removeClass("hidden fade-out")
      .addClass(`visible alert alert-${type.toLowerCase()} fade-in`);

    if (autoDismiss) {
      if (this._timeout) clearTimeout(this._timeout);
      this._timeout = setTimeout(() => this.hide(), 6000);
    }

    closeButton.on("click", () => this.hide());
    this.log({ message, type });
  }

  hide() {
    const widgetElement = $("#" + this._elementId);
    const feedbackSection = $("#feedback-section");

    widgetElement.removeClass("fade-in").addClass("fade-out");

    setTimeout(() => {
      widgetElement.addClass("hidden").removeClass("visible fade-out").empty();
      feedbackSection.addClass("hidden").removeClass("flex");
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

$(function () {
  const feedbackWidget = FeedbackSingleton.getInstance();

  $("#game-board-container").on("click", ".possible-move", (event) => {
    const cell = $(event.target).closest("td");
    const row = cell.data("row");
    const col = cell.data("col");

    Game.Data.sendMove(row, col)
      .then(() => {
        feedbackWidget.log({
          message: `Move made on row ${row} and col ${col}.`,
          type: "Success",
        });
        feedbackWidget.history();
      })
      .catch((error) => {
        feedbackWidget.log({
          message: `Move failed: ${error.responseText || error}`,
          type: "Danger",
        });
      });
  });

  $("#pass-button").on("click", function () {
    Game.Data.passGame()
      .then(() => {
        feedbackWidget.log({ message: `Turn passed.`, type: "Success" });
        feedbackWidget.history();
      })
      .catch((error) => {
        feedbackWidget.log({
          message: "Pass failed: " + error.responseText,
          type: "Danger",
        });
      });
  });

  $("#forfeit-button").on("click", function () {
    feedbackWidget.show("Are you sure you want to forfeit?", "info", true, [
      {
        icon: "✓",
        class: "feedback-icon feedback-icon--success",
        callback: () => {
          Game.Data.forfeitGame()
            .then(() => {
              feedbackWidget.removeLog();
              feedbackWidget.hide();
            })
            .catch((error) => {
              feedbackWidget.log(
                "Forfeit failed: " + error.responseText,
                "Danger"
              );
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
    ]);
  });
});
