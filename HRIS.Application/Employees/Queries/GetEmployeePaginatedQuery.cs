using HRIS.Application.Common.Models;
using HRIS.Application.Employees.Dtos.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Queries
{
    public class GetEmployeePaginatedQuery : IRequest<PaginatedList<GetEmployeesDto>>
    {
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
