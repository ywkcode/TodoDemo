using Todo.API.Context;
using Todo.Shared.Dtos;
using Todo.Shared.Parameters;

namespace Todo.API.Service
{
    public interface IToDoService:IBaseService<ToDoDto>
    {
        Task<ApiResponse> GetAllAsync(ToDoParameter query);

        Task<ApiResponse> Summary();
    }
}
