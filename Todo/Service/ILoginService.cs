using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.Contact;
using Todo.Shared.Dtos;

namespace Todo.Service
{
    public interface ILoginService
    {
        Task<ApiResponse<UserDto>> LoginAsync(UserDto user);

        Task<ApiResponse> Register(UserDto user);
    }
}
