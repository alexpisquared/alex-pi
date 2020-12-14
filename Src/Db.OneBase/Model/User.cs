using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class User
    {
        public User()
        {
            SessionResult = new HashSet<SessionResult>();
        }

        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<SessionResult> SessionResult { get; set; }
    }
}
