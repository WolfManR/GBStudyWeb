using System;
using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
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
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new HddMetricsFromAgentRequest(agentId, fromTime, toTime);

            //Act
            var result = _controller.GetLeftSpaceOnHddFromAgent(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetLeftSpaceOnHddFromAllCluster_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new HddMetricsFromAllClusterRequest(fromTime, toTime);
            
            //Act
            var result = _controller.GetLeftSpaceOnHddFromAllCluster(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}