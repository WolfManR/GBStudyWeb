using System;
using MetricsAgent.Controllers;
using MetricsAgent.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private readonly CpuMetricsController _controller;
        
        
        public CpuControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            CpuMetricsRequest request = new(fromTime,toTime);
            
            //Act
            var result = _controller.GetMetrics(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}