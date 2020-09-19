using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
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
        private IRepositoryBase<Company, Guid> _companyRepository;
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching", "Tao moi 1 tinh nang thanh cong - Merge vao staging"
        };

        public WeatherForecastController(ILoggerManager logger, IRepositoryBase<Company, Guid> companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }


        [HttpGet]
        public List<Company> Get()
        {
            var result = _companyRepository.FindAll(false).ToList();
            return result;

            //
            //
            // var rng = new Random();
            // return Enumerable.Range(1, 4).Select(index => new WeatherForecast
            // {
            //     Date = DateTime.Now.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // })
            // .ToArray();
        }
    }
}
