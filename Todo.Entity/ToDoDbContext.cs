using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity.System;

namespace Todo.Entity
{
    public class ToDoDbContext: DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options):base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // EFCore的数据库执行日志，可以跟踪时使用
            //optionsBuilder.LogTo(Console.WriteLine);
           
            //if (!optionsBuilder.IsConfigured)
            //    optionsBuilder.UseSqlite("Data Source=to.db");
 
        }
        
        public virtual DbSet<User>  User { get; set; }
    }
}
