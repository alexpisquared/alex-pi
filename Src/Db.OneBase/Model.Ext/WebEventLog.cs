using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;
using Microsoft.SqlServer.Server;

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
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Amazon Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Brasilia Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Central European Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Central Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(China Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Coordinated Universal Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(East Africa Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Eastern Daylight Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Eastern Daylight Saving Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Eastern European Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Eastern Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(India Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Moscow Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Mountain Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Pacific Standard Time)'",
        "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(北美东部标准时间)'",

        "MM/dd/yyyy h:mm:ss tt zzz",
        "M/d/yyyy h:mm:ss tt zzz",
        "MM/dd/yyyy HH:mm:ss",
        "M/d/yyyy HH:mm:ss",
        "MM/dd/yyyy h:mm:ss tt",
        "M/d/yyyy h:mm:ss tt",
        "MM/dd/yyyy",
        "M/d/yyyy",
        "yyyy-MM-dd hh:mm:ss tt zzz",
        "yyyy-MM-dd h:mm:ss tt zzz",
        "yyyy-MM-dd hh:mm:ss t.t. zzz",
        "yyyy-MM-dd h:mm:ss t.t. zzz",
        "yyyy-MM-dd hh:mm:ss tt zz",
        "yyyy-MM-dd h:mm:ss tt zz",
        "yyyy-MM-dd hh:mm:ss tt %z",
        "yyyy-MM-dd h:mm:ss tt %z",
        "yyyy-MM-dd hh:mm:ss",
        "yyyy-MM-dd h:mm:ss",
        "yyyy-MM-dd HH:mm:ss",
        "yyyy-MM-dd H:mm:ss",
        "yyyy-MM-dd HH:mm:ss zzz",
        "yyyy-MM-dd H:mm:ss zzz",
        "yyyy/MM/dd",
        "yyyy/MM/dd HH:mm:ss zzz",
        "dd/MM/yyyy HH:mm:ss zzz",
        "yyyy/MM/dd tt h:mm:ss zzz",
        "yyyy/MM/dd tt hh:mm:ss zzz",
        "yyyy/MM/dd tth:mm:ss zzz",
        "yyyy/MM/dd tthh:mm:ss zzz",
      ];

      if (DateTime.TryParseExact(MemberSinceKey, formats, null, DateTimeStyles.None, out var d1))
        return d1;

      // Create a new DateTimeFormatInfo object and set the AM/PM designators.
      DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
      dtfi.AMDesignator = "a.m.";
      dtfi.PMDesignator = "p.m.";

      if (DateTime.TryParseExact(MemberSinceKey, formats, dtfi, DateTimeStyles.None, out var d2))
        return d2;

      dtfi.AMDesignator = "上午";
      dtfi.PMDesignator = "下午";

      if (DateTime.TryParseExact(MemberSinceKey, formats, dtfi, DateTimeStyles.None, out var d3))
        return d3;


      int startIndex = MemberSinceKey.IndexOf("DoneAt = ") + "DoneAt = ".Length;
      int endIndex = MemberSinceKey.IndexOf(", WebsiteUser ");
      string dateString = MemberSinceKey.Substring(startIndex, endIndex - startIndex);

      if (DateTime.TryParseExact(dateString, formats, null, DateTimeStyles.None, out var d4))
        return d4;

      Trace.WriteLine($"Since none of the items from formats[] worked, what format to use for this date string: \t \"{MemberSinceKey}\""); // System.Windows.Clipboard.SetText($"What format to use for this date string: \n{MemberSinceKey}");
      //Trace.WriteLine($"\"ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '{MemberSinceKey}'\","); // System.Windows.Clipboard.SetText($"What format to use for this date string: \n{MemberSinceKey}");

      return DateTime.MaxValue;
    }
  }
}