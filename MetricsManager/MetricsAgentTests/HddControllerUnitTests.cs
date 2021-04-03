using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;
using MetricsAgent;

namespace MetricsAgentTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController _controller;

        private Mock<IHddMetricsRepository> _mock;

        public HddControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();

            _controller = new HddMetricsController(_mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();
            _mock.Setup(repository => repository.GetAll()).Verifiable();

            var resultCreate = _controller.Create(new HddMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
            var resultGetAll = _controller.GetAll();

            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
            _mock.Verify(repository => repository.GetAll());
        }
    }
}
