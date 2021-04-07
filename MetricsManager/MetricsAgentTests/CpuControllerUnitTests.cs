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
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_Update_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Update(It.IsAny<CpuMetric>())).Verifiable();

            var result = _controller.Update(new CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            _mockRepository.Verify(repository => repository.Update(It.IsAny<CpuMetric>()), Times.AtMostOnce());
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
            _mockRepository.Setup(repository => repository.GetAll()).Returns(new List<CpuMetric>());

            var result = _controller.GetAll();

            _mockRepository.Verify(repository => repository.GetAll());
        }
    }
}
