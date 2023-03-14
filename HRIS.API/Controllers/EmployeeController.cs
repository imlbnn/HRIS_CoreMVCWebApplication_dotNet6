using HRIS.Application.Common.Models;
using HRIS.Application.Employees.Commands;
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS.Application.Employees.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
        public async Task<ActionResult<PaginatedList<GetEmployeesDto>>> GetEmployees([FromQuery] GetEmployeePaginatedQuery request)
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
        public async Task<ActionResult<IEnumerable<GetEmployeesDto>>> GetEmployees()
        {
            try
            {
                var _result = await Mediator.Send(new GetEmployeesQuery() { });

                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("{empid}")]
        //Run and use Postman to call this request
        public async Task<ActionResult<GetEmployeesDto>> GetEmployeeByEmpID(string empid)
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

        //[HttpGet]
        ////Run and use Postman to call this request
        //public async Task<ActionResult<IEnumerable<GetEmployeesDto>>> GetEmployeeByName([FromQuery] GetEmployeeConsistingNameQuery request)
        //{
        //    try
        //    {
        //        var _result = await Mediator.Send(request);

        //        return Ok(_result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
        //    }
        //}


        [HttpPost("create")]
        //Run and use Postman to call this request
        public async Task<ActionResult<bool>> CreateEmployee(CreateEmployeeCommand request)
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

        [HttpPut("update")]
        //Run and use Postman to call this request
        public async Task<ActionResult<Tuple<bool, string>>> UpdateEmployee(UpdateEmployeeCommand request)
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


        [HttpPut("archive/{empid}")]
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
