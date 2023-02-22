using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.HTTPClientHandlers
{ 
    //public class AuthenticationHandler : DelegatingHandler
    //{
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    public AuthenticationHandler(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        var _accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
    //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
    //        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //        return await base.SendAsync(request, cancellationToken);
    //    }
    //}
}
