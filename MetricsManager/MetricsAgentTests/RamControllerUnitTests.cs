using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsCommon;

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
            var getMetricsResult = controller.GetMetrics(fromTime, toTime);
            var getMetricsByPercentileResult = controller.GetMetricsByPercentile(fromTime, toTime, percentile);
            var getMetricsAvailableResult = controller.GetMetricsAvailable();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsByPercentileResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsAvailableResult);
        }
    }
}
