using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity;
using Todo.Models;

namespace Todo.IService
{
    public  interface IDutyOrderService
    {
        List<DutyOrder> GetDataLists();
        void DeleteOrder(DutyOrder order);

        bool SaveOrder(DutyOrder order);
    }
}
