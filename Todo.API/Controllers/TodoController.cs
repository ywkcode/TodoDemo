using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.UnitOfWork;
using Todo.API.Context;
using Todo.API.Service;
using Todo.Shared.Dtos;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
       
        private readonly IToDoService service;
        public TodoController(IToDoService serviceArg)
        {
            service = serviceArg;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await service.GetSingleAsync(id);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto model) => await service.AddAsync(model);
    }
}
