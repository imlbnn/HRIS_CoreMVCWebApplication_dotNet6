using HRIS.Application.Common.Models;

namespace HRISBlazorServerApp.Interfaces.Services
{
    public interface IAccountService
    {
        Task<bool> Login(LoginRequest loginRequest);
        Task Logout();
    }
}
