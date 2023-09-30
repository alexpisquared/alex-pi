using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Db.OneBase.Model;

public partial class OneBaseContext //: DbContext
{
  public OneBaseContext() { } // public OneBaseContext(DbContextOptions<OneBaseContext> options) { }
  public virtual DbSet<GuestbookMsg> GuestbookMsg { get; set; }
  public virtual IQueryable<VwUserHopsUtc> VwUserHopsUtc => new VwUserHopsUtc[] {
    new() {      Id = 1, Nickname = "AaaNick", Hops=11, TotalMin=111, Started=DateTime.Today, Finished = new DateTime(2020,1,1) },
    new() {      Id = 2, Nickname = "BbbNick", Hops=22, TotalMin=222, Started=DateTime.Today, Finished = new DateTime(2020,2,2) },
    new() {      Id = 3, Nickname = "CccNick", Hops=33, TotalMin=333, Started=DateTime.Today, Finished = DateTime.Now } }.AsQueryable();
  public virtual IQueryable<VwEventUserUtc> VwEventUserUtc => new VwEventUserUtc[] {
    new() {      Nickname = "AaaNick", EventName="Aaa Event Name", DoneAt= new DateTime(2020,1,1)},
    new() {      Nickname = "BbbNick", EventName="Bbb Event Name", DoneAt= new DateTime(2020,2,2)},
    new() {      Nickname = "BbbNick", EventName="Bbb Event Name", DoneAt= DateTime.Now } }.AsQueryable();

  public virtual DbSet<WebEventLog> WebEventLog { get; set; }
  public virtual DbSet<WebsiteUser> WebsiteUser { get; set; }

  public Entry Entry(object guestbookMsg) => new();
  public async Task<int> SaveChangesAsync() { await Task.Yield(); return -1; }
}

public class Entry
{
  public EntityState State { get; set; }
}
