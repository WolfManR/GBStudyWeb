using System;
using System.Collections.Generic;
using MetricsManager.Models;
using MetricsManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("weather")]
    public class WeatherController : ApiController
    {
        private readonly WeatherStore _store;

        public WeatherController(WeatherStore store)
        {
            _store = store;
        }
        
        [HttpPost("save")]
        public IActionResult SaveTemperature([FromQuery] Weather weather)
        {
            try
            {
                _store.AddTemperature(weather.Temperature, weather.Time);
            }
            catch(ArgumentException)
            {
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        } 
        
        [HttpPut("edit")]
        public IActionResult EditTemperature([FromQuery] Weather weather)
        {
            try
            {
                _store.UpdateTemperature(weather.Temperature, weather.Time);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        } 
        
        [HttpDelete("delete")]
        public IActionResult RemoveTemperature([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            try
            {
                _store.RemoveTemperature(beginTime, endTime);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        } 
        
        [HttpGet("getRange")]
        public IActionResult GetTemperatures([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            var weather = _store.GetTemperatures(beginTime, endTime);
            if (weather.Count <= 0)
                return NotFound();

            return Ok(weather);
        }

        [HttpGet]
        public IEnumerable<Weather> GetAllTemperatures()
        {
            var weather = _store.GetAllTemperatures();
            return weather;
        }
    }
}