using HRISBlazorServerApp.Dtos.DepartmentSection;
using HRISBlazorServerApp.Exceptions;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Models;

namespace HRISBlazorServerApp.Services.Page
{
    public class DepartmentSectionService : ApiServiceBase, IDepartmentSectionService
    {
        public DepartmentSectionService(TokenProvider tokenProvider, HttpClient httpClient, IConfiguration config)
            : base(tokenProvider, httpClient, config)
        {
        }

        public async Task<IEnumerable<GetDepartmentSectionDto>> GetDepartmentalSection(string code)
        {
            try
            {
                var _url = $"api/departmentalsection/{code}";

                var _result = await base.GetAsync<IEnumerable<GetDepartmentSectionDto>>(_url.ToString(), true);

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
