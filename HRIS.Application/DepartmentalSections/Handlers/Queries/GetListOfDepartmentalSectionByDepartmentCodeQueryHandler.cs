using AutoMapper;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.DepartmentalSections.Dtos.Queries;
using HRIS.Application.DepartmentalSections.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.DepartmentalSections.Handlers.Queries
{
    public class GetListOfDepartmentalSectionByDepartmentCodeQueryHandler : IRequestHandler<GetListOfDepartmentalSectionByDepartmentCodeQuery, IEnumerable<GetDepartmentSectionDto>>
    {
        private readonly IMapper _Mapper;
        private readonly IDepartmentalSectionRepository _departmentalSectionRepository;

        public GetListOfDepartmentalSectionByDepartmentCodeQueryHandler(IMapper Mapper, IDepartmentalSectionRepository departmentalSectionRepository)
        {
            _Mapper = Mapper;
            _departmentalSectionRepository = departmentalSectionRepository;
        }

        public async Task<IEnumerable<GetDepartmentSectionDto>> Handle(GetListOfDepartmentalSectionByDepartmentCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _result = (await _departmentalSectionRepository.GetAllAsync())
                                .Where(x=> x.DepartmentCode == request.departmentCode);

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
