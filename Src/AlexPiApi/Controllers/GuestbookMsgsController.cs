using Db.OneBase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AlexPiApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GuestbookMsgsController : ControllerBase
  {
    readonly OneBaseContext _context;
    readonly ILogger<GuestbookMsgsController> _logger;
    readonly IConfiguration _configuration;

    //public GuestbookMsgsController(OneBaseContext context) => _context = context;
    public GuestbookMsgsController(OneBaseContext context, ILogger<GuestbookMsgsController> logger, IConfiguration configuration) => (_context, _logger, _configuration) = (context, logger, configuration);

    // GET: api/GuestbookMsgs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GuestbookMsg>>> GetGuestbookMsg()
    {
      _logger.LogInformation($"▄▀▄▀▄▀ {nameof(GuestbookMsgsController)}.{nameof(GetGuestbookMsg)}() - {_configuration["WhereAmI"]} ▀▄▀▄▀▄");

      var messages = await _context.GuestbookMsg.OrderByDescending(r => r.Id).Take(3).ToListAsync();

      return messages;
    }

    // GET: api/GuestbookMsgs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GuestbookMsg>> GetGuestbookMsg(int id)
    {
      _logger.LogInformation($"▄▀▄▀▄▀ {nameof(GuestbookMsgsController)}.{nameof(GetGuestbookMsg)}({id}) - {_configuration["WhereAmI"]} ▀▄▀▄▀▄");

      var GuestbookMsg = await _context.GuestbookMsg.FindAsync(id);
      if (GuestbookMsg == null)
      {
        return NotFound();
      }

      return GuestbookMsg;
    }

    // PUT: api/GuestbookMsgs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGuestbookMsg(int id, GuestbookMsg GuestbookMsg)
    {
      if (id != GuestbookMsg.Id)
      {
        return BadRequest();
      }

      _context.Entry(GuestbookMsg).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GuestbookMsgExists(id))
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

    // POST: api/GuestbookMsgs
    [HttpPost]
    public async Task<ActionResult<GuestbookMsg>> PostGuestbookMsg(GuestbookMsg GuestbookMsg)
    {
      try
      {
        GuestbookMsg.CreatedAt = DateTime.UtcNow; // DateTime.Now is ambiguos ~ local time of the web server => UTC is better.

        if (string.IsNullOrEmpty(GuestbookMsg.EventData))
          GuestbookMsg.EventData = $"MVC Controller adds  this: {GuestbookMsg.CreatedAt}.";

        _context.GuestbookMsg.Add(GuestbookMsg);
      }
      catch (Exception ex) { Debug.WriteLine(ex); throw; }

      await SaveToDb();

      CreatedAtActionResult caa;
      try
      {
        caa = CreatedAtAction("GetGuestbookMsg", new { id = GuestbookMsg.Id }, GuestbookMsg);
        Debug.WriteLine($" ** CreatedAtActionResult = {caa}");
      }
      catch (DbUpdateConcurrencyException ex) { Debug.WriteLine(ex); throw; }
      catch (Exception ex) { Debug.WriteLine(ex); throw; }

      return caa;
    }

    async Task SaveToDb()
    {
      try
      {
        var rowsSaved = await _context.SaveChangesAsync();
        Debug.WriteLine($" ** Rows Saved = {rowsSaved}");
      }
      catch (DbUpdateConcurrencyException ex) { Debug.WriteLine(ex); throw; }
      catch (Exception ex) { Debug.WriteLine(ex); throw; }
    }

    // DELETE: api/GuestbookMsgs/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<GuestbookMsg>> DeleteGuestbookMsg(int id)
    {
      var GuestbookMsg = await _context.GuestbookMsg.FindAsync(id);
      if (GuestbookMsg == null)
      {
        return NotFound();
      }

      _context.GuestbookMsg.Remove(GuestbookMsg);
      await _context.SaveChangesAsync();

      return GuestbookMsg;
    }

    bool GuestbookMsgExists(int id) => _context.GuestbookMsg.Any(e => e.Id == id);
  }
}
