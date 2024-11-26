class FeedbackWidget {
  constructor(elementId) {
    this._elementId = elementId;
    this._timeout = null;
  }

  get elementId() {
    return this._elementId;
  }

  show(message, type, time = 6000, autoDismiss = true, actions = [], contentElement = null) {
    const widgetElement = $("#" + this._elementId);
    const feedbackSection = $("#feedback-section");
    const closeButton = $('<button class="feedback-widget__close">×</button>');

    feedbackSection.addClass("flex").removeClass("hidden");
    widgetElement.removeClass("hidden fade-out feedbackcanvas");
    widgetElement.empty().append(closeButton);

    if (message) {
      widgetElement.append(`<span>${message}</span>`);
    }

    if (contentElement) {
      widgetElement.append(contentElement);
      widgetElement.addClass("feedbackcanvas");
    }

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
      this._timeout = setTimeout(() => this.hide(), time);
    }

    closeButton.on("click", () => this.hide());
    this.log({ message, type });
  }

  hide() {
    const widgetElement = $("#" + this._elementId);
    const feedbackSection = $("#feedback-section");

    widgetElement.removeClass("fade-in", "feedbackcanvas").addClass("fade-out");

    setTimeout(() => {
      widgetElement.addClass("hidden").removeClass("visible fade-out feedbackcanvas").empty();
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
  }

  history() {
    const storedData = JSON.parse(localStorage.getItem("feedback_widget"));
    let history =
      storedData?.messages
        .map((msg) => `${msg.type} - ${msg.message}`)
        .join("\n") || "No history.";
    console.log(history);
  }
}
