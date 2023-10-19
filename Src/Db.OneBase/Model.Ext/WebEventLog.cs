using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Db.OneBase.Model
{
  public partial record WebEventLog
  {
    [NotMapped] public string BrowserSignature { get; set; }
    [NotMapped] public string Nickname { get; set; }       // don't change order!!! :parsing depends on it.
    [NotMapped] public string FirstVisitId { get; set; }   // don't change order!!! :parsing depends on it.
  }
  public partial class WebsiteUser
  {
    [NotMapped] public int? VisitCount__New { get; set; }  // new / unreviewed visits  
    [NotMapped] public int TotalVisits { get; set; }
    [NotMapped] public TimeSpan Spread { get; set; }
  }
}
