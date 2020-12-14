using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class Player
    {
        public Player()
        {
            Audition = new HashSet<Audition>();
            TombStone = new HashSet<TombStone>();
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Desc { get; set; }
        public DateTime AddedAt { get; set; }
        public string AddedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        public virtual ICollection<Audition> Audition { get; set; }
        public virtual ICollection<TombStone> TombStone { get; set; }
    }
}
