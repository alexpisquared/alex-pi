using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class WebsiteUser
    {
        public WebsiteUser()
        {
            WebEventLog = new HashSet<WebEventLog>();
        }

        public int Id { get; set; }
        public string MemberSinceKey { get; set; }
        public string Nickname { get; set; }
        public string Note { get; set; }
        public bool? DoNotLog { get; set; }
        public int? ReviewedBy { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public DateTime LastVisitAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<WebEventLog> WebEventLog { get; set; }
    }
}
