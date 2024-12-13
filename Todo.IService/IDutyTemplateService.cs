using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity;

namespace Todo.IService
{
    public  interface IDutyTemplateService
    {
        bool SaveTemplate(DutyTemplate template);

        bool UpDateTemplate(DutyTemplate template);

        DutyTemplate GetSingle(int Id);
    }
}
