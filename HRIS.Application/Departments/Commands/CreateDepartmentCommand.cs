using HRIS.Application.Common.Mappings;
using HRIS.Application.Departments.Dtos.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<CreateDepartmentDto>, IMapTo<Department>
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
