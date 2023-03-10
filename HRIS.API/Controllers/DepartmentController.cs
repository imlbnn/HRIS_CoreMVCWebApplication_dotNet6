using HRIS.Application.Departments.Queries;
using HRIS.Application.Employees.Dtos;
using HRIS.Application.Employees.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using HRIS.Application.Departments.Dtos.Queries;
using HRIS.Application.Departments.Dtos.Commands;
using HRIS.Application.Departments.Commands;

namespace HRIS.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "HRIS")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartmentController : ApiControllerBase
    {
        [HttpGet]
        //Run and use Postman to call this request
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartments()
        {
            try
            {
                var _result = await Mediator.Send(new GetListofDepartmentQuery() { });

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("{name}")]
        //Run and use Postman to call this request
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartmentsConsistingName(string name)
        {
            try
            {
                var _result = await Mediator.Send(new GetDepartmentConsistingNameQuery { DepartmentName = name });

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<CreateDepartmentDto>> CreateDepartment([FromBody] CreateDepartmentCommand request)
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

        [HttpDelete]
        [Route("archive/{code}")]
        public async Task<ActionResult<CreateDepartmentDto>> ArchiveDepartment(string code)
        {
            try
            {
                var _result = await Mediator.Send(new DeleteDepartmentCommand { Code = code});

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<CreateDepartmentDto>> UpdateDepartment([FromBody] UpdateDepartmentCommand request)
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
