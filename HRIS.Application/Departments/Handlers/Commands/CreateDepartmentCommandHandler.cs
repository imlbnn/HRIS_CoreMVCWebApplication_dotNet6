using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Departments.Commands;
using HRIS.Application.Departments.Dtos.Commands;
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Departments.Handlers.Commands
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, CreateDepartmentDto>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentCommandHandler(IMapper mapper,
            ITransactionScopeFactory transactionScopeFactory,
            IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _transactionScopeFactory = transactionScopeFactory;
            _departmentRepository = departmentRepository;
        }

        private async Task<bool> ValidateFields(CreateDepartmentCommand request)
        {
            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Code is required");

            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Description is required");

            return await Task.FromResult(true);
        }

        public async Task<CreateDepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionScopeFactory.Create())
            {
                await ValidateFields(request);

                var isExists = await _departmentRepository.GetAllAsync(x=> x.Code == request.Code && x.IsDeleted == false);

                if (isExists.Any())
                    throw new ValidationException("Department already registered.");

                var _department = _mapper.Map<Department>(request);

                var result = await _departmentRepository.AddAsync(_department);

                var _data = _mapper.Map<CreateDepartmentDto>(result);

                scope.Complete();

                return _data;
            }
        }
    }
}
