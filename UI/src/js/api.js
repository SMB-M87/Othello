Game.Api = (function () {
  let configMap = {
    apiUrl: null,
  };

  // private functions
  const _init = function () {
    configMap.apiUrl = "https://geek-jokes.sameerkumar.website/api?format=json";
    _get();
  };

  const _get = function () {
    $.get(configMap.apiUrl)
      .then((result) => {
        _set(result);
      })
      .catch((e) => {
        console.log(e.message);
      });
  };

  const _set = function (result) {
    const joke = document.getElementById("joke-content");

    if (joke) {
      if (result && result.joke) {
        joke.textContent = result.joke;
      } else if (typeof result === "string") {
        joke.textContent = result;
      } else {
        joke.textContent = "No joke available at the moment!";
      }
    }
  };

  // public functions
  const init = () => {
    _init();
  };

  const get = () => {
    _get();
  };

  // return object
  return {
    init: init,
    get: get,
  };
})();
