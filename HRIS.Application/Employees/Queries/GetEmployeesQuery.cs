using HRIS.Application.Common.Security;
using HRIS.Application.Employees.Dtos.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Queries
{
    public class GetEmployeesQuery : IRequest<IEnumerable<GetEmployeesDto>>
    {
    }
}
