using HRISBlazorServerApp.Dtos.CivilStatus;
using HRISBlazorServerApp.Exceptions;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Models;

namespace HRISBlazorServerApp.Services.Page
{
    public class CivilStatusService : ApiServiceBase, ICivilStatusService
    {
        public CivilStatusService(TokenProvider tokenProvider, HttpClient httpClient, IConfiguration config) 
            : base(tokenProvider, httpClient, config) { }

        public async Task<IEnumerable<GetCivilStatusDto>> GetCivilStatus()
        {
            try
            {
                var _url = $"api/civistatus";

                var _result = await base.GetAsync<IEnumerable<GetCivilStatusDto>>(_url.ToString(), true);

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
