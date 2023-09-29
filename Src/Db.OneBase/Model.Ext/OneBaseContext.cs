using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Db.OneBase.Model;

public partial class OneBaseContext //: DbContext
{
  public OneBaseContext() { }
  //public OneBaseContext(DbContextOptions<OneBaseContext> options) { }
  public virtual DbSet<GuestbookMsg> GuestbookMsg { get; set; }
  public virtual IQueryable<VwUserHopsUtc> VwUserHopsUtc => new VwUserHopsUtc[] { new() { Id = 1, Nickname = "Aaa", Finished = DateTime.Now }, new() { Id = 2, Nickname = "Bbb", Finished = DateTime.Now } }.AsQueryable();
  public virtual DbSet<VwEventUserUtc> VwEventUserUtc { get; set; }
  public virtual DbSet<WebEventLog> WebEventLog { get; set; }
  public virtual DbSet<WebsiteUser> WebsiteUser { get; set; }

  public Entry Entry(object guestbookMsg) => new();
  public async Task<int> SaveChangesAsync() { await Task.Yield(); return -1; }
}

public class Entry
{
  public EntityState State { get; set; }
}
