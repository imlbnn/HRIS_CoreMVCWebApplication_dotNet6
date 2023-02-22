using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Commands
{
    public class DeleteEmployeeCommand : IRequest<Tuple<bool,string>>, IMapTo<Employee>
    {
        public string EmpID { get; set; }
    }
}
