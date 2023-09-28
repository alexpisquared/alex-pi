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
    var report = $"{GetType().FullName} - WhereAmI:[{_configuration["WhereAmI"]}] - {Environment.MachineName} - Compiled: 2023-09-28";

    _ = _textDbContext.AddStringAsync(report);

    _logger.LogInformation(report);
    return report;
  }
}
