using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("weather")]
    public class WeatherController : ApiController
    {
        [HttpPost("save")]
        public IActionResult SaveTemperature([FromQuery] double temperature, [FromQuery] DateTime time)
        {
            return Ok();
        } 
        
        [HttpPut("edit")]
        public IActionResult EditTemperature([FromQuery] double temperature, [FromQuery] DateTime time)
        {
            return Ok();
        } 
        
        [HttpDelete("delete")]
        public IActionResult RemoveTemperature([FromQuery] DateTime time)
        {
            return Ok();
        } 
        
        [HttpGet]
        public IActionResult GetTemperatures([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            return Ok();
        } 
    }
}