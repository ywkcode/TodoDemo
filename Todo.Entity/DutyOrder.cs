using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Entity
{
    public class DutyOrder:BaseEntity
    {
        public string? Name { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

        public string? Remark { get; set; }
    }
}
