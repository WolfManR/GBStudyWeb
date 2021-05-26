using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DataBase.Interfaces;
using MetricsAgent.DataBase.Models;
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
            IMapper mapper = new Mapper(
                new MapperConfiguration(expression =>
                    expression.AddProfile(typeof(MapperProfile))));
            _controller = new(_repoMock.Object, loggerMock.Object, mapper);
        }

        [Fact]
        public void GetMetrics_WithEmptyRepository_ReturnsEmptyResponse()
        {
            _repoMock.Setup(repo => repo
                .GetByTimePeriod(
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<DateTimeOffset>()))
                .Returns(() => Enumerable.Empty<DotnetMetric>().ToList());
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            ErrorsCountRequest request = new(fromTime, toTime);

            //Act
            var result = _controller.GetByTimePeriod(request);

            // Assert
            _ = Assert.IsType<DotnetMetricsByTimePeriodResponse>(result);
            Assert.Empty(result.Metrics);
        }

        [Fact]
        public void GetMetrics_WithOneEntryInRepository_ReturnsResponseWithOneEntry()
        {
            _repoMock.Setup(repo => repo
                    .GetByTimePeriod(
                        It.IsAny<DateTimeOffset>(),
                        It.IsAny<DateTimeOffset>()))
                .Returns(() => new List<DotnetMetric>(){new()
                {
                    Time = DateTimeOffset.FromUnixTimeSeconds(10).ToUnixTimeSeconds(),
                    Value = 1
                }});
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            ErrorsCountRequest request = new(fromTime, toTime);

            //Act
            var result = _controller.GetByTimePeriod(request);

            // Assert
            _ = Assert.IsType<DotnetMetricsByTimePeriodResponse>(result);
            Assert.Equal(1, result.Metrics.ToArray()[0].Value);
        }

        [Fact]
        public void GetMetrics_WithOneEntryInRepository_ReturnsEmptyResponse_WhenEntryIsNotInTimeRange()
        {
            _repoMock.Setup(repo => repo
                .GetByTimePeriod(
                    It.Is<DateTimeOffset>(offset => offset.Millisecond > 10),
                    It.Is<DateTimeOffset>(offset => offset.Millisecond > 10)))
                .Returns(() => Enumerable.Empty<DotnetMetric>().ToList());

            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(11);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            ErrorsCountRequest request = new(fromTime, toTime);

            //Act
            var result = _controller.GetByTimePeriod(request);

            // Assert
            _ = Assert.IsType<DotnetMetricsByTimePeriodResponse>(result);
            Assert.Empty(result.Metrics);
        }

        [Fact]
        public void GetErrorsCount_VerifyRequestToRepository()
        {
            // Mock setup
            _repoMock.Setup(repo => repo
                        .GetByTimePeriod(
                            It.IsAny<DateTimeOffset>(),
                            It.IsAny<DateTimeOffset>()))
                .Verifiable();
            _repoMock.Setup(repo => repo
                    .GetByTimePeriod(
                        It.IsAny<DateTimeOffset>(),
                        It.IsAny<DateTimeOffset>()))
                .Returns(() => Enumerable.Empty<DotnetMetric>().ToList());

            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);
            ErrorsCountRequest request = new(fromTime,toTime);
            
            //Act
            _ = _controller.GetByTimePeriod(request);

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