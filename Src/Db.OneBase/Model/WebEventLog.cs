using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial record WebEventLog
    {
        public int Id { get; set; }
        public int WebsiteUserId { get; set; }
        public string EventName { get; set; }
        public DateTime DoneAt { get; set; }

        public virtual WebsiteUser WebsiteUser { get; set; }
    }
}
