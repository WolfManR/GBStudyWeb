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
        
        
        [Fact]
        public void GetErrorsCount_VerifyRequestToRepository()
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
            ErrorsCountRequest request = new(fromTime,toTime);
            
            //Act
            _ = _controller.GetErrorsCount(request);

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