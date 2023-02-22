using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Services
{
    public interface IDataServiceBase<TData, TValueId> : IDataServiceBase<TData, TData, TValueId>
        where TData : class
    {
    }

    public interface IDataServiceBase<TInput, TOuput, TValueId>
        where TInput : class
        where TOuput : class
    {
        Task<IEnumerable<TOuput>> GetAllAsync();
        Task<TOuput> GetByIdAsync(TValueId id);
        Task<TOuput> AddAsync(TInput model);
        Task<TOuput> UpdateAsync(TInput model);
        Task DeleteItemAsync(TValueId id);
    }
}
