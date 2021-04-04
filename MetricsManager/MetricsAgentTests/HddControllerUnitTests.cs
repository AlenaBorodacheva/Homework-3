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
            _mock.Setup(repository => repository.Update(It.IsAny<HddMetric>())).Verifiable();
            _mock.Setup(repository => repository.Delete(1)).Verifiable();
            _mock.Setup(repository => repository.GetById(1)).Verifiable();

            var resultCreate = _controller.Create(new HddMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
            var resultGetAll = _controller.GetAll();
            var resultUpdate = _controller.Update(new HddMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
            var resultDelete = _controller.Delete(1);
            var resultGetById = _controller.GetById(1);

            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
            _mock.Verify(repository => repository.GetAll());
            _mock.Verify(repository => repository.Update(It.IsAny<HddMetric>()), Times.AtMostOnce());
            _mock.Verify(repository => repository.Delete(1));
            _mock.Verify(repository => repository.GetById(1));
        }
    }
}
