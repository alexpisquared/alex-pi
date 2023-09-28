using System;
using System.Collections.Generic;
using System.Linq;
using AlexPiApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlexPiApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  static readonly string[] Summaries = new[] { "freezing", "bracing", "chilly", "cool", "mild", "warm", "balmy", "hot", "sweltering", "scorching" };

  readonly ILogger<WeatherForecastController> _logger;
  readonly IConfiguration _configuration;
  readonly ITextDbContext _textDbContext;

  public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, ITextDbContext textDbContext) => (_logger, _configuration, _textDbContext) = (logger, configuration, textDbContext);

  [HttpGet]
  public IEnumerable<WeatherForecast> Get()
  {
    _logger.LogInformation($"▄▀▄▀▄▀ {nameof(WeatherForecastController)} - {_configuration["WhereAmI"]} - {_textDbContext.Audit()}▀▄▀▄▀▄");

    _ = _textDbContext.AddStringAsync($"WeatherForecastController");

    var random = new Random();
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = DateTime.Now.AddDays(index),
      TemperatureC = random.Next(-20, 55),
      Summary = $"{Summaries[random.Next(Summaries.Length)],-12}  ▀{_configuration["WhereAmI"]}▀  {_textDbContext.Audit()}"
    })
    .ToArray();
  }
}
