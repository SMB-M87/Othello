Game.Stat = (function () {
  let configMap = {
    turn: 0,
    chartContainer: null,
    chartInstance: null,
    statsData: {
      labels: [],
      capturedPieces: { white: [], black: [] },
    },
    cumulativeWhite: 0,
    cumulativeBlack: 0,
  };

  // Private methods
  const _init = (containerId) => {
    if (!containerId) {
      throw new Error("Game.Stat init failed: No container ID provided.");
    }
    configMap.chartContainer = containerId;
  };

  const _initChart = function (ctx, storeInstance = false) {
    if (storeInstance && configMap.chartInstance) {
      configMap.chartInstance.destroy();
    }
    const chartInstance = new Chart(ctx, {
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
          },
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
              text: "Flipped",
            },
          },
        },
      },
    });
    if (storeInstance) {
      configMap.chartInstance = chartInstance;
    }
    return chartInstance;
  };

  const _updateStats = (white, black) => {
    configMap.statsData.labels.push(`${configMap.turn}`);

    configMap.cumulativeWhite += white;
    configMap.cumulativeBlack += black;
    configMap.turn++;

    configMap.statsData.capturedPieces.white.push(configMap.cumulativeWhite);
    configMap.statsData.capturedPieces.black.push(configMap.cumulativeBlack);

    if (configMap.chartInstance) {
      configMap.chartInstance.update();
    }
  };

  const _renderChartInCanvas = (canvasElement) => {
    const ctx = canvasElement.getContext("2d");
    _initChart(ctx);
  };

  // Public methods
  const init = (containerId) => {
    _init(containerId);
  };

  const updateStats = (turn, white, black) => {
    _updateStats(turn, white, black);
  };

  const render = (canvas) => {
    _renderChartInCanvas(canvas);
  }

  return {
    init: init,
    updateStats: updateStats,
    render: render
  };
})();
