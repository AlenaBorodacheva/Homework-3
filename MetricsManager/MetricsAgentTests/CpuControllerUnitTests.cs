using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;
using MetricsAgent;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Requests;
using MetricsAgent.Models;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;

        private Mock<ICpuMetricsRepository> _mockRepository;

        private Mock<IMapper> _mockMapper;

        private Mock<ILogger<CpuMetricsController>> _logger;

        public CpuControllerUnitTests()
        {
            _mockRepository = new Mock<ICpuMetricsRepository>();

            _mockMapper = new Mock<IMapper>();

            _logger = new Mock<ILogger<CpuMetricsController>>();

            _controller = new CpuMetricsController(_mockRepository.Object, _mockMapper.Object, _logger.Object);
        }

        [Fact]
        public void Call_GetMetrics_From_Repository()
        {
            var fixture = new Fixture();
            var returnList = fixture.Create<List<CpuMetric>>();

            _mockRepository.Setup(repository => repository.GetMetrics(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(returnList);

            var fromTime = DateTimeOffset.FromUnixTimeSeconds(1000);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(2000);

            var result = (OkObjectResult)_controller.GetMetrics(fromTime, toTime);
            var actualResult = (List<CpuMetric>)result.Value;

            _mockRepository.Verify(repository => repository.GetMetrics(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
            Assert.Equal(returnList[0].Id, actualResult[0].Id);
        }
    }
}
