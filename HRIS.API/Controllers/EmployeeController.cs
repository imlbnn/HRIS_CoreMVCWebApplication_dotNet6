using HRIS.Application.Common.Models;
using HRIS.Application.Employees.Commands;
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.API.Controllers
{
    [ApiExplorerSettings(GroupName = "HRIS")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : ApiControllerBase
    {
        [HttpGet]
        [Route("paged")]
        //Run and use Postman to call this request
        public async Task<ActionResult<PaginatedList<GetEmployeesDto>>> GetEmployees([FromQuery] GetEmployeesQuery request)
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

        [HttpGet("{empid}")]
        //Run and use Postman to call this request
        public async Task<ActionResult<IEnumerable<GetEmployeesDto>>> GetEmployeeByEmpID(string empid)
        {
            try
            {
                var _result = await Mediator.Send(new GetEmployeeByEmpIDQuery() { EmpID = empid});

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("{name}")]
        //Run and use Postman to call this request
        public async Task<ActionResult<IEnumerable<GetEmployeesDto>>> GetEmployeeByName(string name)
        {
            try
            {
                var _result = await Mediator.Send(new GetEmployeeConsistingNameQuery() { Name = name });

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }





        [HttpPost("create")]
        //Run and use Postman to call this request
        public async Task<ActionResult<CreateEmployeeDto>> CreateEmployee(CreateEmployeeCommand request)
        {
            try
            {
                var _results = await Mediator.Send(request);
                return Ok(_results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost("update")]
        //Run and use Postman to call this request
        public async Task<ActionResult<CreateEmployeeDto>> UpdateEmployee(UpdateEmployeeCommand request)
        {
            try
            {
                var _results = await Mediator.Send(request);
                return Ok(_results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


        [HttpDelete("archive/{empid}")]
        //Run and use Postman to call this request
        public async Task<ActionResult<CreateEmployeeDto>> ArchiveEmployee( string empid)
        {
            try
            {
                var _results = await Mediator.Send(new DeleteEmployeeCommand { EmpID = empid});
                return Ok(_results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
