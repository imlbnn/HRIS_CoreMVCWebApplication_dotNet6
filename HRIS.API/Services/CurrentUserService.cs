using AspNet.Security.OpenIdConnect.Primitives;
using HRIS.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HRIS.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                var _userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.Subject);

                ///if (string.IsNullOrEmpty(_userId))
                   // _userId = Username;

                if (string.IsNullOrEmpty(_userId))
                {
                    var _clientId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.ClientId);
                    if (!string.IsNullOrEmpty(_clientId))
                    {
                        var _clientUser = _httpContextAccessor.HttpContext?.Request.Headers["X-Authenticated-Client-User"];
                        if (!string.IsNullOrEmpty(_clientUser))
                            _userId = _clientUser;
                        else
                            _userId = _clientId;
                    }
                }

                if (string.IsNullOrEmpty(_userId))
                {
                    _userId = "";//_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    //_userId = JwtRegisteredClaimNames.NameId
                }

                return _userId;
            }
        }

        public string Username
        {
            get
            {
                var _username = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.PreferredUsername);

                if (string.IsNullOrEmpty(_username))
                    _username = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.Username);


                if (string.IsNullOrEmpty(_username))
                    _username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value;

                if (string.IsNullOrEmpty(_username))
                {
                    var _clientId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.ClientId);
                    if (!string.IsNullOrEmpty(_clientId))
                    {
                        var _clientUser = _httpContextAccessor.HttpContext?.Request.Headers["X-Authenticated-Client-User"];
                        if (!string.IsNullOrEmpty(_clientUser))
                            _username = _clientUser;
                        else
                            _username = _clientId;
                    }
                }

                if (string.IsNullOrEmpty(_username) && IsAuthenticated)
                {
                    _username = DisplayName;
                }

                if (string.IsNullOrEmpty(_username))
                    _username = "Default User";

                return _username;
            }
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string DisplayName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.Name);

        public string ClientInfo => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() + " " + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"][0].ToString();

    }

}
