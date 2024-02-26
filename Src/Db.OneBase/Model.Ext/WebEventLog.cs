using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;

namespace Db.OneBase.Model;

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
  public DateTime FirstVisitDt
  {
    get {
      string[] formats = [
                "ddd MMM dd yyyy HH:mm:ss",
            "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz",
            "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Eastern Daylight Time)'",
            "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Eastern Standard Time)'",
            "MM/dd/yyyy h:mm:ss tt zzz",
            "M/d/yyyy h:mm:ss tt zzz",
            "MM/dd/yyyy HH:mm:ss",
            "M/d/yyyy HH:mm:ss",
            "MM/dd/yyyy h:mm:ss tt",
            "M/d/yyyy h:mm:ss tt",
            "MM/dd/yyyy",
            "M/d/yyyy"
      ];

      if (DateTime.TryParseExact(MemberSinceKey, formats, null, DateTimeStyles.None, out var d1))
        return d1;

      Trace.WriteLine($"Since none of the items from formats[] worked, what format to use for this date string: \n{MemberSinceKey}"); // System.Windows.Clipboard.SetText($"What format to use for this date string: \n{MemberSinceKey}");

      return DateTime.MaxValue;
    }
  }
}

