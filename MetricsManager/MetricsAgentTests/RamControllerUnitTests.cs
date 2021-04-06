using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;
using MetricsAgent;
using System.Collections.Generic;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;

        private Mock<IRamMetricsRepository> _mock;

        public RamControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();

            _controller = new RamMetricsController(_mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            var result = _controller.Create(new RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            _mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_Update_From_Repository()
        {
            _mock.Setup(repository => repository.Update(It.IsAny<RamMetric>())).Verifiable();

            var result = _controller.Update(new RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            _mock.Verify(repository => repository.Update(It.IsAny<RamMetric>()), Times.AtMostOnce());
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
            _mock.Setup(repository => repository.GetAll()).Returns(new List<RamMetric>());

            var result = _controller.GetAll();

            _mock.Verify(repository => repository.GetAll());
        }
    }
}
