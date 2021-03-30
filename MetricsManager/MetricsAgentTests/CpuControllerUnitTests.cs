using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsCommon;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController controller;
        public CpuControllerUnitTests()
        {
            controller = new CpuMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var percentile = Percentile.P99;

            //Act
            var getMetricsByPercentileResult = controller.GetMetricsByPercentile(fromTime, toTime, percentile);
            var getMetricsResult = controller.GetMetrics(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsByPercentileResult);
            _ = Assert.IsAssignableFrom<IActionResult>(getMetricsResult);
        }
    }
}
