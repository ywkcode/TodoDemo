using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity;
using Todo.IService;
using Todo.Models;

namespace Todo.Service
{
    public class DutyOrderService : BaseService, IDutyOrderService
    {

        public DutyOrderService(DbContext context)
         : base(context)
        {

        }
        public List<DutyOrder> GetDataLists()
        {
            var results = this.Query<DutyOrder>(s => s.Id > 0).ToList();
            return results;
        }

        public void DeleteOrder(DutyOrder order)
        { 
           this.Delete<DutyOrder>(order);
        }

        public bool SaveOrder(DutyOrder order)
        {
            return true;
        }
    }
}
