using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class TombStone
    {
        public string Id { get; set; }
        public string User { get; set; }
        public string PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
