﻿using Eden.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eden.Core.Entities.Notes
{
    public class Note : BaseEntity
    {
        public string? Content { get; set; }
        public NotesCategory? Category { get; set; }
    }
}
