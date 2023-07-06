using Eden.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eden.Core.Entities.Notes
{
    public class NotesCategory : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
