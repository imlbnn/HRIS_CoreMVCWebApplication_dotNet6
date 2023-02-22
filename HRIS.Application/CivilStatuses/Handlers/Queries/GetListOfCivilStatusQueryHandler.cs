using AutoMapper;
using HRIS.Application.CivilStatuses.Dtos;
using HRIS.Application.CivilStatuses.Queries;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.CivilStatuses.Handlers.Queries
{
    public class GetListOfCivilStatusQueryHandler : IRequestHandler<GetListOfCivilStatusQuery, IEnumerable<GetCivilStatusDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly ICivilStatusRepository _civilStatusRepository;

        public GetListOfCivilStatusQueryHandler(IMapper mapper, ITransactionScopeFactory transactionScopeFactory, ICivilStatusRepository civilStatusRepository)
        {
            _mapper = mapper;
            _transactionScopeFactory = transactionScopeFactory;
            _civilStatusRepository = civilStatusRepository;
        }

        public async Task<IEnumerable<GetCivilStatusDto>> Handle(GetListOfCivilStatusQuery request, CancellationToken cancellationToken)
        {
            var data = await _civilStatusRepository.GetAllAsync();

            if (!data.Any())
                throw new NotFoundException("No Civil Status Found");

            var result = _mapper.Map<IEnumerable<GetCivilStatusDto>>(data);

            return result;
        }
    }
}
