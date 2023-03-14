using HRISBlazorServerApp.Dtos.CivilStatus;
using Microsoft.AspNetCore.Mvc;

namespace HRISBlazorServerApp.Interfaces.Services
{
    public interface ICivilStatusService
    {
        Task<IEnumerable<GetCivilStatusDto>> GetCivilStatus();
    }
}
