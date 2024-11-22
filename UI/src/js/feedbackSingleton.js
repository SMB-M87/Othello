const FeedbackSingleton = (function () {
  let instance;
  return {
    getInstance: function () {
      if (!instance) {
        instance = new FeedbackWidget("feedback-widget");
      }
      return instance;
    },
  };
})();
