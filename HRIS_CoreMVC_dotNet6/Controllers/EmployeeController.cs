using HRIS.API.Controllers;
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


        [HttpGet("edit/{empid}")]
        public async Task<ActionResult<GetEmployeesDto>> EditEmployee(string empid)
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

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag._isError = true;
                ViewBag._message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
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
                return View();
            }
            catch (Exception ex)
            {
                ViewBag._isError = true;
                ViewBag._message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }

        [Route("getsections/{code}")]
        public async Task<IActionResult> FillDepartmentSection(string code)
        {
            var _sections = await Mediator.Send(new GetListofDepartmentalSectionQuery() { });

            var sections = _sections.Where(x => x.DepartmentCode == code);

            return Json(sections);
        }
    }
}
