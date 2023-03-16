using HRISBlazorServerApp.Models;

namespace HRISBlazorServerApp.Interfaces
{
    public interface ITokenProviderService
    { 
        Task<bool> IsValidToken(string token);
        Task<LoginResult> RefreshToken(string username);
    }
}
