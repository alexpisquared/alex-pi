using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class GuestbookMsg // 2021-01 manually added
  {
        public int Id { get; set; }
        public string MemberSinceKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
    }
}
