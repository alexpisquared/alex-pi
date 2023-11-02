using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class VwLast100
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Nickname { get; set; }
        public DateTime? DoneAtLocalTime { get; set; }
        public string MemberSinceKey { get; set; }
    }
}
