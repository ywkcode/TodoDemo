using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared.Contact;
using Todo.Shared.Parameters;

namespace Todo.Service
{
    public interface IBaseService<T> where T:class
    {
        Task<ApiResponse<T>> AddAsync(T entity);

        Task<ApiResponse<T>> UpdateAsync(T entity);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<T>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponse<PagedList<T>>> GetAllAsync(QueryParameter parameter);
    }
}
