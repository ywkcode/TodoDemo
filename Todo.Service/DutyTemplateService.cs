using Microsoft.EntityFrameworkCore;
using Todo.Entity;
using Todo.IService;

namespace Todo.Service
{
    public class DutyTemplateService : BaseService, IDutyTemplateService
    {
        public DutyTemplateService(DbContext context) : base(context)
        {
        }

        public bool SaveTemplate(DutyTemplate template)
        {
            this.Insert(template);
            return true;
        }

        public DutyTemplate GetSingle(int Id)
        {
            return this.Find<DutyTemplate>(Id);
        }
    }
}
