using Db.OneBase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexPi.WebApi.Core2.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VwUserHopsUtcsController : ControllerBase
  {
    readonly OneBaseContext _context;

    public VwUserHopsUtcsController(OneBaseContext context) => _context = context;

    // GET: api/VwUserHopsUtcs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VwUserHopsUtc>>> GetVwUserHopsUtc() => await _context.VwUserHopsUtc.OrderByDescending(r => r.Finished).ToListAsync();

    // GET: api/VwUserHopsUtcs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<VwUserHopsUtc>> GetVwUserHopsUtc(int id)
    {
      var webEventLog = await _context.VwUserHopsUtc.FirstOrDefaultAsync(r => r.Id == id);
      return webEventLog == null ? NotFound() : (ActionResult<VwUserHopsUtc>)webEventLog;
    }


    // DELETE: api/VwUserHopsUtcs/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<VwUserHopsUtc>> DeleteVwUserHopsUtc(int id)
    {
      var vwUserHopsUtc = await _context.VwUserHopsUtc.FirstOrDefaultAsync(r => r.Id == id);
      
      var websiteUser = await _context.WebsiteUser.FindAsync(id);
      if (websiteUser == null)
      {
        return NotFound();
      }

      websiteUser.ReviewedAt = DateTime.UtcNow;
      websiteUser.ReviewedBy = -1; // _context.WebsiteUser.Remove(websiteUser);
      await _context.SaveChangesAsync();

      return vwUserHopsUtc == null ? NotFound() : (ActionResult<VwUserHopsUtc>)vwUserHopsUtc;
    }
  }
}