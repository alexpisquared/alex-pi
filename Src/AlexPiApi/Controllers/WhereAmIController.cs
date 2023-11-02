using System;
using AlexPiApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlexPiApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WhereAmIController : ControllerBase
{
  readonly ILogger<WhereAmIController> _logger;
  readonly IConfiguration _configuration;
  readonly ITextDbContext _textDbContext;

  public WhereAmIController(ILogger<WhereAmIController> logger, IConfiguration configuration, ITextDbContext textDbContext) => (_logger, _configuration, _textDbContext) = (logger, configuration, textDbContext);

  [HttpGet]
  public string Get()
  {
    var report = $" {DateTime.Now} \r\n {GetType().FullName} \r\n WhereAmI:[{_configuration["WhereAmI"]}] \r\n {Environment.MachineName} \r\n Compiled: 2023-11-01 2200 - net7.0 + net6.0";

    _ = _textDbContext.AddStringAsync(report);

    _logger.LogInformation(report);
    return report;
  }
}
