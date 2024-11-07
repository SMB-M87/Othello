var myApp = {
  init: function () {
    return true;
  },
};

describe("Game Module", function () {
  beforeAll(function (done) {
    // Wait for DOM ready if necessary
    $(function () {
      done();
    });
  });

  beforeEach(function () {
    // Reset mock data to default before each test
    Game.Data.setMockData([
      {
        url: "api/game/turn/<token>",
        data: 0,
      },
    ]);
  });

  it("should be defined", function () {
    expect(Game).toBeDefined();
  });

  it("should have an init function", function () {
    expect(Game.init).toBeDefined();
    expect(typeof Game.init).toBe("function");
  });

  it("should initialize correctly", function (done) {
    // Spy on console.log to check initialization messages
    spyOn(console, "log");

    // Initialize Game module
    Game.init(function () {
      expect(console.log).toHaveBeenCalledWith("Game module started from url: https://www.s1164087/api/game/");
      expect(console.log).toHaveBeenCalledWith("Data module started from url: https://www.s1164087/api/game/data");
      expect(console.log).toHaveBeenCalledWith("Model module started from url: https://www.s1164087/api/game/model");
      expect(console.log).toHaveBeenCalledWith("Othello module started from url: https://www.s1164087/api/game/othello");
      done();
    });
  });
});
