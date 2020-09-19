using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanResourceAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HumanResourceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _logger;
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching", "Tao moi 1 tinh nang thanh cong - Merge vao staging"
        };

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInfo("Đây là info message from Weather Controller");
            _logger.LogWarn("Đây là warn message from Weather Controller");
            _logger.LogDebug("Đây là debug message from Weather Controller");
            _logger.LogError("Đây là error message from Weather Controller");
            
            var rng = new Random();
            return Enumerable.Range(1, 4).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
