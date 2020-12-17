using System.ComponentModel.DataAnnotations.Schema;

namespace Db.OneBase.Model
{
  public partial class WebEventLog {[NotMapped] public string EventData__Copy { get; set; } }
  public partial class WebsiteUser {[NotMapped] public int? VisitCount__New { get; set; } } // new / unreviewed visits  
}
