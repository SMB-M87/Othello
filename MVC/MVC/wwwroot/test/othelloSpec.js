var myApp = {
  init: function () {
    return true;
  },
};

describe("Game.Othello Module", function () {
  beforeAll(function () {
    Game.Othello.init("https://www.s1164087/game/");
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
    expect(Game.Othello).toBeDefined();
  });

  it("should have an init function", function () {
    expect(Game.Othello.init).toBeDefined();
    expect(typeof Game.Othello.init).toBe("function");
  });

  // Add more tests as you implement more functionality in Othello module
});
