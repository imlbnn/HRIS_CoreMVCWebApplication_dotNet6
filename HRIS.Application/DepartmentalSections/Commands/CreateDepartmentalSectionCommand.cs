using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.DepartmentalSections.Commands
{
    public class CreateDepartmentalSectionCommand : IRequest<Tuple<bool, string>>, IMapTo<DepartmentalSection>
    {
        public string DepartmentCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
