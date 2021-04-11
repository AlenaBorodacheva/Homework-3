using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsCommon;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController _controller;

        private Mock<ILogger<AgentsController>> _logger;

        public AgentsControllerUnitTests()
        {
            _logger = new Mock<ILogger<AgentsController>>();
            _controller = new AgentsController(_logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            MetricsManager.AgentInfo agentInfo = new MetricsManager.AgentInfo();
            var agentId = 1;

            //Act
            var registerAgentResult = _controller.RegisterAgent(agentInfo);
            var enableAgentByIdResult = _controller.EnableAgentById(agentId);
            var disableAgentByIdResult = _controller.DisableAgentById(agentId);
            var listOfRegisteredObjectsResult = _controller.ListOfRegisteredObjects();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(registerAgentResult);
            _ = Assert.IsAssignableFrom<IActionResult>(enableAgentByIdResult);
            _ = Assert.IsAssignableFrom<IActionResult>(disableAgentByIdResult);
            _ = Assert.IsAssignableFrom<IActionResult>(listOfRegisteredObjectsResult);
        }
    }
}
