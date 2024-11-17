using Todo.Shared.Dtos;

namespace Todo.API.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string Account, string Password);

        Task<ApiResponse> Register(UserDto user);
    }
}
