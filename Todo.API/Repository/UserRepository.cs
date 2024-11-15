using Todo.Api.UnitOfWork;
using Todo.API.Context;

namespace Todo.API.Repository
{
    public class UserRepository: Repository<User>, IRepository<User>
    {
        public UserRepository(TodoContext dbContext) : base(dbContext)
    {
    }
}
}