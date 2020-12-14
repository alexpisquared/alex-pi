using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class VwEventUserUtc
    {
        public DateTime DoneAt { get; set; }
        public string Nickname { get; set; }
        public string EventName { get; set; }
    }
}
