using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db.OneBase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlexPiApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VwEventUserUtcsController : ControllerBase
{
  readonly OneBaseContext _context;

  public VwEventUserUtcsController(OneBaseContext context) => _context = context;

  // GET: api/VwEventUserUtcs
  [HttpGet] public ActionResult<IEnumerable<VwEventUserUtc>> GetVwEventUserUtc() => _context.VwEventUserUtc.OrderByDescending(r => r.DoneAt).Take(100).ToList(); // [HttpGet] public async Task<ActionResult<IEnumerable<VwEventUserUtc>>> GetVwEventUserUtc() => await _context.VwEventUserUtc.OrderByDescending(r => r.DoneAt).Take(100).ToListAsync();

  [HttpGet("{nickname}/{userId}")] public ActionResult<IEnumerable<VwEventUserUtc>> GetVwEventUserUtcWithParam(string nickname, int userId) => _context.VwEventUserUtc.Where(r => r.Nickname == nickname).OrderByDescending(r => r.DoneAt).Take(21).ToList(); // [HttpGet("{nickname}/{userId}")] public async Task<ActionResult<IEnumerable<VwEventUserUtc>>> GetVwEventUserUtcWithParam(string nickname, int userId) => await _context.VwEventUserUtc.Where(r => r.NickUser == nickname).OrderByDescending(r => r.DoneAt).Take(21).ToListAsync(); //todo: use UserId eventually: modify the view for that.

  [HttpGet("{a}/{b}/{c}")] public string Get__just_a_POC(int a, int b, int c) => (a + b + c).ToString();

  // GET: api/VwEventUserUtcs/5
  [HttpGet("{DoneAt}")]
  public ActionResult<VwEventUserUtc> GetVwEventUserUtc(DateTime DoneAt) // public async Task<ActionResult<VwEventUserUtc>> GetVwEventUserUtc(DateTime DoneAt)
  {
    var webEventLog = _context.VwEventUserUtc.FirstOrDefault(r => r.DoneAt == DoneAt);
    return webEventLog == null ? NotFound() : (ActionResult<VwEventUserUtc>)webEventLog;
  }
}
