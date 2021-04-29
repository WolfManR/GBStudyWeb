using System;
using MetricsAgent.Controllers;
using MetricsAgent.Controllers.Requests;
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
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            FreeHardDriveSpaceRequest request = new(fromTime,toTime);
            
            //Act
            var result = _controller.GetFreeHardDriveSpace(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}