
using HRISBlazorServerApp.Models;

namespace HRISBlazorServerApp.Interfaces.Services
{
    public interface IAccountService
    {
        Task<LoginResult> Login(LoginRequest loginRequest);
        Task Logout();
    }
}
