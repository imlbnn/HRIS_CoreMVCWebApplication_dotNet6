using HRISBlazorServerApp.Dtos.Department;
using HRISBlazorServerApp.Exceptions;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRISBlazorServerApp.Services.Page
{
    public class DepartmentService : ApiServiceBase, IDepartmentService
    {
        public DepartmentService(TokenProvider tokenProvider, HttpClient httpClient, IConfiguration config)
            : base(tokenProvider, httpClient, config)
        {
        }

        public async Task<IEnumerable<GetDepartmentDto>> GetDepartments()
        {
            try
            {
                var _url = "api/department";

                var _result = await base.GetAsync<IEnumerable<GetDepartmentDto>>(_url.ToString(), true);

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

        public async Task<IEnumerable<GetDepartmentDto>> GetDepartmentsConsistingName(string name)
        {
            try
            {
                var _url = $"api/department/{name}";

                var _result = await base.GetAsync<IEnumerable<GetDepartmentDto>>(_url.ToString(), true);

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
    }
}
