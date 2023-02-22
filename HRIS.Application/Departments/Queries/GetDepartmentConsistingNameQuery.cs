using HRIS.Application.Departments.Dtos.Queries;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Departments.Queries
{
    public class GetDepartmentConsistingNameQuery : IRequest<IEnumerable<GetDepartmentDto>>
    {
        public string DepartmentName { get; set; }
    }
}
