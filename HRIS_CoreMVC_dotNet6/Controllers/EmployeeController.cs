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
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Application.Employees.Commands;
using HRIS.Application.Common.Models;
using AutoMapper;
using static Duende.IdentityServer.Models.IdentityResources;
using NuGet.Protocol;

namespace HRIS_CoreMVC_dotNet6.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class EmployeeController : ApiControllerBase
    {
        private readonly IMapper mapper;
        public EmployeeController(IMapper mapper)
        {
            this.mapper = mapper;
        }

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
            PaginatedList<GetEmployeesDto> _result;
            var sortColumnIndex = param.iSortCol_0;

            var sortDirection = param.sSortDir_0;

            if (sortColumnIndex == 2)
            {
                _result = await Mediator.Send(new GetEmployeesQuery()
                {
                    SearchKey = param.sSearch,
                    OrderBy = "EmpID",
                    PageNumber = (param.iDisplayStart / param.iDisplayLength) + 1,
                    PageSize = param.iDisplayLength,

                });
            }
            else if (sortColumnIndex == 3)
            {
                _result = await Mediator.Send(new GetEmployeesQuery()
                {
                    SearchKey = param.sSearch,
                    OrderBy = "LastName",
                    PageNumber = (param.iDisplayStart / param.iDisplayLength) + 1,
                    PageSize = param.iDisplayLength,

                });
            }
            else if (sortColumnIndex == 4)
            {
                _result = await Mediator.Send(new GetEmployeesQuery()
                {
                    SearchKey = param.sSearch,
                    OrderBy = "FirstName",
                    PageNumber = (param.iDisplayStart / param.iDisplayLength) + 1,
                    PageSize = param.iDisplayLength,

                });
            }

            else if (sortColumnIndex == 5)
            {
                _result = await Mediator.Send(new GetEmployeesQuery()
                {
                    SearchKey = param.sSearch,
                    OrderBy = "MiddleName",
                    PageNumber = (param.iDisplayStart / param.iDisplayLength) + 1,
                    PageSize = param.iDisplayLength,

                });
            }

            else if (sortColumnIndex == 5)
            {
                _result = await Mediator.Send(new GetEmployeesQuery()
                {
                    SearchKey = param.sSearch,
                    OrderBy = "Department.Description",
                    PageNumber = (param.iDisplayStart / param.iDisplayLength) + 1,
                    PageSize = param.iDisplayLength,

                });
            }

            else
            {
                _result = await Mediator.Send(new GetEmployeesQuery()
                {
                    SearchKey = param.sSearch,
                    OrderBy = "LastName",
                    PageNumber = (param.iDisplayStart / param.iDisplayLength) + 1,
                    PageSize = param.iDisplayLength,

                });
            }



            /*
                     //Use This if you are returning IEnumerable List
                      if (!string.IsNullOrEmpty(param.sSearch))
                        {
                            _result = _result.Where(x =>

                            x.FirstName.ToLower().Contains(param.sSearch.ToLower()) ||
                            x.MiddleName.ToLower().Contains(param.sSearch.ToLower()) ||
                            x.LastName.ToLower().Contains(param.sSearch.ToLower())

                                                   ).ToList();
                        }

                        var sortColumnIndex = param.iSortCol_0;

                        var sortDirection = param.sSortDir_0;

                        if (sortColumnIndex == 1)
                        {
                            _result = sortDirection == "asc" ? _result.OrderBy(c => c.LastName) : _result.OrderByDescending(c => c.LastName);
                        }
                        else
                        {
                            Func<GetEmployeesDto, string> orderingFunction = e =>

                                                sortColumnIndex == 0 ? e.EmpID :
                                                sortColumnIndex == 1 ? e.LastName :
                                                sortColumnIndex == 2 ? e.FirstName :
                                                sortColumnIndex == 3 ? e.MiddleName :
                                                e.LastName;

                            _result = sortDirection == "asc" ? _result.OrderBy(orderingFunction) : _result.OrderByDescending(orderingFunction);
                        }


                        var displayResult = _result.Skip(param.iDisplayStart)
                        .Take(param.iDisplayLength).ToList();
            */

            var totalRecords = _result.TotalCount;
            var totalFiltered = _result.ItemCount;

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalFiltered,
                aaData = _result.Items,
                res = _result.Items
            });
        }



        [HttpGet("{empid}")]
        public async Task<ActionResult<GetEmployeesDto>> ViewEmployee(string empid)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                GetEmployeesDto model = new GetEmployeesDto();

                model = await Mediator.Send(new GetEmployeeByEmpIDQuery() { EmpID = empid });

                if (!await VerifySession())
                    return View("../Account/Login");

                TempData["IsHasError"] = false;
                return View(model);

            }
            catch (Exception ex)
            {
                TempData["IsHasError"] = true;
                TempData["Message"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return RedirectToAction("");
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

                TempData["IsHasError"] = true;

                return View();
            }
            catch (Exception ex)
            {
                TempData["IsHasError"] = true;
                TempData["Message"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return RedirectToAction("");
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
                });

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

                UpdateEmployeeDto model = new UpdateEmployeeDto();

                var _departments = await Mediator.Send(new GetListofDepartmentQuery() { });
                var _sections = await Mediator.Send(new GetListofDepartmentalSectionQuery() { });
                var _civilStatuses = await Mediator.Send(new GetListOfCivilStatusQuery() { });

                TempData["__Departments"] = System.Text.Json.JsonSerializer.Serialize(_departments);

                var data = await Mediator.Send(new GetEmployeeByEmpIDQuery() { EmpID = empid });

                model = mapper.Map<UpdateEmployeeDto>(data);

                TempData["__DepartmentalSection"] = System.Text.Json.JsonSerializer.Serialize(_sections.Where(x => x.DepartmentCode == model.DepartmentCode));

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
        public async Task<ActionResult> EditEmployee(string empid, UpdateEmployeeDto model)
        {
            try
            {
                await Mediator.Send(new UpdateEmployeeCommand()
                {
                    EmpID = empid,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    DepartmentCode = model.DepartmentCode,
                    DepartmentSectionCode = model.DepartmentSectionCode,
                    DateOfBirth = model.DateOfBirth,
                    CivilStatusCode = model.CivilStatusCode
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

        [Route("deleteselected")]
        public async Task<IActionResult> DeleteEmployee(string[] arrayOfValues)
        {
            try
            {
                foreach (string employee in arrayOfValues)
                {
                    var result = await Mediator.Send(new DeleteEmployeeCommand()
                    {
                        EmpID = employee
                    });
                }

                TempData["IsHasError"] = false;
                TempData["Message"] = "Deleted Successfully";

                return View("GetEmployees");
            }
            catch (Exception ex)
            {
                TempData["IsHasError"] = true;
                TempData["Message"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return RedirectToAction("");
            }

        }


        [Route("delete/{empid}")]
        public async Task<IActionResult> DeleteEmployee(string empid)
        {
            try
            {
                var result = await Mediator.Send(new DeleteEmployeeCommand()
                {
                    EmpID = empid
                });

                TempData["IsHasError"] = false;
                TempData["Message"] = result.Item2;

                return View("GetEmployees");
            }
            catch (Exception ex)
            {
                TempData["IsHasError"] = true;
                TempData["Message"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return RedirectToAction("");
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
