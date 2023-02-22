using HRIS.Application.CivilStatuses.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.CivilStatuses.Queries
{
    public class GetListOfCivilStatusQuery : IRequest<IEnumerable<GetCivilStatusDto>>
    {
    }
}
