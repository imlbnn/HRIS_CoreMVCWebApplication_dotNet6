﻿using HRIS.API.Controllers;
using HRIS.Application.Common.Security;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using HRIS.Domain.Entities;
using HRIS_CoreMVC_dotNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using HRIS.Application.Departments.Queries;
using HRIS.Application.DepartmentalSections.Queries;
using HRIS.Application.Departments.Dtos.Queries;
using Newtonsoft.Json;
using MediatR;
using HRIS.Application.CivilStatuses.Queries;
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Application.Employees.Commands;

namespace HRIS_CoreMVC_dotNet6.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class EmployeeController : ApiControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                if (!await VerifySession())
                    return View("../Account/Login");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag._isError = true;
                ViewBag._message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }


        [Route("displayall")]
        public async Task<IActionResult> GetAllEmployees(JqueryDatatableParam param)
        {
            var _result = await Mediator.Send(new GetEmployeesQuery() { });

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                _result = _result.Where(x => x.FirstName.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
            }

            var sortColumnIndex = param.iSortCol_0;

            var sortDirection = param.sSortDir_0;

            if (sortColumnIndex == 2)
            {
                _result = sortDirection == "asc" ? _result.OrderBy(c => c.FirstName) : _result.OrderByDescending(c => c.FirstName);
            }
            else
            {
                Func<GetEmployeesDto, string> orderingFunction = e =>

                                    sortColumnIndex == 0 ? e.EmpID :
                                    sortColumnIndex == 1 ? e.LastName :
                                    e.EmpID;

                _result = sortDirection == "asc" ? _result.OrderBy(orderingFunction) : _result.OrderByDescending(orderingFunction);
            }


            var displayResult = _result.Skip(param.iDisplayStart)
            .Take(param.iDisplayLength).ToList();
            var totalRecords = _result.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }



        [HttpGet("{empid}")]
        public async Task<ActionResult<GetEmployeesDto>> ViewEmployee(string empid)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                GetEmployeesDto model = new GetEmployeesDto();

                //TempData["__Departments"] = await Mediator.Send(new GetListofDepartmentQuery() { });
                //TempData["__DepartmentalSection"] = await Mediator.Send(new GetListofDepartmentalSectionQuery() { });
                //TempData["__CivilStatuses"] = await Mediator.Send(new GetListofDepartmentalSectionQuery() { });

                model = await Mediator.Send(new GetEmployeeByEmpIDQuery() { EmpID = empid });

                if (!await VerifySession())
                    return View("../Account/Login");

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag._isError = true;
                ViewBag._message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }


        [HttpGet("create")]
        public async Task<ActionResult> CreateEmployee()
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (!await VerifySession())
                    return View("../Account/Login");

                var _departments = await Mediator.Send(new GetListofDepartmentQuery() { });
                var _civilStatuses = await Mediator.Send(new GetListOfCivilStatusQuery() { });

                TempData["__Departments"] = System.Text.Json.JsonSerializer.Serialize(_departments);

                TempData["__CivilStatuses"] = System.Text.Json.JsonSerializer.Serialize(_civilStatuses);

                return View();
            }
            catch (Exception ex)
            {
                ViewBag._isError = true;
                ViewBag._message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateEmployee(CreateEmployeeDto _model)
        {
            try
            {
                if (!await VerifySession())
                    return View("../Account/Login");

                await Mediator.Send(new CreateEmployeeCommand()
                {
                    LastName = _model.LastName,
                    FirstName = _model.FirstName,
                    MiddleName = _model.MiddleName,
                    CivilStatusCode = _model.CivilStatusCode,
                    DepartmentCode = _model.DepartmentCode,
                    DepartmentSectionCode = _model.DepartmentSectionCode,
                    DateOfBirth = _model.DateOfBirth
                }) ;

                TempData["IsHasError"] = false;
                TempData["Message"] = "New employee has been created";

                return RedirectToAction("");
            }
            catch (Exception ex)
            {
                TempData["IsHasError"] = true;
                TempData["Message"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View(_model);
            }
        }



        [HttpGet("edit/{empid}")]
        public async Task<ActionResult> EditEmployee(string empid)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (!await VerifySession())
                    return View("../Account/Login");

                GetEmployeesDto model = new GetEmployeesDto();

                var _departments = await Mediator.Send(new GetListofDepartmentQuery() { });
                var _sections = await Mediator.Send(new GetListofDepartmentalSectionQuery() { });
                var _civilStatuses = await Mediator.Send(new GetListOfCivilStatusQuery() { });

                TempData["__Departments"] = System.Text.Json.JsonSerializer.Serialize(_departments);

                model = await Mediator.Send(new GetEmployeeByEmpIDQuery() { EmpID = empid });

                TempData["__DepartmentalSection"] = System.Text.Json.JsonSerializer.Serialize(_sections.Where(x=>x.DepartmentCode == model.Department.Code));

                TempData["__CivilStatuses"] = System.Text.Json.JsonSerializer.Serialize(_civilStatuses);
                
                TempData["IsHasError"] = false;
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["IsHasError"] = true;
                TempData["Message"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }


        [HttpPost("edit/{empid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmployee(string empid, GetEmployeesDto model)
        {
            try
            {
                await Mediator.Send(new UpdateEmployeeCommand()
                {
                    EmpID = empid,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    DepartmentCode = model.Department.Code,
                    DepartmentSectionCode = model.DepartmentSection.Code,
                    DateOfBirth = model.DateOfBirth,
                    CivilStatusCode = model.CivilStatus.Code
                });

                TempData["IsHasError"] = false;
                TempData["Message"] = "Employee has been updated";

                return RedirectToAction("");
            }
            catch (Exception ex)
            {
                TempData["IsHasError"] = true;
                TempData["Message"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View(model);
            }
        }

        [Route("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public IActionResult ReturnToEmployeeMain()
        {
            try
            {
                TempData["IsHasError"] = false;
                return View();
            }
            catch (Exception ex)
            {
                TempData["IsHasError"] = "true";
                TempData["Message"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }

        
    }
}
