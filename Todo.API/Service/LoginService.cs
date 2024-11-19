using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Principal;
using Todo.Api.UnitOfWork;
using Todo.API.Context;
using Todo.Shared.Dtos;
using Todo.Shared.Extensions;

namespace Todo.API.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork workArg,IMapper mapperArg)
        {
            work = workArg;
            mapper = mapperArg;
        }
        public async Task<ApiResponse> LoginAsync(string Account, string Password)
        {
            Password = Password.GetMD5();
            var user =await work.GetRepository<User>().GetFirstOrDefaultAsync(predicate: s => s.PassWord == Password && s.Account == Account);
            if (user is null)
            {
                return new ApiResponse("账号或密码错误，请重试！");
            }
            return new ApiResponse(true, user);
        }

        public async Task<ApiResponse> Register(UserDto user)
        {
            var model = mapper.Map<User>(user);
            model.PassWord = model.PassWord.GetMD5();
            var respository = work.GetRepository<User>();
            var userMol =await work.GetRepository<User>().GetFirstOrDefaultAsync(predicate: s => s.PassWord == model.PassWord && s.Account == model.Account);
            if (userMol != null)
            {
                return new ApiResponse($"当前账号:{user.Account}已存在，请重新注册！");
            }

            model.CreateDate = DateTime.Now; 
            await respository.InsertAsync(model);
            if (await work.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true, model);
            }
            return new ApiResponse("注册失败，请稍后重试！");
        }
    }
}
