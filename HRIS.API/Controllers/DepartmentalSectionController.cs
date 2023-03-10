using HRIS.Application.DepartmentalSections.Commands;
using HRIS.Application.DepartmentalSections.Dtos.Queries;
using HRIS.Application.DepartmentalSections.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.API.Controllers
{
    [ApiExplorerSettings(GroupName = "HRIS")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartmentalSectionController : ApiControllerBase
    {
        [HttpGet]
        //Run and use Postman to call this request
        public async Task<ActionResult<IEnumerable<GetDepartmentSectionDto>>> GetDepartmentalSection()
        {
            try
            {
                var _result = await Mediator.Send(new GetListofDepartmentalSectionQuery() { });

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


        [HttpPost]
        [Route("create")]
        //Run and use Postman to call this request
        public async Task<ActionResult<Tuple<bool, string>>> CreateDepartmentalSection([FromBody] CreateDepartmentalSectionCommand request)
        {
            try
            {
                var _result = await Mediator.Send(request);

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        //Run and use Postman to call this request
        public async Task<ActionResult<Tuple<bool, string>>> UpdateDepartmentalSection([FromBody] UpdateDepartmentalSectionCommand request)
        {
            try
            {
                var _result = await Mediator.Send(request);

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


        
    }
}
