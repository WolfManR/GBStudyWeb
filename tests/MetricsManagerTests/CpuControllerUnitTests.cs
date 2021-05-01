using System;
using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuControllerUnitTests
    {
        private readonly CpuMetricsController _controller;
        
        
        public CpuControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new CpuMetricsFromAgentRequest(agentId, fromTime, toTime);
            
            //Act
            var result = _controller.GetMetricsFromAgent(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new CpuMetricsFromAllClusterRequest(fromTime, toTime);

            //Act
            var result = _controller.GetMetricsFromAllCluster(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}