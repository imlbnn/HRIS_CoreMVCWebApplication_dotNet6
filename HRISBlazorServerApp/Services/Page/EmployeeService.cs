using AutoMapper;
using HRISBlazorServerApp.Dtos.Employee;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Models;
using MediatR;
using static Duende.IdentityServer.Models.IdentityResources;

namespace HRISBlazorServerApp.Services.Page
{
    public class EmployeeService : ApiServiceBase, IEmployeeService
    {
        private readonly string baseUrl;
        private readonly IConfiguration _config;

        public EmployeeService(TokenProvider tokenProvider, HttpClient httpClient, IConfiguration configuration) 
            : base(tokenProvider, httpClient)
        {
            _config= configuration;
            baseUrl = _config.GetValue<string>("HRISBaseUrl");
        }

        public async Task<IEnumerable<GetEmployeesDto>> GetEmployees()
        {
            try
            {
                UriBuilder _url = new UriBuilder(baseUrl)
                {
                    Path = "api/employee"
                };

                var _result = await base.GetAsync<IEnumerable<GetEmployeesDto>>(_url.ToString(), true);

                return _result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<GetEmployeesDto> GetEmployeeByEmpID(string empid)
        {
            try
            {
                UriBuilder _url = new UriBuilder(baseUrl)
                {
                    Path = $"api/employee/{empid}"
                };

                var _result = await base.GetAsync<GetEmployeesDto>(_url.ToString(), true);

                return _result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<bool> CreateEmployee(CreateEmployeeDto request)
        {
            try
            {
                UriBuilder _url = new UriBuilder(baseUrl)
                {
                    Path = $"api/employee/create"
                };

                var _result = await base.PostAsync<CreateEmployeeDto, bool>(_url.ToString(), request);

                return _result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<Tuple<bool, string>> UpdateEmployee(UpdateEmployeeDto request)
        {
            try
            {
                UriBuilder _url = new UriBuilder(baseUrl)
                {
                    Path = $"api/employee/update"
                };

                var _result = await base.PutAsync<UpdateEmployeeDto, Tuple<bool, string>>(_url.ToString(), request);

                return _result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


        public async Task<bool> ArchiveEmployee(string empid)
        {
            try
            {
                UriBuilder _url = new UriBuilder(baseUrl)
                {
                    Path = $"api/employee/archive/{empid}"
                };

                var _result = await base.PutAsync(_url.ToString(), true);

                return _result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


    }
}
