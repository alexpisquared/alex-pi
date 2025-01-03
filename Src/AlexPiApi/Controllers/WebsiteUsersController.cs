﻿using System;
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
public class WebsiteUsersController : ControllerBase
{
  readonly OneBaseContext _context;
  readonly ILogger<WebsiteUsersController> _logger;
  readonly IConfiguration _configuration;
  readonly ITextDbContext _textDbContext;

  public WebsiteUsersController(OneBaseContext context, ILogger<WebsiteUsersController> logger, IConfiguration configuration, ITextDbContext textDbContext) => (_context, _logger, _configuration, _textDbContext) = (context, logger, configuration, textDbContext);

  // GET: api/WebsiteUsers
  [HttpGet]
  public async Task<ActionResult<IEnumerable<WebsiteUser>>> GetWebsiteUser()
  {
    await _textDbContext.AddStringAsync($"{GetType().FullName}.GetWebsiteUser() {Environment.MachineName}");

    return new WebsiteUser[] { new() { CreatedAt = DateTime.Now, Note = "Aaa" }, new() { CreatedAt = DateTime.Now, Note = "Bbb" } }.ToList();

    var users = await _context.WebsiteUser.Where(r => r.ReviewedAt == null ||

      _context.WebEventLog.Any(e => e.WebsiteUserId == r.Id) && r.ReviewedAt < _context.WebEventLog.Where(e => e.WebsiteUserId == r.Id).Max(e => e.DoneAt)
    ).ToListAsync();

    var toSave = false;
    foreach (var user in users)
    {
      var userViews = _context.WebEventLog.Where(r => r.WebsiteUserId == user.Id);
      if (userViews != null && userViews.Any())
      {
        user.LastVisitAt = await userViews.MaxAsync(r => r.DoneAt);
        //user.VisitCount = await userViews.CountAsync(); <= used from view instead.
        user.VisitCount__New = await userViews.CountAsync(r => r.DoneAt > user.ReviewedAt); //todo: show on UI new / unreviewed visit count.
        toSave = true;
      }
    }

    if (toSave)
    {
      var rowsSaved = await _context.SaveChangesAsync();
      Debug.WriteLine($" ** Rows Saved = {rowsSaved}");
    }

    return users.OrderByDescending(r => r.LastVisitAt).ToList();
  }

  // GET: api/WebsiteUsers/5
  [HttpGet("{id}")]
  public async Task<ActionResult<WebsiteUser>> GetWebsiteUser(int id)
  {
    await _textDbContext.AddStringAsync($"{GetType().FullName}.GetWebsiteUser({id})");

    var websiteUser = await _context.WebsiteUser.FindAsync(id);

    return websiteUser == null ? (ActionResult<WebsiteUser>)NotFound() : (ActionResult<WebsiteUser>)websiteUser;
  }

  // PUT: api/WebsiteUsers/5
  // To protect from overposting attacks, please enable the specific properties you want to bind to, for
  // more details see https://aka.ms/RazorPagesCRUD.
  [HttpPut("{id}")]
  public async Task<IActionResult> PutWebsiteUser(int id, WebsiteUser websiteUser)
  {
    await _textDbContext.AddStringAsync($"{GetType().FullName}.PutWebsiteUser({id}  --  {websiteUser})");

    if (id != websiteUser.Id)
      return BadRequest();

    _context.Entry(websiteUser).State = EntityState.Modified;

    try
    {
      _ = await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!WebsiteUserExists(id))
        return NotFound();
      else
        throw;
    }

    return NoContent();
  }

  // POST: api/WebsiteUsers
  // To protect from overposting attacks, please enable the specific properties you want to bind to, for
  // more details see https://aka.ms/RazorPagesCRUD.
  [HttpPost]
  public async Task<ActionResult<WebsiteUser>> PostWebsiteUser(WebsiteUser websiteUser)
  {
    await _textDbContext.AddStringAsync($"{GetType().FullName}.Post({websiteUser})");

    _ = _context.WebsiteUser.Add(websiteUser);
    _ = await _context.SaveChangesAsync();

    return CreatedAtAction("GetWebsiteUser", new { id = websiteUser.Id }, websiteUser);
  }

  // DELETE: api/WebsiteUsers/5
  [HttpDelete("{id}")]
  public async Task<ActionResult<WebsiteUser>> DeleteWebsiteUser(int id)
  {
    await _textDbContext.AddStringAsync($"{GetType().FullName}.DeleteWebsiteUser({id})");

    var websiteUser = await _context.WebsiteUser.FindAsync(id);
    if (websiteUser == null)
      return NotFound();

    websiteUser.ReviewedAt = DateTime.UtcNow;
    websiteUser.ReviewedBy = -1; // _context.WebsiteUser.Remove(websiteUser);
    _ = await _context.SaveChangesAsync();

    return websiteUser;
  }

  bool WebsiteUserExists(int id) => _context.WebsiteUser.Any(e => e.Id == id);
}
