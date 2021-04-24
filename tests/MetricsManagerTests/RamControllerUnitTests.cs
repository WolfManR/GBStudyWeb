using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
    public class RamControllerUnitTests
    {
        private readonly RamMetricsController _controller;
        
        
        public RamControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetAvailableSpaceInfoFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = _controller.GetAvailableSpaceInfoFromAgent(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetAvailableSpaceInfoFromAllCluster_ReturnsOk()
        {
            //Act
            var result = _controller.GetAvailableSpaceInfoFromAllCluster();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}