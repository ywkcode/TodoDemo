using AutoMapper;
using AutoMapper.Configuration;
using Todo.API.Context;
using Todo.Shared.Dtos;
namespace Todo.API.Extensions
{
    public class AutoMapperProFile : MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
