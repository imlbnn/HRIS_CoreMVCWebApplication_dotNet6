
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Common.Interfaces.Services;
using HRIS.Application.Common.Models;
using HRIS.Domain.Common;
using HRIS.Domain.Common.Interfaces;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Infrastructure
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly IDateTime _dateTime;
        private IQueryable<T> _getQuery;
        private readonly ICurrentUserService _currentUserService;

        public GenericRepositoryAsync(DbContext dbContext, IDateTime dateTimeService
            , ICurrentUserService currentUserService
            )
        {
            _dbContext = dbContext;
            _dateTime = dateTimeService;
            _currentUserService = currentUserService;
        }

        public void SetGetQuery(IQueryable<T> query)
        {
            _getQuery = query;
        }

        public IQueryable<T> GetQuery
        {
            get
            {
                return _getQuery ?? _dbContext.Set<T>();
            }
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            var _result = await _dbContext.Set<T>().FindAsync(id);
            return _result;
        }

        public virtual async Task<IReadOnlyList<T>> GetAllJoinedAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAllJoinedAsync(predicate); ;
        }

        public virtual async Task<PaginatedList<T>> GetPaginatedJoinedAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            var _result = await GetPaginatedJoinedAsync(predicate, pageNumber, pageSize);
            return _result;
        }

        public virtual async Task<IReadOnlyList<T>> GetByRow(Expression<Func<T, bool>> predicate)
        {
            return await GetByRow(predicate);
        }

        public virtual async Task<T> GetByCodeAsync(object code)
        {
            return await _dbContext.Set<T>().FindAsync(code);
        }

        public virtual async Task<IReadOnlyList<T>> GetByCodeListAsync(object code)
        {
            return await _dbContext.Set<IReadOnlyList<T>>().FindAsync(code);
        }

        public virtual async Task<T> GetByMultipleIdAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await GetQuery
                  .Where(predicate)
                  .FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var _result = await AddAsync(entity, CancellationToken.None);
            return _result;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await UpdateAsync(entity, CancellationToken.None);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await DeleteAsync(entity, CancellationToken.None);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await GetAllAsync(CancellationToken.None);
            }
            catch(Exception ex) { throw ex; }
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            UpdateAuditEntities();  
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
       

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            UpdateAuditEntities();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Remove(entity);
            UpdateAuditEntities();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await GetQuery.ToListAsync(cancellationToken);
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<PaginatedList<T>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            return await GetPaginatedListAsync(pageNumber, pageSize, CancellationToken.None);
        }

        public async Task<PaginatedList<T>> GetPaginatedListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var _query = GetQuery;

            var _count = await _query.CountAsync();
            var _items = await _query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var _pagedResult = new PaginatedList<T>(_items, _count, pageNumber, pageSize);

            return _pagedResult;
        }

        public virtual async Task SoftDeleteAsync(T entity)
        {
            await SoftDeleteAsync(entity, CancellationToken.None);
        }

        public virtual async Task SoftDeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            if (!entity.GetType().IsSubclassOf(typeof(SoftDeletableEntity)))
            {
                throw new ArgumentException("Soft deletion works only for SoftDeletableEntity types", "entity");
            }

            var _softDeletableEntity = entity as SoftDeletableEntity;
            _softDeletableEntity.IsDeleted = true;
            _softDeletableEntity.DeletedDate = _dateTime.Now;
            _softDeletableEntity.DeletedBy = _currentUserService.Username;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAllAsync(predicate, CancellationToken.None);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await GetQuery
                 .Where(predicate)
                 .ToListAsync(cancellationToken);
        }

        public async Task<PaginatedList<T>> GetPaginatedListAsync(Expression<Func<T, bool>> wherePredicate, int pageNumber, int pageSize)
        {
            return await GetPaginatedListAsync(wherePredicate, pageNumber, pageSize, CancellationToken.None);
        }

        public async Task<PaginatedList<T>> GetPaginatedListAsync(Expression<Func<T, bool>> wherePredicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var _query = GetQuery.Where(wherePredicate);
            var _count = await _query.CountAsync();
            var _skip = (pageNumber - 1) * pageSize;
            var _list = await _query.Skip(_skip).Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var _pagedResult = new PaginatedList<T>(_list, _count, pageNumber, pageSize);
            return _pagedResult;
        }

        public async Task<PaginatedList<T>> GetPaginatedListAsync(Expression<Func<T, bool>> fixedWhereExpr, Expression<Func<T, bool>> filterExpr, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var _query = GetQuery.Where(fixedWhereExpr);
            var _totalCount = await _query.CountAsync();

            _query = _query.Where(filterExpr);
            var _filteredCount = await _query.CountAsync();

            var _skip = (pageNumber - 1) * pageSize;
            var _list = await _query.Skip(_skip).Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var _pagedResult = new PaginatedList<T>(_list, _totalCount, _filteredCount, pageNumber, pageSize);
            return _pagedResult;
        }

        private void UpdateAuditEntities()
        {
            var modifiedEntries = _dbContext.ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries)
            {

                var entity = (IAuditableEntity)entry.Entity;
                DateTime now = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.DateCreated = now;
                    entity.CreatedBy = _currentUserService.Username;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.DateModified = now;
                        entity.ModifiedBy =  _currentUserService.Username;
                }
            }
        }
    }
}
