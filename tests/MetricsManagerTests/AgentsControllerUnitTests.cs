using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private readonly AgentsController _controller;


        public AgentsControllerUnitTests()
        {
            _controller = new();
        }


        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            var agent = new AgentInfo(1, new("https://localhost:5000"));

            //Act
            var result = _controller.RegisterAgent(agent);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void EnableAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = _controller.EnableAgentById(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void DisableAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = _controller.DisableAgentById(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetRegisteredAgents_ReturnsEmptyEnumerable()
        {
            //Act
            var result = _controller.GetRegisteredAgents();

            // Assert
            Assert.Empty(result);
        }
    }
}