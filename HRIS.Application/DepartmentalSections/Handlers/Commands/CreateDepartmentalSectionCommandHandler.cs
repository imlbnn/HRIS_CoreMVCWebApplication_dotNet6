using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.DepartmentalSections.Commands;
using HRIS.Application.Departments.Commands;
using HRIS.Application.Departments.Dtos.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.DepartmentalSections.Handlers.Commands
{
    public class CreateDepartmentalSectionCommandHandler : IRequestHandler<CreateDepartmentalSectionCommand, Tuple<bool, string>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IDepartmentalSectionRepository _departmentalSectionRepository;

        public CreateDepartmentalSectionCommandHandler(IMapper mapper,
            ITransactionScopeFactory transactionScopeFactory,
            IDepartmentalSectionRepository departmentalSectionRepository)
        {
            _mapper = mapper;
            _transactionScopeFactory = transactionScopeFactory;
            _departmentalSectionRepository = departmentalSectionRepository;
        }

        private async Task<bool> ValidateFields(CreateDepartmentalSectionCommand request)
        {
            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Code is required");

            if (string.IsNullOrEmpty(request.DepartmentCode))
                throw new ValidationException("Department Code is required");

            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Description is required");

            return await Task.FromResult(true);
        }

        public async Task<Tuple<bool, string>> Handle(CreateDepartmentalSectionCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionScopeFactory.Create())
            {
                await ValidateFields(request);

                var _entity = await _departmentalSectionRepository.GetAllAsync(x => x.DepartmentCode == request.DepartmentCode && x.Code == request.Code && x.IsDeleted == false);

                if (_entity.Any())
                    throw new ValidationException("Departmental Section already registered.");

                var _departmentalsection = _mapper.Map<DepartmentalSection>(request);

                //var result =
                await _departmentalSectionRepository.AddAsync(_departmentalsection);

                //var _data = _mapper.Map<CreateDepartmentalSectionDto>(result);

                scope.Complete();

                return Tuple.Create(true, "Departmental Section Successfully Created");
            }
        }
    }
}
