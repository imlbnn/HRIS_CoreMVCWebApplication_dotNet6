using HRIS.Application.CivilStatuses.Commands;
using HRIS.Application.CivilStatuses.Dtos;
using HRIS.Application.CivilStatuses.Queries;
using HRIS.Application.DepartmentalSections.Commands;
using HRIS.Application.DepartmentalSections.Dtos.Queries;
using HRIS.Application.DepartmentalSections.Queries;
using HRIS.Application.Departments.Commands;
using HRIS.Application.Departments.Dtos.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "HRIS")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CivilStatusController : ApiControllerBase
    {
        [HttpPost]
        [Route("create")]
        //Run and use Postman to call this request
        public async Task<ActionResult<Tuple<bool, string>>> CreateCivilStatus([FromBody] CreateCivilStatusCommand request)
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


        [HttpGet]
        //Run and use Postman to call this request
        public async Task<ActionResult<IEnumerable<GetCivilStatusDto>>> GetCivilStatus()
        {
            try
            {
                var _result = await Mediator.Send(new GetListOfCivilStatusQuery() { });

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

    }
}
