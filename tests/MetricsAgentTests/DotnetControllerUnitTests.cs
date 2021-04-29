using System;
using MetricsAgent.Controllers;
using MetricsAgent.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class DotnetControllerUnitTests
    {
        private readonly DotnetMetricsController _controller;
        
        
        public DotnetControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetErrorsCount_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            ErrorsCountRequest request = new(fromTime,toTime);
            
            //Act
            var result = _controller.GetErrorsCount(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}