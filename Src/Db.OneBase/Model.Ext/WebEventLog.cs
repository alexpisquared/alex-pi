using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Db.OneBase.Model
{
  public partial record WebEventLog
  {
    [NotMapped] public string[] Sub { get; set; }
    [NotMapped] public string BrowserSignature { get; set; }
    [NotMapped] public string NickUser { get; set; }       // don't change order!!! :parsing depends on it.
    [NotMapped] public string FirstVisitId { get; set; }   // don't change order!!! :parsing depends on it.
    [NotMapped] public string NickWare { get; set; }       // don't change order!!! :parsing depends on it.
  }
  public partial class WebsiteUser
  {
    [NotMapped] public int? VisitCount__New { get; set; }  // new / unreviewed visits  
    [NotMapped] public int TotalVisits { get; set; }
    [NotMapped] public TimeSpan Spread => LastVisitAt - FirstVisitDt;

    [NotMapped]
    public DateTime FirstVisitDt =>
      DateTime.TryParse(MemberSinceKey, out var dt) ? dt :
      DateTime.TryParseExact(MemberSinceKey, _fmt0, null, DateTimeStyles.None, out var d0) ? d0 :
      DateTime.TryParseExact(MemberSinceKey, _fmt1, null, DateTimeStyles.None, out var d1) ? d1 :
      DateTime.TryParseExact(MemberSinceKey, _fmt2, null, DateTimeStyles.None, out var d2) ? d2 :
      DateTime.MinValue;
 
    const string 
      _fmt0 = "ddd MMM dd yyyy HH:mm:ss",
      _fmt1 = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz",
      _fmt2 = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Eastern Daylight Time)'";
  }
}

