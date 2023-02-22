using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Departments.Commands;
using HRIS.Application.Employees.Commands;
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Application.Utils;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Handlers.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeDto>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomEmployeeRepository _customEmployeeRepository;

        public CreateEmployeeCommandHandler(IMapper mapper, ITransactionScopeFactory transactionScopeFactory, IEmployeeRepository employeeRepository, ICustomEmployeeRepository customEmployeeRepository)
        {
            _mapper = mapper;
            _transactionScopeFactory = transactionScopeFactory;
            _employeeRepository = employeeRepository;
            _customEmployeeRepository = customEmployeeRepository;
        }

        private async Task<bool> ValidateFields(CreateEmployeeCommand request)
        {
            if (string.IsNullOrEmpty(request.LastName))
                throw new ValidationException("LastName is required");

            if (string.IsNullOrEmpty(request.LastName))
                throw new ValidationException("FirstName is required");

            if (string.IsNullOrEmpty(request.DepartmentCode))
                throw new ValidationException("Department Code is required");

            if (string.IsNullOrEmpty(request.DepartmentSectionCode))
                throw new ValidationException("Department Section Code is required");

            if (string.IsNullOrEmpty(request.CivilStatusCode))
                throw new ValidationException("Civil Status Code is required");
            
            if(!request.DateOfBirth.HasValue)
                throw new ValidationException("Date Of Birth is required");
            
            return await Task.FromResult(true);
        }


        public async Task<CreateEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionScopeFactory.Create())
            {
                await ValidateFields(request);

                var _employee = _mapper.Map<Employee>(request);

                var result = await _employeeRepository.AddAsync(_employee);

                var data = _mapper.Map<CreateEmployeeDto>(result);

                scope.Complete();

                return data;
            }
        }


    }
}

