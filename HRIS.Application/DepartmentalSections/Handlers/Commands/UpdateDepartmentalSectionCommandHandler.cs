using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.DepartmentalSections.Commands;
using HRIS.Application.Departments.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace HRIS.Application.DepartmentalSections.Handlers.Commands
{
    public class UpdateDepartmentalSectionCommandHandler : IRequestHandler<UpdateDepartmentalSectionCommand, Tuple<bool, string>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IDepartmentalSectionRepository _departmentalSectionRepository;


        public UpdateDepartmentalSectionCommandHandler(IMapper mapper, ITransactionScopeFactory transactionScopeFactory, IDepartmentalSectionRepository departmentalSectionRepository)
        {
            _mapper = mapper;
            _transactionScopeFactory = transactionScopeFactory;
            _departmentalSectionRepository = departmentalSectionRepository;
        }


        private async Task<bool> ValidateFields(UpdateDepartmentalSectionCommand request)
        {
            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Code is required");

            if (string.IsNullOrEmpty(request.DepartmentCode))
                throw new ValidationException("Department Code is required");

            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Description is required");

            return await Task.FromResult(true);
        }

        public async Task<Tuple<bool, string>> Handle(UpdateDepartmentalSectionCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionScopeFactory.Create())
            {
                await ValidateFields(request);

                var _entity = (await _departmentalSectionRepository
                                .GetAllAsync(x => x.Code == request.Code &&
                                             x.DepartmentCode == request.DepartmentCode))
                                             .FirstOrDefault();

                if (_entity == null)
                    throw new NotFoundException("Departmental Section is not exist");


                _entity.Description = request.Description;

                await _departmentalSectionRepository.UpdateAsync(_entity);

                scope.Complete();

                return Tuple.Create(true, "Departmental Section Successfully Updated");
            }
        }
    }
}
