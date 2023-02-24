using HRIS.Application.Common.Extensions;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Common.Interfaces.Services;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core.Exceptions;
using System.Linq.Dynamic.Core;

namespace HRIS.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepositoryAsync<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext,
                                  IDateTime dateTimeService,
                                  ICurrentUserService currentUserService)
                                  : base(dbContext, dateTimeService, currentUserService)
        {
            SetGetQuery(dbContext.Set<Employee>().Where(x => x.IsDeleted == false));
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
            try
            {
                var _orderBy = orderBy.HandleNullableOrderBy();
                if (!string.IsNullOrEmpty(_orderBy))
                    SetGetQuery(GetQuery.OrderBy(_orderBy));
            }
            catch (ParseException ex)
            {
                throw;
            }

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
