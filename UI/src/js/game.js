const Game = (function (config) {
  let configMap = { 
    apiUrl: config.apiUrl,
    apiKey: config.userToken,
    redirectUrl: config.redirectUrl
  };

  // private functions
  const _init = function () {
    _template();
    _eventListener();
    Game.Data.init(configMap.apiUrl, configMap.apiKey, "production");
    Game.Model.init(configMap.redirectUrl);
  };

  const _template = function () {
    if (!spa_templates || !spa_templates["body"]) {
      throw new Error("Template 'body' is not available. Ensure the template is compiled and loaded.");
    }

    const html = spa_templates["body"]();
    document.getElementById("body").innerHTML = html;
  };

  const _eventListener = () => {
    const feedbackWidget = FeedbackSingleton.getInstance();
    feedbackWidget.removeLog();
  
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
  };  

  const _getCurrentGameState = function () {
    Game.Othello.board();

    setInterval(function () {
      Game.Model.getGameState();
    }, 1000);
  };

  // public functions
  const init = (callback) => {
    _init();
    _getCurrentGameState();
    if (callback) callback();
  };

  // return object
  return {
    init: init
  };
})({ apiUrl:"https://localhost:7023/api/game/", userToken: "test", redirectUrl: "https://localhost:7269/Home/Result" });

$(() => {
  function afterInit() {}
  Game.init(afterInit);
});
