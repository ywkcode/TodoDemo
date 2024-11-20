using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity.System;
using Todo.Models;

namespace Todo.IService
{
    public interface ITodoLoginService
    {
        User LoginAsync(UserDto user);

        bool Register(UserDto user);
    }
}
