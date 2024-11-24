var myApp = {
  init: function () {
    return true;
  },
};

describe("Game.Data Module", function () {
  beforeAll(function () {
    Game.Data.init("https://www.s1164087/game/", "development");
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
    expect(Game.Data).toBeDefined();
  });

  it("should have a get function", function () {
    expect(Game.Data.get).toBeDefined();
    expect(typeof Game.Data.get).toBe("function");
  });

  it("should return mock data in development environment", function (done) {
    Game.Data.get().then(function (data) {
      expect(data).toBeDefined();
      expect(Array.isArray(data)).toBe(true);
      expect(data[0]).toEqual({
        url: "api/game/turn/<token>",
        data: 0,
      });
      done();
    });
  });

  it("should throw an error for unknown environment", function () {
    expect(function () {
      Game.Data.init("https://www.s1164087/game/", "staging");
    }).toThrowError("This environment is unknown.");
  });
});
