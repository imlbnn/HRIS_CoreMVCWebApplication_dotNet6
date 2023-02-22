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
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace HRIS.Application.Departments.Handlers.Commands
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Tuple<bool, string>>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITransactionScopeFactory _transactionScopeFactory;

        public UpdateDepartmentCommandHandler(IMapper mapper,
            IDepartmentRepository departmentRepository,
            ITransactionScopeFactory transactionScopeFactory)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _transactionScopeFactory = transactionScopeFactory;
        }

        private async Task<bool> ValidateFields(UpdateDepartmentCommand request)
        {
            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Code is required");

            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Description is required");

            return await Task.FromResult(true);
        }


        public async Task<Tuple<bool,string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionScopeFactory.Create())
            {
                await ValidateFields(request);

                var _entity = await _departmentRepository.GetByCodeAsync(request.Code);

                if (_entity == null)
                    throw new NotFoundException("Department does not exist");

                _entity.Description = request.Description;

                await _departmentRepository.UpdateAsync(_entity);

                scope.Complete();

                return Tuple.Create(true, "Department has been updated");
            }
        }
    }
}
