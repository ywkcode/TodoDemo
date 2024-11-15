using Todo.API.Context;

namespace Todo.API.Repository
{
    public interface ITodoRepository
    {
        Task<bool> Add(ToDo todo);

        Task<bool> Update(ToDo todo);

        Task<bool> Delete(int id);
    }

    public class ToDoRepository : ITodoRepository
    {
        public Task<bool> Add(ToDo todo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ToDo todo)
        {
            throw new NotImplementedException();
        }
    }
}
