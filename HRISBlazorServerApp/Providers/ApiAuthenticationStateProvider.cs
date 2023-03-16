using HRISBlazorServerApp.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using HRISBlazorServerApp.Interfaces;

namespace HRISBlazorServerApp.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly TokenProvider tokenProvider;
        private readonly ITokenProviderService _tokenProviderService;
        private readonly TokenConfig _tokenConfig;

        public ApiAuthenticationStateProvider(HttpClient httpClient, 
            ILocalStorageService localStorage, 
            IJSRuntime jSRuntime, 
            TokenProvider _tokenProvider, 
            ITokenProviderService tokenProviderService,
            TokenConfig tokenConfig
            )
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            tokenProvider = _tokenProvider;
            _tokenProviderService = tokenProviderService;
            _tokenConfig = tokenConfig;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await Task.FromResult(tokenProvider.AccessToken);

            if (string.IsNullOrEmpty(savedToken))
            {
                var _isValid = await _tokenProviderService.IsValidToken(_tokenConfig.CurrentAccessToken);

                if (_isValid)
                {
                    savedToken = tokenProvider.AccessToken = _tokenConfig.CurrentAccessToken;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_tokenConfig.CurrentUser))
                    {
                        var result = await _tokenProviderService.RefreshToken(_tokenConfig.CurrentUser);

                        if (result != null)
                        {
                            if (result.Success)
                            {
                                _tokenConfig.SetToken(result.Token);
                                savedToken = tokenProvider.AccessToken = result.Token;
                            }
                            else
                            {
                                _tokenConfig.SetToken(string.Empty);
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(savedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
            }
            else
            {
                await _localStorage.ClearAsync();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public void MarkUserAsAuthenticated(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
