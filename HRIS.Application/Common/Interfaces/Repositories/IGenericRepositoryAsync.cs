using HRIS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<IReadOnlyList<T>> GetAllJoinedAsync(Expression<Func<T, bool>> predicate);
        //Task<PaginatedList<T>> GetPaginatedJoinedAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
        Task<T> GetByCodeAsync(object code);
        Task<IReadOnlyList<T>> GetByRow(Expression<Func<T, bool>> predicate);

        Task<T> GetByMultipleIdAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetByCodeListAsync(object code);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<PaginatedList<T>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<PaginatedList<T>> GetPaginatedListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
        Task SoftDeleteAsync(T entity);
        Task SoftDeleteAsync(T entity, CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<PaginatedList<T>> GetPaginatedListAsync(Expression<Func<T, bool>> wherePredicate, int pageNumber, int pageSize);
        Task<PaginatedList<T>> GetPaginatedListAsync(Expression<Func<T, bool>> wherePredicate, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<PaginatedList<T>> GetPaginatedListAsync(Expression<Func<T, bool>> fixedWhereExpr, Expression<Func<T, bool>> filterExpr, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
