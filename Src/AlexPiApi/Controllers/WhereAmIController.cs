using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlexPiApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WhereAmIController : ControllerBase
  {
    readonly ILogger<WhereAmIController> _logger;
    readonly IConfiguration _configuration;

    public WhereAmIController(ILogger<WhereAmIController> logger, IConfiguration configuration) => (_logger, _configuration) = (logger, configuration);

    [HttpGet]
    public string Get()
    {
      var report = $"▄▀▄▀▄▀ {this.GetType().FullName} - {_configuration["WhereAmI"]} - Compiled: 2020-12-17 ▀▄▀▄▀▄";

      _logger.LogInformation(report);
      return report;
    }
  }
}
