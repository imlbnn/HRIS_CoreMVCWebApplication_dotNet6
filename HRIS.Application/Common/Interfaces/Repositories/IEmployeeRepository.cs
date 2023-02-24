using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Repositories
{
    public interface IEmployeeRepository : IGenericRepositoryAsync<Employee>
    {
        IEmployeeRepository SetOrderBy(string orderBy);
        IEmployeeRepository IncludeDepartment();
        IEmployeeRepository IncludeDepartmentSection();
        IEmployeeRepository IncludeCivilStatus();
    }

    public interface ICustomEmployeeRepository : IGenericRepositoryAsync<CustomEmployee>
    {

    }
}
