using AspNet.Security.OpenIdConnect.Primitives;
using HRIS.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            string _username = "";

            if (!string.IsNullOrEmpty(_username))
            {
                var _clientUser = _httpContextAccessor.HttpContext?.Request.Headers["X-Authenticated-Client-User"];
                if (!string.IsNullOrEmpty(_clientUser))
                    _username = _clientUser;
            }

            if (string.IsNullOrEmpty(_username))
            {
                if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName) == null)
                {
                    _username = "Default User";//_httpContextAccessor.HttpContext.User.FindFirst("Username").Value;
                }
                else
                {
                    _username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value;
                }


            }

            return Task.FromResult(_username);
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }
    }
}
