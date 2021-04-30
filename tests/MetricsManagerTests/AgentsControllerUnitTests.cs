using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
using MetricsManager.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private readonly AgentsController _controller;
        private readonly Mock<IAgentsRepository> _repoMock;
        
        public AgentsControllerUnitTests()
        {
            _repoMock = new();
            Mock<ILogger<AgentsController>> loggerMock = new();
            _controller = new(_repoMock.Object, loggerMock.Object);
        }


        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            var request = new RegisterAgentRequest(new("https://localhost:5000"));

            //Act
            var result = _controller.RegisterAgent(request);

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
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}