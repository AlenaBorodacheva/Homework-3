using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsCommon;

namespace MetricsAgentTests
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
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var percentile = Percentile.P99;

            //Act
            var getMetricsResult = controller.GetMetrics(fromTime, toTime);
            var getMetricsByPercentileResult = controller.GetMetricsByPercentile(fromTime, toTime, percentile);
            var getMetricsLeftResult = controller.GetMetricsLeft();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsByPercentileResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsLeftResult);
        }
    }
}
