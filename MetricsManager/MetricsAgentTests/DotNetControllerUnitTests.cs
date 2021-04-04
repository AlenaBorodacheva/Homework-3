using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;
using MetricsAgent;

namespace MetricsAgentTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;

        private Mock<IDotNetMetricsRepository> _mock;

        public DotNetControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();

            _controller = new DotNetMetricsController(_mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
           
            var result = _controller.Create(new DotNetMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
            
            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_Update_From_Repository()
        {
            _mock.Setup(repository => repository.Update(It.IsAny<DotNetMetric>())).Verifiable();

            var result = _controller.Update(new DotNetMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            _mock.Verify(repository => repository.Update(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
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
    }
}
