using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Entity.System
{
    public class User : BaseEntity
    {
        public string? Account { get; set; }

        public string? UserName { get; set; }

        public string? PassWord { get; set; }
    }
}
