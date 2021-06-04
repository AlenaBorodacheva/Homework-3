using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;
using MetricsAgent;
using System.Collections.Generic;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;

        private Mock<ICpuMetricsRepository> _mock;

        public CpuControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();

            _controller = new CpuMetricsController(_mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_Update_From_Repository()
        {
            _mock.Setup(repository => repository.Update(It.IsAny<CpuMetric>())).Verifiable();

            var result = _controller.Update(new CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            _mock.Verify(repository => repository.Update(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_Delete_From_Repository()
        {
            _mock.Setup(repository => repository.Delete(1)).Verifiable();

            var result = _controller.Delete(1);

            _mock.Verify(repository => repository.Delete(1));
        }

        [Fact]
        public void Create_ShouldCall_GetById_From_Repository()
        {
            _mock.Setup(repository => repository.GetById(1)).Verifiable();

            var result = _controller.GetById(1);

            _mock.Verify(repository => repository.GetById(1));
        }

        [Fact]
        public void Create_ShouldCall_GetAll_From_Repository()
        {
            _mock.Setup(repository => repository.GetAll()).Returns(new List<CpuMetric>());

            var result = _controller.GetAll();

            _mock.Verify(repository => repository.GetAll());
        }
    }
}
