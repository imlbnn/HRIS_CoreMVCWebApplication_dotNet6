using AutoMapper;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Handlers.Queries
{
    public class GetEmployeeByEmpIDQueryHandler : IRequestHandler<GetEmployeeByEmpIDQuery, GetEmployeesDto>
    {
        private readonly IMapper _Mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByEmpIDQueryHandler(IMapper Mapper, IEmployeeRepository employeeRepository)
        {
            _Mapper = Mapper;
            _employeeRepository = employeeRepository;
        }


        public async Task<GetEmployeesDto> Handle(GetEmployeeByEmpIDQuery request, CancellationToken cancellationToken)
        {
            var _result = await _employeeRepository.GetAllAsync(x => x.EmpID == request.EmpID);

            var _output = _Mapper.Map<GetEmployeesDto>(_result.FirstOrDefault());

            return _output;

        }
    }
}
