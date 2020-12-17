using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlexPiApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    static readonly string[] Summaries = new[] { "freezing", "bracing", "chilly", "cool", "mild", "warm", "balmy", "hot", "sweltering", "scorching" };

    readonly ILogger<WeatherForecastController> _logger;
    readonly IConfiguration _configuration;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration) => (_logger, _configuration) = (logger, configuration);

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      _logger.LogInformation($"▄▀▄▀▄▀ {nameof(WeatherForecastController)} - {_configuration["WhereAmI"]} ▀▄▀▄▀▄");

      var random = new Random();
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = random.Next(-20, 55),
        Summary = $"{Summaries[random.Next(Summaries.Length)]} ▄▀▄▀▄▀ {_configuration["WhereAmI"]} ▀▄▀▄▀▄" 
      })
      .ToArray();
    }
  }
}
