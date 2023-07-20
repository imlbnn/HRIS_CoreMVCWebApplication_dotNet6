using HRIS.Application.Common.Extensions;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Common.Interfaces.Services;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core.Exceptions;
using System.Linq.Dynamic.Core;
using System.Data.SqlClient;

namespace HRIS.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepositoryAsync<Employee>, IEmployeeRepository
    {
        private ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext,
                                  IDateTime dateTimeService,
                                  ICurrentUserService currentUserService)
                                  : base(dbContext, dateTimeService, currentUserService)
        {
            _dbContext = dbContext;
            SetGetQuery(dbContext.Set<Employee>()
                .Where(x => x.IsDeleted == false)
                );
        }

        public IEmployeeRepository IncludeCivilStatus()
        {
            SetGetQuery(GetQuery.Include(t => t.CivilStatus));
            return this;
        }

        public IEmployeeRepository IncludeDepartment()
        {
            SetGetQuery(GetQuery.Include(t => t.Department));
            return this;
        }

        public IEmployeeRepository IncludeDepartmentSection()
        {
            SetGetQuery(GetQuery.Include(t => t.DepartmentSection));
            return this;
        }

        public IEmployeeRepository SetOrderBy(string orderBy)
        {
            var _orderBy = orderBy.HandleNullableOrderBy();
            
            if (!string.IsNullOrEmpty(_orderBy))
                SetGetQuery(GetQuery.OrderBy(_orderBy));

            return this;
        }

    }

    public class CustomEmployeeRepository : GenericRepositoryAsync<CustomEmployee>, ICustomEmployeeRepository
    {
        public CustomEmployeeRepository(ApplicationDbContext dbContext, IDateTime dateTimeService
            , ICurrentUserService currentUserService
            ) : base(dbContext, dateTimeService
                , currentUserService
                )
        {
            SetGetQuery(dbContext.Set<CustomEmployee>());
        }
    }
}
