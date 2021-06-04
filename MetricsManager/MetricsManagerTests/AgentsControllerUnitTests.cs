using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsCommon;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;
        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            MetricsManager.AgentInfo agentInfo = new MetricsManager.AgentInfo();
            var agentId = 1;

            //Act
            var registerAgentResult = controller.RegisterAgent(agentInfo);
            var enableAgentByIdResult = controller.EnableAgentById(agentId);
            var disableAgentByIdResult = controller.DisableAgentById(agentId);
            var listOfRegisteredObjectsResult = controller.ListOfRegisteredObjects();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(registerAgentResult);
            _ = Assert.IsAssignableFrom<IActionResult>(enableAgentByIdResult);
            _ = Assert.IsAssignableFrom<IActionResult>(disableAgentByIdResult);
            _ = Assert.IsAssignableFrom<IActionResult>(listOfRegisteredObjectsResult);
        }
    }
}
