using System;
using MetricsAgent.Controllers;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;
        private readonly Mock<INetworkMetricsRepository> _repoMock;
        
        public NetworkControllerUnitTests()
        {
            _repoMock = new();
            Mock<ILogger<NetworkMetricsController>> loggerMock = new();
            _controller = new(_repoMock.Object,loggerMock.Object);
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
        
        
        [Fact]
        public void GetFreeHardDriveSpace_VerifyRequestToRepository()
        {
            // Mock setup
            _repoMock.Setup(repo =>
                    repo
                        .GetByTimePeriod(
                            It.IsAny<DateTimeOffset>(),
                            It.IsAny<DateTimeOffset>()))
                .Verifiable();
            
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            NetworkMetricsRequest request = new(fromTime,toTime);
            
            //Act
            _ = _controller.GetMetrics(request);

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