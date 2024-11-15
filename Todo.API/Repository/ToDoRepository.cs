using Todo.API.Context;
using Todo.Api.UnitOfWork;

namespace Todo.API.Repository
{
    public class ToDoRepository : Repository<ToDo>, IRepository<ToDo>
    {
        public ToDoRepository(TodoContext dbContext) : base(dbContext)
        {
        }
    }
}
