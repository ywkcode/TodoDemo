using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity;

namespace Todo.IService
{
    public interface IDutyPlanService
    {
        List<DutyPlan> GetDataLists();
        void DeletePlan(DutyPlan plan); 
        bool SavePlan(DutyPlan order);
    }
}
