using Microsoft.EntityFrameworkCore;
using Todo.Base.Extentions;
using Todo.Entity.System;
using Todo.IService;
using Todo.Models;

namespace Todo.Service
{
    public class TodoLoginService : BaseService, ITodoLoginService
    {
        public TodoLoginService(DbContext context)
          : base(context)
        {

        }

        public User LoginAsync(UserDto user)
        {

            var data = this.Query<User>(s => s.Account == user.Account).ToList().FirstOrDefault();
            return data ;
        }

        public bool Register(UserDto user)
        {
            return true;
        }
    }
}
