using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Enums;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController controller;

        public RamControllerUnitTests()
        {
            controller = new RamMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var percentile = Percentile.P99;

            //Act
            var getMetricsFromAllClusterResult = controller.GetMetricsFromAllCluster(fromTime, toTime);
            var getMetricsByPercentileFromAllClusterResult = controller.GetMetricsByPercentileFromAllCluster(fromTime, toTime, percentile);
            var getMetricsAvailableResult = controller.GetMetricsAvailable();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsFromAllClusterResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsByPercentileFromAllClusterResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsAvailableResult);
        }
    }
}
