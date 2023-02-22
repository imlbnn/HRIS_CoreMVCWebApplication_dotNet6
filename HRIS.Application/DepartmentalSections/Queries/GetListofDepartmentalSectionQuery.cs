using HRIS.Application.DepartmentalSections.Dtos.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.DepartmentalSections.Queries
{
    public class GetListofDepartmentalSectionQuery : IRequest<IEnumerable<GetDepartmentSectionDto>>
    {
    }
}
