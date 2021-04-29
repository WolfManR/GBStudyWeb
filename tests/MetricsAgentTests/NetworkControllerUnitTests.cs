using System;
using MetricsAgent.Controllers;
using MetricsAgent.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;
        
        
        public NetworkControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetErrorsCount_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            NetworkMetricsRequest request = new(fromTime,toTime);
            
            //Act
            var result = _controller.GetMetrics(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}