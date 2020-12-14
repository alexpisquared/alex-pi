﻿using System;
using System.Collections.Generic;

namespace Db.OneBase.Model
{
    public partial class VwUserHopsUtc
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int? Hops { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }
        public int? TotalMin { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }
}
