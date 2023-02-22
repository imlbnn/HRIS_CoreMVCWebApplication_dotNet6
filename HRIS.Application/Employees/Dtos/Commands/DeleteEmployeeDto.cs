using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Dtos.Commands
{
    public class DeleteEmployeeDto : IMapFrom<Employee>
    {
        public string EmpID { get; set; }
    }
}
