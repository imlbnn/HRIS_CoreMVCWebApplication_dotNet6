using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Departments.Commands
{
    public class DeleteDepartmentCommand : IRequest<Tuple<bool, string>>, IMapTo<Department>
    {
        public string Code { get; set; }
    }
}
