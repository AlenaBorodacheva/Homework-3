using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;
using MetricsAgent;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;

        private Mock<IRamMetricsRepository> _mockRepository;

        private Mock<IMapper> _mockMapper;

        private Mock<ILogger<RamMetricsController>> _logger;

        public RamControllerUnitTests()
        {
            _mockRepository = new Mock<IRamMetricsRepository>();

            _mockMapper = new Mock<IMapper>();

            _logger = new Mock<ILogger<RamMetricsController>>();

            _controller = new RamMetricsController(_mockRepository.Object, _mockMapper.Object, _logger.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            var result = _controller.Create(new RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            _mockRepository.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_Update_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Update(It.IsAny<RamMetric>())).Verifiable();

            var result = _controller.Update(new RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            _mockRepository.Verify(repository => repository.Update(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_Delete_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Delete(1)).Verifiable();

            var result = _controller.Delete(1);

            _mockRepository.Verify(repository => repository.Delete(1));
        }

        [Fact]
        public void Create_ShouldCall_GetById_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetById(1)).Verifiable();

            var result = _controller.GetById(1);

            _mockRepository.Verify(repository => repository.GetById(1));
        }

        [Fact]
        public void Create_ShouldCall_GetAll_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetAll()).Returns(new List<RamMetric>());

            var result = _controller.GetAll();

            _mockRepository.Verify(repository => repository.GetAll());
        }
    }
}
