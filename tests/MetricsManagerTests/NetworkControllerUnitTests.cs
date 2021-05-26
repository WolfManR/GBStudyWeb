using System;
using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
using MetricsManager.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class NetworkControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;
        private readonly Mock<INetworkMetricsRepository> _repoMock;
        
        public NetworkControllerUnitTests()
        {
            _repoMock = new();
            Mock<ILogger<NetworkMetricsController>> loggerMock = new();
            Mock<IMapper> mapperMock = new();
            _controller = new(_repoMock.Object,loggerMock.Object, mapperMock.Object);
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var request = new NetworkMetricsFromAgentRequest(agentId, fromTime, toTime);

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
            var request = new NetworkMetricsFromAllClusterRequest(fromTime, toTime);

            //Act
            var result = _controller.GetMetricsFromAllCluster(request);

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
            var request = new NetworkMetricsFromAgentRequest(agentId, fromTime, toTime);
            
            //Act
            _ = _controller.GetMetricsFromAgent(request);

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
            var request = new NetworkMetricsFromAllClusterRequest(fromTime, toTime);

            //Act
            _ = _controller.GetMetricsFromAllCluster(request);

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