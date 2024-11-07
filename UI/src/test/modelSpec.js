var myApp = {
  init: function () {
    return true;
  },
};

describe("Game.Model Module", function () {
  beforeAll(function () {
    // Initialize dependencies
    Game.Data.init("https://www.s1164087/game/", "development");
    Game.Model.init("https://www.s1164087/game/");
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
    expect(Game.Model).toBeDefined();
  });

  it("should have a getGameState function", function () {
    expect(Game.Model.getGameState).toBeDefined();
    expect(typeof Game.Model.getGameState).toBe("function");
  });

  it("should return the correct game state", function (done) {
    spyOn(console, "log");

    Game.Model.getGameState().then(function (data) {
      expect(data).toBeDefined();
      expect(data.data).toBe(0);
      expect(console.log).toHaveBeenCalledWith(
        "It's nobody's turn apparently."
      );
      done();
    });
  });

  it("should handle unknown game state", function (done) {
    // Modify the mock data to return an unknown state
    Game.Data.init("https://www.s1164087/game/", "development");
    Game.Data.setMockData([
      {
        url: "api/game/turn/<token>",
        data: 99,
      },
    ]);

    spyOn(console, "log");

    Game.Model.getGameState().then(function () {
      expect(console.log).toHaveBeenCalledWith("Unknown game state: 99.");
      done();
    });
  });

  it("should handle different game states correctly", function (done) {
    // Define different game states to test
    const gameStates = [
      { data: 0, message: "It's nobody's turn apparently." },
      { data: 1, message: "White's turn." },
      { data: 2, message: "Black's turn." },
    ];

    spyOn(console, "log");

    let testPromises = gameStates.map(function (state) {
      // Set the mock data
      Game.Data.setMockData([
        {
          url: "api/game/turn/<token>",
          data: state.data,
        },
      ]);

      // Call getGameState and verify the output
      return Game.Model.getGameState().then(function (data) {
        expect(data.data).toBe(state.data);
        expect(console.log).toHaveBeenCalledWith(state.message);
      });
    });

    // Wait for all promises to complete
    Promise.all(testPromises).then(function () {
      done();
    });
  });
});
