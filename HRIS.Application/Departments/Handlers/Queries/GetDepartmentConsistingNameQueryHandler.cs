using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Factories;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Departments.Dtos.Queries;
using HRIS.Application.Departments.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Departments.Handlers.Queries
{
    public class GetDepartmentConsistingNameQueryHandler : IRequestHandler<GetDepartmentConsistingNameQuery, IEnumerable<GetDepartmentDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IDepartmentRepository _departmentRepository;


        public GetDepartmentConsistingNameQueryHandler(IMapper mapper, ITransactionScopeFactory transactionScopeFactory, IDepartmentRepository departmentRepository)
        {
            _mapper= mapper;
            _transactionScopeFactory= transactionScopeFactory;
            _departmentRepository= departmentRepository;
        }
        
        public async Task<IEnumerable<GetDepartmentDto>> Handle(GetDepartmentConsistingNameQuery request, CancellationToken cancellationToken)
        {
            var lstDepartment = await _departmentRepository.GetAllAsync(x=> x.Description.ToLower().Contains(request.DepartmentName.ToLower()));

            if (!lstDepartment.Any())
                throw new NotFoundException("No department found on the current filter");

            var data  = _mapper.Map<IEnumerable<GetDepartmentDto>>(lstDepartment);

            return data;
        }
    }
}
