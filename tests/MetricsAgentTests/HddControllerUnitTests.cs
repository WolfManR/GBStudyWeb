using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;
        
        
        public HddControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetFreeHardDriveSpace_ReturnsOk()
        {
            //Act
            var result = _controller.GetFreeHardDriveSpace();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}