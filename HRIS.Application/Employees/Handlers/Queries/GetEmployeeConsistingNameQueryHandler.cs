using AutoMapper;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Handlers.Queries
{
    public class GetEmployeeConsistingNameQueryHandler : IRequestHandler<GetEmployeeConsistingNameQuery, IEnumerable<GetEmployeesDto>>
    {
        private readonly IMapper _Mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeConsistingNameQueryHandler(IMapper Mapper, IEmployeeRepository employeeRepository)
        {
            _Mapper = Mapper;
            _employeeRepository = employeeRepository;
        }


        public async Task<IEnumerable<GetEmployeesDto>> Handle(GetEmployeeConsistingNameQuery request, CancellationToken cancellationToken)
        {
            var data = await _employeeRepository.GetAllAsync
                             (x => (x.FirstName + " " + x.LastName).Contains(request.Name) ||
                             (x.FirstName + " " + x.MiddleName + " " + x.LastName).Contains(request.Name)
                             );

            var _output = _Mapper.Map<IEnumerable<GetEmployeesDto>>(data);

            return _output;

        }
    }
}
