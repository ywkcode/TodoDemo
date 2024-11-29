using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity;
using Todo.IService;

namespace Todo.Service
{
    public class DutyPlanService : BaseService, IDutyPlanService
    {
        public DutyPlanService(DbContext context) : base(context)
        {
        }

        public void DeletePlan(DutyPlan plan)
        {
           
        }

        public async Task<List<DutyPlan>> GetDataLists()
        {
            var results = this.Query<DutyPlan>(s => s.Id > 0).ToList();
            return await Task.Run(() => results);
        }

        public bool SavePlan(DutyPlan order)
        {
            return true;
        }
    }
}
