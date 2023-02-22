using AutoMapper;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.DepartmentalSections.Dtos.Queries;
using HRIS.Application.DepartmentalSections.Queries;
using HRIS.Application.Departments.Dtos;
using HRIS.Application.Departments.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.DepartmentalSections.Handlers.Queries
{
    public class GetListofDepartmentalSectionQueryHandler : IRequestHandler<GetListofDepartmentalSectionQuery, IEnumerable<GetDepartmentSectionDto>>
    {
        private readonly IMapper _Mapper;
        private readonly IDepartmentalSectionRepository _departmentalSectionRepository;

        public GetListofDepartmentalSectionQueryHandler(IMapper Mapper, IDepartmentalSectionRepository departmentalSectionRepository)
        {
            _Mapper = Mapper;
            _departmentalSectionRepository = departmentalSectionRepository;
        }


        public async Task<IEnumerable<GetDepartmentSectionDto>> Handle(GetListofDepartmentalSectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = await _departmentalSectionRepository.GetAllAsync();

                var _output = _Mapper.Map<IEnumerable<GetDepartmentSectionDto>>(_result);

                return _output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
