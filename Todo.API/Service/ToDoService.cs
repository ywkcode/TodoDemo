using AutoMapper;
using System.Data.Common;
using System.Reflection.Metadata;
using Todo.Api.UnitOfWork;
using Todo.API.Context;
using Todo.Shared.Contact;
using Todo.Shared.Dtos;
using Todo.Shared.Parameters;

namespace Todo.API.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;
        public ToDoService(IUnitOfWork workArg, IMapper mapperArg)
        {
            work = workArg;
            mapper = mapperArg;
        }
        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            var todo = mapper.Map<ToDo>(model);
            await work.GetRepository<ToDo>().InsertAsync(todo);
            if (await work.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true,model);
            }
            return new ApiResponse("添加数据库失败");
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todo = await repository
                    .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsnyc(QueryParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todos = await repository.GetPagedListAsync(predicate:
                x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Title.Contains(parameter.Search),
                pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(ToDoParameter query)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todos = await repository.GetPagedListAsync();
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public Task<ApiResponse> Summary()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            throw new NotImplementedException();
        }
    }
}
