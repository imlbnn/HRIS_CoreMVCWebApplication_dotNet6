using HRIS.Application.CivilStatuses.Dtos;
using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.CivilStatuses.Commands
{
    public class CreateCivilStatusCommand : IRequest<Tuple<bool, string>>, IMapTo<CivilStatus>
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
