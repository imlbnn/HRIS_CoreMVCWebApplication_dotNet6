using HRIS.API.Controllers;
using HRIS.Application.DepartmentalSections.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRIS_CoreMVC_dotNet6.Controllers
{
    [Route("[controller]")]
    public class DepartmentalSectionController : ApiControllerBase
    {
        public IActionResult Index()
        {
            return View();
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
