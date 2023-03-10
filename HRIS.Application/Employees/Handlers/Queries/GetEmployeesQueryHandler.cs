using AutoMapper;
using HRIS.Application.Common.Extensions;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Common.Models;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using HRIS.Domain.Entities;
using LinqKit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Handlers.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<GetEmployeesDto>>
    {
        private readonly IMapper _Mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesQueryHandler(IMapper Mapper, IEmployeeRepository employeeRepository)
        {
            _Mapper = Mapper;
            _employeeRepository = employeeRepository;
        }


        public async Task<IEnumerable<GetEmployeesDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository
                .IncludeDepartment()
                .IncludeDepartmentSection()
                .IncludeCivilStatus().GetAllAsync();

            var data = _Mapper.Map<IEnumerable<GetEmployeesDto>>(result);

            return data;

        }
    }
}
