﻿using AutoMapper;
using HRIS.Application.Common.Extensions;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Common.Models;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using HRIS.Domain.Entities;
using LinqKit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Handlers.Queries
{
    internal class GetEmployeePaginatedQueryHandler : IRequestHandler<GetEmployeePaginatedQuery, PaginatedList<GetEmployeesDto>>
    {
        private readonly IMapper _Mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeePaginatedQueryHandler(IMapper Mapper, IEmployeeRepository employeeRepository)
        {
            _Mapper = Mapper;
            _employeeRepository = employeeRepository;
        }


        public async Task<PaginatedList<GetEmployeesDto>> Handle(GetEmployeePaginatedQuery request, CancellationToken cancellationToken)
        {

            /*
            //Use this format if returning IEnumerable List
            //var _result = await _employeeRepository.GetAllAsync();
            var _output = _Mapper.Map<IEnumerable<GetEmployeesDto>>(result);
            */
            var _wherePredicate = PredicateBuilder.New<Employee>();

            var _searchKeys = request.SearchKey.SplitSearchKeys();
            if (_searchKeys != null && _searchKeys.Any())
            {
                foreach (var _searchKey in _searchKeys)
                {
                    _wherePredicate = _wherePredicate
                        .And(x =>
                            x.LastName.Contains(_searchKey)
                            || x.FirstName.Contains(_searchKey)
                            || x.MiddleName.Contains(_searchKey)
                            );
                }
            }

            if (!_wherePredicate.IsStarted)
                _wherePredicate.And(x => true);

            var result = await _employeeRepository
                .IncludeDepartment()
                .IncludeDepartmentSection()
                .IncludeCivilStatus()
                .SetOrderBy(request.OrderBy)
                .GetPaginatedListAsync(_wherePredicate,
                                        request.PageNumber,
                                        request.PageSize,
                                        cancellationToken);

            var totalCount = await _employeeRepository
                .IncludeDepartment()
                .IncludeDepartmentSection()
                .IncludeCivilStatus()
                .SetOrderBy(request.OrderBy)
                .GetAllAsync();

            var data = _Mapper.Map<IEnumerable<GetEmployeesDto>>(result.Items);

            var paginatedList = new PaginatedList<GetEmployeesDto>(data.ToList(), totalCount.Count, request.PageNumber, request.PageSize); ;

            return paginatedList;

        }
    }
}
