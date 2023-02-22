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
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<GetEmployeesDto>>
    {
        private readonly IMapper _Mapper;
        private readonly IEmployeeRepository _employeeRepository;
        
        public GetEmployeesQueryHandler(IMapper Mapper,IEmployeeRepository employeeRepository)
        {
            _Mapper = Mapper;
            _employeeRepository = employeeRepository;
        }


        public async Task<IEnumerable<GetEmployeesDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _employeeRepository.GetAllAsync();

                var _output = _Mapper.Map<IEnumerable<GetEmployeesDto>>(_result);

                return _output;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
