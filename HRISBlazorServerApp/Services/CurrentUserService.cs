using HRIS.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace HRISBlazorServerApp.Services
{
    //public class CurrentUserService : ICurrentUserService
    //{
    //    private readonly IHttpContextAccessor _httpContextAccessor;

    //    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    //    {
    //        _httpContextAccessor = httpContextAccessor;
    //    }

    //    public string UserId => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(q => q.Type == "preferred_username")?.Value ?? "Anonymous";

    //    public string DisplayName => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(q => q.Type == "name")?.Value;

    //    public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

    //    public string Username => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(q => q.Type == "preferred_username")?.Value;

    //    public string ClientInfo => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() + " " + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"][0].ToString();
    //}
}
