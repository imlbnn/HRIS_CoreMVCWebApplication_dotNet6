namespace HRISBlazorServerApp.Interfaces
{
    public interface ITokenProviderService
    { 
        Task<bool> IsValidToken(string token);
    }
}
