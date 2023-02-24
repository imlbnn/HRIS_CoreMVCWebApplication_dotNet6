using HRIS.API.Controllers;
using HRIS.Application.Common.Security;
using HRIS.Application.DepartmentalSections.Queries;
using HRIS.Application.Departments.Dtos.Queries;
using HRIS.Application.Departments.Queries;
using HRIS.Domain.Entities;
using HRIS_CoreMVC_dotNet6.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace HRIS_CoreMVC_dotNet6.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class DepartmentController : ApiControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartments()
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
        public async Task<IActionResult> GetAllDepartments(JqueryDatatableParam param)
        {
            var _result = await Mediator.Send(new GetListofDepartmentQuery() { });

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                _result = _result.Where(x => x.Description.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
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


        [HttpGet]
        [Route("{code}")]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartmentByCode(string code)
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


        

    }
}
