using System;
using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
using MetricsManager.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class DotnetControllerUnitTests
    {
        private readonly DotnetMetricsController _controller;
        private readonly Mock<IDotnetMetricsRepository> _repoMock;
        
        public DotnetControllerUnitTests()
        {
            _repoMock = new();
            Mock<ILogger<DotnetMetricsController>> loggerMock = new();
            _controller = new(_repoMock.Object,loggerMock.Object);
        }
        
        
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new ErrorsCountFromAgentRequest(agentId, fromTime, toTime);

            //Act
            var result = _controller.GetErrorsCountFromAgent(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new ErrorsCountFromAllClusterRequest(fromTime, toTime);

            //Act
            var result = _controller.GetErrorsCountFromAllCluster(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void GetMetricsFromAgent_VerifyRequestToRepository()
        {
            // Mock setup
            _repoMock.Setup(repo =>
                    repo
                        .GetByTimePeriod(
                            It.IsAny<DateTimeOffset>(),
                            It.IsAny<DateTimeOffset>(),
                            It.IsAny<int>()))
                .Verifiable();
            
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new ErrorsCountFromAgentRequest(agentId, fromTime, toTime);
            
            //Act
            _ = _controller.GetErrorsCountFromAgent(request);

            // Assert
            _repoMock.Verify(repo => 
                repo
                    .GetByTimePeriod(
                        It.IsAny<DateTimeOffset>(),
                        It.IsAny<DateTimeOffset>(),
                        It.IsAny<int>()
                    ), Times.AtMostOnce());
        }
        
        [Fact]
        public void GetMetricsFromAllCluster_VerifyRequestToRepository()
        {
            // Mock setup
            _repoMock.Setup(repo =>
                    repo
                        .GetByTimePeriod(
                            It.IsAny<DateTimeOffset>(),
                            It.IsAny<DateTimeOffset>()))
                .Verifiable();
            
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new ErrorsCountFromAllClusterRequest(fromTime, toTime);

            //Act
            _ = _controller.GetErrorsCountFromAllCluster(request);

            // Assert
            _repoMock.Verify(repo => 
                repo
                    .GetByTimePeriod(
                        It.IsAny<DateTimeOffset>(),
                        It.IsAny<DateTimeOffset>()
                    ), Times.AtMostOnce());
        }
    }
}