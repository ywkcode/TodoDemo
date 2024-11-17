using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Service;
using Todo.Shared.Dtos;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService service;
        public LoginController(ILoginService serviceArg)
        {
            service = serviceArg;
        }


        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto model) => await service.LoginAsync(model.Account,model.PassWord);

        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] UserDto model) => await service.Register(model);
    }
}
