using AutoMapper;
using HRIS.Application.CivilStatuses.Commands;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Departments.Commands;
using HRIS.Application.Departments.Dtos.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.CivilStatuses.Handlers.Commands
{
    public class CreateCivilStatusCommandHandler : IRequestHandler<CreateCivilStatusCommand, Tuple<bool, string>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly ICivilStatusRepository _civilStatusRepository;

        public CreateCivilStatusCommandHandler(IMapper mapper, ITransactionScopeFactory transactionScopeFactory, ICivilStatusRepository civilStatusRepository) 
        {
            _mapper = mapper;
            _transactionScopeFactory = transactionScopeFactory;
            _civilStatusRepository= civilStatusRepository;
        }

        private async Task<bool> ValidateFields(CreateCivilStatusCommand request)
        {
            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Code is required");

            if (string.IsNullOrEmpty(request.Code))
                throw new ValidationException("Description is required");

            return await Task.FromResult(true);
        }

        public async Task<Tuple<bool, string>> Handle(CreateCivilStatusCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionScopeFactory.Create())
            {
                await ValidateFields(request);

                var isExists = await _civilStatusRepository.GetAllAsync(x => x.Code == request.Code && x.IsDeleted == false);

                if (isExists.Any())
                    throw new ValidationException("Civil Status already registered.");

                var _civilStat = _mapper.Map<CivilStatus>(request);

                await _civilStatusRepository.AddAsync(_civilStat);

                scope.Complete();

                return Tuple.Create(true, "Civil Status Successfully registered");
            }
        }
    }
}
