using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Common.Interfaces.Services;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepositoryAsync<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext, 
                                  IDateTime dateTimeService, 
                                  ICurrentUserService currentUserService) 
                                  : base(dbContext, dateTimeService, currentUserService)
        {
            SetGetQuery(dbContext.Set<Employee>()
                .Include(a=> a.Department)
                .Include(a=> a.DepartmentSection)
                .Include(a=> a.CivilStatus)
                .Where(x=> x.IsDeleted == false));
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
