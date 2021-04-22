using System;
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
                _store.AddTemperature(weather.Temperature,weather.Time);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
        } 
        
        [HttpPut("edit")]
        public IActionResult EditTemperature([FromQuery] Weather weather)
        {
            try
            {
                _store.UpdateTemperature(weather.Temperature,weather.Time);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
            return Ok();
        } 
        
        [HttpDelete("delete")]
        public IActionResult RemoveTemperature([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            try
            {
                _store.RemoveTemperature(beginTime,endTime);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
            return Ok();
        } 
        
        [HttpGet]
        public IActionResult GetTemperatures([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            var weather = _store.GetTemperatures(beginTime, endTime);
            if (weather.Count <= 0)
                return NotFound();

            return Content(string.Join(";",weather));
        } 
    }
}