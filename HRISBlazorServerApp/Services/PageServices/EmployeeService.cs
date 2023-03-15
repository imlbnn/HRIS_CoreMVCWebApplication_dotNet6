using AutoMapper;
using HRISBlazorServerApp.Dtos.Employee;
using HRISBlazorServerApp.Exceptions;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Models;

namespace HRISBlazorServerApp.Services.PageServices
{
    public class EmployeeService : ApiServiceBase, IEmployeeService
    {
        public EmployeeService(TokenProvider tokenProvider, HttpClient httpClient, IConfiguration config)
            : base(tokenProvider, httpClient, config)
        {
        }

        public async Task<PaginatedList<GetEmployeesDto>> GetEmployees(string searchKey, int pageNumber, int pageSize, string orderBy = "LastName")
        {
            try
            {
                var _url = $"api/employee/paged?searchKey={(string.IsNullOrEmpty(searchKey) ? "\"\"" : searchKey)}&orderBy={orderBy}&pageNumber={pageNumber}&pageSize={pageSize}";

                var _result = await base.GetAsync<PaginatedList<GetEmployeesDto>>(_url.ToString(), true);

                return _result;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetEmployeesDto> GetEmployeeByEmpID(string empid)
        {
            try
            {
                var _url = $"api/employee/{empid}";

                var _result = await base.GetAsync<GetEmployeesDto>(_url.ToString(), true);

                return _result;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateEmployee(CreateEmployee request)
        {
            try
            {
                var _url = $"api/employee/create";


                var _result = await base.PostAsync<CreateEmployee, bool>(_url.ToString(), request);

                return _result;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Tuple<bool, string>> UpdateEmployee(UpdateEmployee request)
        {
            try
            {
                var _url = $"api/employee/update";

                var _result = await base.PutAsync<UpdateEmployee, Tuple<bool, string>>(_url.ToString(), request);

                return _result;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> ArchiveEmployee(string empid)
        {
            try
            {
                var _url =  $"api/employee/archive/{empid}";

            
                var _result = await base.PutAsync<Tuple<bool,string>>(_url, null);

                return _result.Item1;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
