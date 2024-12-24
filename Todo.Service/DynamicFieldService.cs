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
    public class DynamicFieldService : BaseService, IDynamicFieldService
    {
        public DynamicFieldService(DbContext context) : base(context)
        {
        }
        public List<DynamicField> GetDataLists()
        {
            var results = this.Query<DynamicField>(s => s.Id > 0).ToList();
            return results;
        }

        public bool Save(DynamicField order)
        {
            return true;
        }
    }
}
