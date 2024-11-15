using Todo.API.Context;
using Todo.Api.UnitOfWork;

namespace Todo.API.Repository
{
    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {
        public MemoRepository(TodoContext dbContext) : base(dbContext)
        {
        }
    }
}