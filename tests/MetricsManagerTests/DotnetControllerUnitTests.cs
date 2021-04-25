using System;
using Domain;
using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
    public class DotnetControllerUnitTests
    {
        private readonly DotnetMetricsController _controller;
        
        
        public DotnetControllerUnitTests()
        {
            _controller = new();
        }
        
        
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var request = new GetErrorsCountFromAgentRequest(agentId, fromTime, toTime);

            //Act
            var result = _controller.GetErrorsCountFromAgent(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var request = new GetErrorsCountFromAllClusterRequest(fromTime, toTime);

            //Act
            var result = _controller.GetErrorsCountFromAllCluster(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}