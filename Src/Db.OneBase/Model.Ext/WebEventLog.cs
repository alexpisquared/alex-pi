using System.ComponentModel.DataAnnotations.Schema;

namespace Db.OneBase.Model
{
  public partial record WebEventLog {[NotMapped] public string BrowserSignature { get; set; }
    public string ToString2 => $"---{BrowserSignature} ";
  }
  public partial class WebsiteUser {[NotMapped] public int? VisitCount__New { get; set; } } // new / unreviewed visits  
}
