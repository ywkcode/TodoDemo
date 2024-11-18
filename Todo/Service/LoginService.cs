using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.Contact;
using Todo.Shared.Dtos;

namespace Todo.Service
{
    public class LoginService : BaseService<UserDto>, ILoginService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "Login";

        public LoginService(HttpRestClient client) : base(client, "ToDo")
        {
            this.client = client;
        }
        public async Task<ApiResponse> LoginAsync(UserDto user)
        {
            BaseRequest request=new BaseRequest();
            request.Method=RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/Login";
            request.Parameter = user;
            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse> Register(UserDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/Register";
            request.Parameter = user;
            return await client.ExecuteAsync(request);
        }
    }
}
