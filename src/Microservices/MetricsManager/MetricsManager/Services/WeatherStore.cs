using System;
using System.Collections.Generic;
using System.Linq;
using MetricsManager.Exceptions;

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

        public void RemoveTemperature(DateTime time)
        {
            if (!_weather.ContainsKey(time))
                throw new DataStoreException($"Not existed entry at {time}");
            _weather.Remove(time);
        }

        public IEnumerable<(DateTime, double)> GetTemperatures(DateTime start, DateTime end)
        {
            var weather = _weather.Where(pair => pair.Key > start && pair.Key < end).OrderBy(pair => pair.Key);
            foreach (var (time, temperature) in weather)
                yield return (time, temperature);
        }
    }
}