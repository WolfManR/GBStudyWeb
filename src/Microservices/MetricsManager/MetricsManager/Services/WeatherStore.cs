using System;
using System.Collections.Generic;
using System.Linq;
using MetricsManager.Exceptions;
using MetricsManager.Models;

namespace MetricsManager.Services
{
    public class WeatherStore
    {
        private readonly Dictionary<DateTime, double> _weather = new();

        public void AddTemperature(double temperature, DateTime time)
        {
            if (!_weather.TryAdd(time, temperature))
                throw new DataStoreException($"Failure to add temperature {temperature} at {time}");
        }

        public void UpdateTemperature(double temperature, DateTime time)
        {
            if (!_weather.ContainsKey(time))
                throw new DataStoreException($"Not existed entry at {time}");
            _weather[time] = temperature;
        }

        public void RemoveTemperature(DateTime beginTime, DateTime endTime)
        {
            var temperatures = GetTemperatures(beginTime, endTime);
            if (!temperatures.Any())
                throw new DataStoreException($"Not existed entries in range [{beginTime} - {endTime}]");
            foreach (var (time,_) in temperatures)
                _weather.Remove(time);
        }

        public List<(DateTime, double)> GetTemperatures(DateTime start, DateTime end) =>
            _weather
                .Where(pair => pair.Key > start && pair.Key < end)
                .OrderBy(pair => pair.Key)
                .Select(pair => (pair.Key,pair.Value))
                .ToList();

        public List<Weather> GetAllTemperatures() =>
            _weather
                .OrderBy(pair => pair.Key)
                .Select(pair => new Weather(){Temperature = pair.Value,Time = pair.Key})
                .ToList();
    }
}