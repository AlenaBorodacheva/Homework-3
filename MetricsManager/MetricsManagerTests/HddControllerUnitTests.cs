using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Enums;

namespace MetricsManagerTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController controller;

        public HddControllerUnitTests()
        {
            controller = new HddMetricsController();
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
            var getMetricsFromAgentResult = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            var getMetricsByPercentileFromAgentResult = controller.GetMetricsByPercentileFromAgent(agentId, fromTime, toTime, percentile);
            var getMetricsFromAllClusterResult = controller.GetMetricsFromAllCluster(fromTime, toTime);
            var getMetricsByPercentileFromAllClusterResult = controller.GetMetricsByPercentileFromAllCluster(fromTime, toTime, percentile);
            var getMetricsLeftResult = controller.GetMetricsLeft();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsFromAgentResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsByPercentileFromAgentResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsFromAllClusterResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsByPercentileFromAllClusterResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsLeftResult);
        }
    }
}
