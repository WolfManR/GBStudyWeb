using System;
using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
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
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new RamMetricsFromAgentRequest(agentId, fromTime, toTime);

            //Act
            var result = _controller.GetAvailableSpaceInfoFromAgent(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetAvailableSpaceInfoFromAllCluster_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new RamMetricsFromAllClusterRequest(fromTime, toTime);
            
            //Act
            var result = _controller.GetAvailableSpaceInfoFromAllCluster(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}