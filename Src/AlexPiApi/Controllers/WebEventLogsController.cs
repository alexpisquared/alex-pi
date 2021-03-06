﻿using Db.OneBase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AlexPi.WebApi.NetCore2._2.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class WebEventLogsController : ControllerBase
  {
    readonly OneBaseContext _context;
    readonly ILogger<WebEventLogsController> _logger;
    readonly IConfiguration _configuration;

    public WebEventLogsController(OneBaseContext context, ILogger<WebEventLogsController> logger, IConfiguration configuration) => (_context, _logger, _configuration) = (context, logger, configuration);

    // GET: api/WebEventLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WebEventLog>>> GetWebEventLog()
    {
      _logger.LogInformation($"▄▀▄▀▄▀ {nameof(WebEventLogsController)}.{nameof(GetWebEventLog)}() - {_configuration["WhereAmI"]} ▀▄▀▄▀▄");

      var events = await _context.WebEventLog.OrderByDescending(r => r.Id).Take(21).ToListAsync();

      //todo: does not await all events.ForEach(async ev => ev.Nickname = (await _context.WebsiteUser.FirstOrDefaultAsync(r => r.Id == ev.WebsiteUserId)).Nickname);

      //use view: foreach (var ev in events) ev.Nickname = (await _context.WebsiteUser.FirstOrDefaultAsync(r => r.Id == ev.WebsiteUserId)).Nickname;

      return events;
    }

    // GET: api/WebEventLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WebEventLog>> GetWebEventLog(int id)
    {
      _logger.LogInformation($"▄▀▄▀▄▀ {nameof(WebEventLogsController)}.{nameof(GetWebEventLog)}({id}) - {_configuration["WhereAmI"]} ▀▄▀▄▀▄");

      var webEventLog = await _context.WebEventLog.FindAsync(id);
      if (webEventLog == null)
      {
        return NotFound();
      }

      return webEventLog;
    }

    // PUT: api/WebEventLogs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWebEventLog(int id, WebEventLog webEventLog)
    {
      if (id != webEventLog.Id)
      {
        return BadRequest();
      }

      _context.Entry(webEventLog).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!WebEventLogExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/WebEventLogs
    [HttpPost]
    public async Task<ActionResult<WebEventLog>> PostWebEventLog(WebEventLog webEventLog)
    {
      try
      {
        webEventLog.DoneAt = DateTime.UtcNow; // DateTime.Now is ambiguos ~ local time of the web server => UTC is better.

        var wsu = _context.WebsiteUser.FirstOrDefault(r => r.EventData.Equals(webEventLog.EventData__Copy));// ..., StringComparison.OrdinalIgnoreCase)); //tu: , StringComparison.* i.e.: none works!!!!!!!!! ==> use without StringComparison param!!!  <= I think it is original Core 3.0 bug
        if (wsu == null)
        {
          wsu = _context.WebsiteUser.Add(new WebsiteUser { Nickname = "¤¤¤", CreatedAt = webEventLog.DoneAt, LastVisitAt = webEventLog.DoneAt, EventData = webEventLog.EventData__Copy }).Entity; // *!?`
          await saveToDb(); // Core 3 does not provide mechanizm for the new ID !!!
        }

        webEventLog.WebsiteUserId = wsu.Id;
        _context.WebEventLog.Add(webEventLog);
      }
      catch (Exception ex) { Debug.WriteLine(ex); throw; }

      await saveToDb();

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
      var webEventLog = await _context.WebEventLog.FindAsync(id);
      if (webEventLog == null)
      {
        return NotFound();
      }

      _context.WebEventLog.Remove(webEventLog);
      await _context.SaveChangesAsync();

      return webEventLog;
    }

    bool WebEventLogExists(int id) => _context.WebEventLog.Any(e => e.Id == id);
  }
}
