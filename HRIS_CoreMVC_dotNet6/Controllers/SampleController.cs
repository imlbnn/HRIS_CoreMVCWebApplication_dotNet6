using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using HRIS_CoreMVC_dotNet6.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRIS_CoreMVC_dotNet6.Controllers
{
    [Route("[controller]")]
    public class SampleController : Controller
    {
        private readonly IEmployeeService service;
        public SampleController(IEmployeeService service) { 
            this.service = service;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("view")]
        public async Task<ActionResult<GetEmployeesDto>> ViewSample()
        {
            try
            {
                if (!ModelState.IsValid) return View();

                GetEmployeesDto model = new GetEmployeesDto();
                model = await service.GetEmployeeByEmpID("040823-000000001");

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
    }
}
