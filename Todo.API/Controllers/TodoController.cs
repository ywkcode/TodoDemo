using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.UnitOfWork;
using Todo.API.Context;
using Todo.API.Service;
using Todo.Shared.Dtos;
using Todo.Shared.Parameters;

namespace Todo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
       
        private readonly IToDoService service;
        public ToDoController(IToDoService serviceArg)
        {
            service = serviceArg;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] ToDoParameter param) => await service.GetAllAsnyc(param);


        [HttpGet]
        public async Task<ApiResponse> Get([FromQuery] int id) => await service.GetSingleAsync(id);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto model) => await service.AddAsync(model);
    }
}
