using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private readonly RamMetricsController _controller;
        
        
        public RamControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetAvailableSpaceInfo_ReturnsOk()
        {
            //Act
            var result = _controller.GetAvailableSpaceInfo();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}