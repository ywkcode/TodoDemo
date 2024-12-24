using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity;

namespace Todo.IService
{
    public interface IDynamicFieldService
    {
        List<DynamicField> GetDataLists(); 
        bool Save(DynamicField order);
    }
}
