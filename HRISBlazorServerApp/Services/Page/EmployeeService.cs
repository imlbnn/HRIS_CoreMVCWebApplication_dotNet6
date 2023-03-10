﻿using AutoMapper;
using HRIS.Application.Common.Models;
using HRIS.Application.Employees.Commands;
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using HRISBlazorServerApp.Interfaces.Services;
using MediatR;
using System.Web.Mvc;

namespace HRISBlazorServerApp.Services.Page
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISender _mediator;
        
        public EmployeeService(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<GetEmployeesDto>> GetEmployees()
        {
            try
            {
                var _result = await _mediator.Send(new GetEmployeesQuery() { });

                return _result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<GetEmployeesDto> GetEmployeeByEmpID(string empid)
        {
            try
            {
                var _result = await _mediator.Send(new GetEmployeeByEmpIDQuery() { EmpID = empid });

                return _result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<bool> CreateEmployee(CreateEmployeeDto request)
        {
            try
            {
                var data = await _mediator.Send(new CreateEmployeeCommand()
                {
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    CivilStatusCode = request.CivilStatusCode,
                    DepartmentCode = request.DepartmentCode,
                    DepartmentSectionCode = request.DepartmentSectionCode,
                    DateOfBirth = request.DateOfBirth
                });
                return data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<Tuple<bool,string>> UpdateEmployee(UpdateEmployeeDto request)
        {
            try
            {
                var data = await _mediator.Send(new UpdateEmployeeCommand()
                {
                    EmpID = request.EmpID,
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    DepartmentCode = request.DepartmentCode,
                    DepartmentSectionCode = request.DepartmentSectionCode,
                    DateOfBirth = request.DateOfBirth,
                    CivilStatusCode = request.CivilStatusCode
                });

                return data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


        public async Task<Tuple<bool,string>> ArchiveEmployee(string empid)
        {
            try
            {
                var _results = await _mediator.Send(new DeleteEmployeeCommand { EmpID = empid });
                return _results;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


    }
}