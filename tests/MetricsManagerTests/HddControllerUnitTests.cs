using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;
        
        
        public HddControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetLeftSpaceOnHddFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = _controller.GetLeftSpaceOnHddFromAgent(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetLeftSpaceOnHddFromAllCluster_ReturnsOk()
        {
            //Act
            var result = _controller.GetLeftSpaceOnHddFromAllCluster();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}