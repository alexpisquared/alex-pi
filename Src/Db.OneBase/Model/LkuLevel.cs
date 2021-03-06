﻿using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class LkuLevel
    {
        public LkuLevel()
        {
            Problem = new HashSet<Problem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public virtual ICollection<Problem> Problem { get; set; }
    }
}
