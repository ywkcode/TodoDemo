﻿using Todo.Shared.Contact;
using Todo.Shared.Parameters;

namespace Todo.API.Service
{
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsnyc(QueryParameter query);

        Task<ApiResponse> GetSingleAsync(int id);

        Task<ApiResponse> AddAsync(T model);

        Task<ApiResponse> UpdateAsync(T model);

        Task<ApiResponse> DeleteAsync(int id);
    }
}
