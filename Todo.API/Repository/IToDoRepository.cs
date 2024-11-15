using Todo.API.Context;

namespace Todo.API.Repository
{
    public interface ITodoRepository
    {
        Task<bool> Add(ToDo todo);

        Task<bool> Update(ToDo todo);

        Task<bool> Delete(int id);
    }

     
}
