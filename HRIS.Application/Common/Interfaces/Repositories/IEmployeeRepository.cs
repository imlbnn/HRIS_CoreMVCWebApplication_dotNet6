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
        
    }

    public interface ICustomEmployeeRepository : IGenericRepositoryAsync<CustomEmployee>
    {

    }
}
