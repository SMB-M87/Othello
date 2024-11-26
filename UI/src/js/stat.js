Game.Stat = (function () {
  let configMap = {
    turn: 0,
    chartContainer: null,
    chartInstance: null,
    statsData: {
      labels: [],
      capturedPieces: { white: [], black: [] }
    },
  };

  // Private methods
  const _init = (containerId) => {
    if (!containerId) {
      throw new Error("Game.Stat init failed: No container ID provided.");
    }
    configMap.chartContainer = containerId;
    _initChart();
  };

  const _initChart = function () {
    const ctx = document
      .getElementById(configMap.chartContainer)
      .getContext("2d");
    if (configMap.chartInstance) {
      configMap.chartInstance.destroy();
    }
    configMap.chartInstance = new Chart(ctx, {
      type: "line",
      data: {
        labels: configMap.statsData.labels,
        datasets: [
          {
            label: "White",
            data: configMap.statsData.capturedPieces.white,
            borderColor: "#d0c292",
            fill: false,
          },
          {
            label: "Black",
            data: configMap.statsData.capturedPieces.black,
            borderColor: "#000000",
            fill: false,
          }
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            display: true,
          },
        },
        scales: {
          x: {
            title: {
              display: true,
              text: "Turns",
            },
          },
          y: {
            title: {
              display: true,
              text: "Count",
            },
          },
        },
      },
    });
  };

  const _updateStats = (white, black) => {
    configMap.statsData.labels.push(`${configMap.turn}`);
    configMap.statsData.capturedPieces.white.push(white);
    configMap.statsData.capturedPieces.black.push(black);
    configMap.turn++;
    _initChart();
  };

  // Public methods
  const init = (containerId) => {
    _init(containerId);
  };

  const updateStats = (turn, white, black) => {
    _updateStats(turn, white, black);
  };

  return {
    init: init,
    updateStats: updateStats
  };
})();