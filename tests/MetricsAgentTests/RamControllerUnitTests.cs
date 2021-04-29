using System;
using MetricsAgent.Controllers;
using MetricsAgent.Controllers.Requests;
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
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            AvailableSpaceInfoRequest request = new(fromTime,toTime);
            
            //Act
            var result = _controller.GetAvailableSpaceInfo(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}