Game.Othello = (function () {
  // config
  let configMap = {
    apiUrl: "",
  };

  // private functions
  const _init = function () {
    console.log("Othello module started from url: " + configMap.apiUrl);
  };

  // public functions
  const init = (url) => {
    configMap.apiUrl = url;
    _init();
  };

  // return object
  return {
    init: init,
  };
})();
