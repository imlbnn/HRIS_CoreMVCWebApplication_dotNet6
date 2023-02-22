using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Employees.Commands;
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Tuple<bool, string>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(IMapper mapper, ITransactionScopeFactory transactionScopeFactory, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _transactionScopeFactory = transactionScopeFactory;
            _employeeRepository = employeeRepository;
        }

        private async Task<bool> ValidateFields(UpdateEmployeeCommand request)
        {
            if (string.IsNullOrEmpty(request.EmpID))
                throw new ValidationException("Employee ID is required");

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

            if (!request.DateOfBirth.HasValue)
                throw new ValidationException("Date Of Birth is required");

            return await Task.FromResult(true);
        }


        public async Task<Tuple<bool, string>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionScopeFactory.Create())
            {
                await ValidateFields(request);

                var _entity = (await _employeeRepository.
                    GetAllAsync(x=> x.EmpID == request.EmpID)).FirstOrDefault();

                if (_entity == null)
                    throw new NotFoundException("Employee does not exist");

                _entity.LastName = request.LastName;

                _entity.FirstName = request.FirstName;

                _entity.MiddleName = request.MiddleName;

                _entity.DepartmentCode = request.DepartmentCode;

                _entity.DepartmentSectionCode = request.DepartmentSectionCode;

                _entity.DateOfBirth = request.DateOfBirth.Value;

                _entity.CivilStatusCode = request.CivilStatusCode;

                var _employee = _mapper.Map<Employee>(request);

                await _employeeRepository.UpdateAsync(_employee);

                scope.Complete();

                return Tuple.Create(true, "Employee Successfully Updated");
            }
        }
    }
}
