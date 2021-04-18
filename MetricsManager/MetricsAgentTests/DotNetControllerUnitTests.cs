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
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;

        private Mock<IDotNetMetricsRepository> _mockRepository;

        private Mock<IMapper> _mockMapper;

        private Mock<ILogger<DotNetMetricsController>> _logger;

        public DotNetControllerUnitTests()
        {
            _mockRepository = new Mock<IDotNetMetricsRepository>();

            _mockMapper = new Mock<IMapper>();

            _logger = new Mock<ILogger<DotNetMetricsController>>();

            _controller = new DotNetMetricsController(_mockRepository.Object, _mockMapper.Object, _logger.Object);
        }

        [Fact]
        public void Call_GetMetrics_From_Repository()
        {
            var fixture = new Fixture();
            var returnList = fixture.Create<List<DotNetMetric>>();

            _mockRepository.Setup(repository => repository.GetMetrics(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(returnList);

            var fromTime = DateTimeOffset.FromUnixTimeSeconds(1000);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(2000);

            var result = (OkObjectResult)_controller.GetMetrics(fromTime, toTime);
            var actualResult = (List<DotNetMetric>)result.Value;

            _mockRepository.Verify(repository => repository.GetMetrics(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
            Assert.Equal(returnList[0].Id, actualResult[0].Id);
        }
    }
}
