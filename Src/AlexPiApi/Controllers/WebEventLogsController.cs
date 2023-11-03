using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AlexPiApi.Services;
using Db.OneBase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlexPiApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebEventLogsController : ControllerBase
{
  readonly OneBaseContext _context;
  readonly ILogger<WebEventLogsController> _logger;
  readonly IConfiguration _configuration;
  readonly ITextDbContext _textDbContext;

  public WebEventLogsController(OneBaseContext context, ILogger<WebEventLogsController> logger, IConfiguration configuration, ITextDbContext textDbContext) => (_context, _logger, _configuration, _textDbContext) = (context, logger, configuration, textDbContext);

  // GET: api/WebEventLogs
  [HttpGet]
  public async Task<ActionResult<IEnumerable<WebEventLog>>> GetWebEventLog()
  {
    await _textDbContext.AddStringAsync($"{GetType().FullName}.Get()");

    _logger.LogInformation($"▄▀▄▀▄▀ {nameof(WebEventLogsController)}.{nameof(GetWebEventLog)}() - {_configuration["WhereAmI"]} ▀▄▀▄▀▄");

    var events = _context.WebEventLog.OrderByDescending(r => r.Id).Take(21).ToList();

    //todo: does not await all events.ForEach(async ev => ev.Nickname = (await _context.WebsiteUser.FirstOrDefaultAsync(r => r.Id == ev.WebsiteUserId)).Nickname);

    //use view: foreach (var ev in events) ev.Nickname = (await _context.WebsiteUser.FirstOrDefaultAsync(r => r.Id == ev.WebsiteUserId)).Nickname;

    return events;
  }

  // GET: api/WebEventLogs/5
  [HttpGet("{id}")]
  public async Task<ActionResult<WebEventLog>> GetWebEventLog(int id)
  {
    await _textDbContext.AddStringAsync($"{GetType().FullName}.Get({id})");

    _logger.LogInformation($"▄▀▄▀▄▀ {nameof(WebEventLogsController)}.{nameof(GetWebEventLog)}({id}) - {_configuration["WhereAmI"]} ▀▄▀▄▀▄");

    var webEventLog = _context.WebEventLog.FirstOrDefault(r => r.Id == id);
    return webEventLog == null ? (ActionResult<WebEventLog>)NotFound() : (ActionResult<WebEventLog>)webEventLog;
  }

  // PUT: api/WebEventLogs/5
  [HttpPut("{id}")]
  public async Task<IActionResult> PutWebEventLog(int id, WebEventLog webEventLog)
  {
    await _textDbContext.AddStringAsync($"{GetType().FullName}.Put({webEventLog})");

    if (id != webEventLog.Id)
      return BadRequest();

    _context.Entry(webEventLog).State = EntityState.Modified;

    try
    {
      _ = await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!WebEventLogExists(id))
        return NotFound();
      else
        throw;
    }

    return NoContent();
  }

  // POST: api/WebEventLogs
  [HttpPost]
  public async Task<ActionResult<WebEventLog>> PostWebEventLog(WebEventLog webEventLog)
  {
    webEventLog.DoneAt = DateTime.UtcNow; // DateTime.Now is ambiguous ~ local time of the web server => UTC is better.

    await _textDbContext.AddStringAsync($"{webEventLog}");
    /* //todo: file sys db (Oct 11, 2023)
          try
          {
            var wsu = _context.WebsiteUser.FirstOrDefault(r => r.MemberSinceKey.Equals(webEventLog.BrowserSignature));
            if (wsu == null)
            {
              wsu = _context.WebsiteUser.Add(new WebsiteUser { Nickname = $"{webEventLog.DoneAt:yyMMdd}", CreatedAt = webEventLog.DoneAt, LastVisitAt = webEventLog.DoneAt, MemberSinceKey = webEventLog.BrowserSignature }).Entity; // *!?`
              await saveToDb(); // :Core3 does not provide mechanism for the new ID !!!
            }

            webEventLog.WebsiteUserId = wsu.Id;
            //todo: _context.WebEventLog.Add(webEventLog);
          }
          catch (Exception ex) { Debug.WriteLine(ex); throw; }

          await saveToDb();
    */
    CreatedAtActionResult caa;
    try
    {
      caa = CreatedAtAction("GetWebEventLog", new { id = webEventLog.Id }, webEventLog);
      Debug.WriteLine($" ** CreatedAtActionResult = {caa}");
    }
    catch (DbUpdateConcurrencyException ex) { Debug.WriteLine(ex); throw; }
    catch (Exception ex) { Debug.WriteLine(ex); throw; }

    return caa;
  }

  async Task saveToDb()
  {
    try
    {
      var rowsSaved = await _context.SaveChangesAsync();
      Debug.WriteLine($" ** Rows Saved = {rowsSaved}");
    }
    catch (DbUpdateConcurrencyException ex) { Debug.WriteLine(ex); throw; }
    catch (Exception ex) { Debug.WriteLine(ex); throw; }
  }

  // DELETE: api/WebEventLogs/5
  [HttpDelete("{id}")]
  public async Task<ActionResult<WebEventLog>> DeleteWebEventLog(int id)
  {
    var webEventLog = _context.WebEventLog.FirstOrDefault(r => r.Id == id);
    if (webEventLog == null)
      return NotFound();

    //todo: _context.WebEventLog.Remove(webEventLog);
    _ = await _context.SaveChangesAsync();

    return webEventLog;
  }

  bool WebEventLogExists(int id) => _context.WebEventLog.Any(e => e.Id == id);
}
