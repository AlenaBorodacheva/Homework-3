using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsCommon;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManagerTests
{
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController _controller;

        private Mock<ILogger<NetworkMetricsController>> _logger;

        public NetworkControllerUnitTests()
        {
            _logger = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(_logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var percentile = Percentile.P99;

            //Act
            var getMetricsFromAgentResult = _controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            var getMetricsByPercentileFromAgentResult = _controller.GetMetricsByPercentileFromAgent(agentId, fromTime, toTime, percentile);
            var getMetricsFromAllClusterResult = _controller.GetMetricsFromAllCluster(fromTime, toTime);
            var getMetricsByPercentileFromAllClusterResult = _controller.GetMetricsByPercentileFromAllCluster(fromTime, toTime, percentile);
            var getMetricsResult = _controller.GetMetrics(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsFromAgentResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsByPercentileFromAgentResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsFromAllClusterResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsByPercentileFromAllClusterResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsResult);
        }
    }
}
