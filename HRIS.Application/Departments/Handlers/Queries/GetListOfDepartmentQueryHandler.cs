using AutoMapper;
using HRIS.Application.Common.Interfaces.Application;
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
    public class GetListOfDepartmentQueryHandler : IRequestHandler<GetListofDepartmentQuery, IEnumerable<GetDepartmentDto>>
    {
        private readonly IMapper _Mapper;
        private readonly IDepartmentRepository _departmentRepository;

        public GetListOfDepartmentQueryHandler(IMapper Mapper, IDepartmentRepository departmentRepository)
        {
            _Mapper = Mapper;
            _departmentRepository = departmentRepository;
        }


        public async Task<IEnumerable<GetDepartmentDto>> Handle(GetListofDepartmentQuery request, CancellationToken cancellationToken)
        {
            var _result = await _departmentRepository.GetAllAsync();

            var _output = _Mapper.Map<IEnumerable<GetDepartmentDto>>(_result);

            return _output;

        }
    }
}
