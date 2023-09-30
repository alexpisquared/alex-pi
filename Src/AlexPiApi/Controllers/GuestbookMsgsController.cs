using AlexPiApi.Services;
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
    readonly ITextDbContext _textDbContext;

    public GuestbookMsgsController(OneBaseContext context, ILogger<GuestbookMsgsController> logger, IConfiguration configuration, ITextDbContext textDbContext) => (_context, _logger, _configuration, _textDbContext) = (context, logger, configuration, textDbContext);

    // GET: api/GuestbookMsgs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GuestbookMsg>>> GetGuestbookMsg()
    {
      await _textDbContext.AddStringAsync($"{GetType().FullName}  {Environment.MachineName}  Get()");

      _logger.LogInformation($"▄▀▄▀▄▀ {nameof(GuestbookMsgsController)}.{nameof(GetGuestbookMsg)}() - {_configuration["WhereAmI"]} ▀▄▀▄▀▄");

      return new GuestbookMsg[] { new() { CreatedAt = DateTime.Now, Message = "Aaa" }, new() { CreatedAt = DateTime.Now, Message = "Bbb" } }.ToList(); //2023-09: return await _context.GuestbookMsg.OrderByDescending(r => r.Id).Take(3).ToListAsync();
    }

    // GET: api/GuestbookMsgs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GuestbookMsg>> GetGuestbookMsg(int id)
    {
      await _textDbContext.AddStringAsync($"{GetType().FullName}  {Environment.MachineName}  id:{id}");


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
      await _textDbContext.AddStringAsync($"{GetType().FullName}  {Environment.MachineName}  {GuestbookMsg.Message}  {GuestbookMsg.EventData}  {GuestbookMsg.CreatedAt}");

      if (id != GuestbookMsg.Id)
      {
        return BadRequest();
      }

      _context.Entry(GuestbookMsg).State = EntityState.Modified;

      try
      {
        ; //2023-09:         await _context.SaveChangesAsync();
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
      await _textDbContext.AddStringAsync($"{GetType().FullName}  {Environment.MachineName}  {GuestbookMsg.Message}  {GuestbookMsg.EventData}  {GuestbookMsg.CreatedAt}");

      try
      {
        GuestbookMsg.CreatedAt = DateTime.UtcNow; // DateTime.Now is ambiguos ~ local time of the web server => UTC is better.

        if (string.IsNullOrEmpty(GuestbookMsg.EventData))
          GuestbookMsg.EventData = $"MVC Controller adds  this: {GuestbookMsg.CreatedAt}.";

        _context.GuestbookMsg.Add(GuestbookMsg);
      }
      catch (Exception ex) { Debug.WriteLine(ex); throw; }

      //2023-09: await SaveToDb();

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
