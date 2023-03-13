using AutoMapper;
using Blazored.LocalStorage;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Models;
using HRISBlazorServerApp.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HRISBlazorServerApp.Services.Page
{
    public class AccountService : IAccountService
    {
        private readonly TokenProvider tokenProvider;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string _baseUrl;


        public AccountService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage, 
                           IConfiguration configuration,
                           IHttpContextAccessor HttpContextAccessor,
                           TokenProvider _tokenProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _config = configuration;
            httpContextAccessor = HttpContextAccessor;
            tokenProvider = _tokenProvider;
            _baseUrl = _config.GetValue<string>("HRISBaseUrl");
        }

        public async Task<LoginResult> Login(LoginRequest loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}api/authentication/login", loginRequest);

            LoginResult loginResult = new LoginResult();

            if (response.IsSuccessStatusCode)
            {
                loginResult = JsonConvert.DeserializeObject<LoginResult>(await response.Content.ReadAsStringAsync());

                tokenProvider.AccessToken = loginResult.Token;

                var user = await GetUserDetails(loginRequest.Username);

                if (user.ContainsKey("email"))
                {
                    ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user["email"].ToString());

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

                    return loginResult;
                }
            }

            throw new Exception("Invalid Login");
        }


        private async Task<Dictionary<string, string>> GetUserDetails(string username)
        {
            UriBuilder usrUrl = new UriBuilder(_config.GetValue<string>("HRISBaseUrl"))
            {
                Path = "api/authentication/GetUserByUsername",
                Query = "username=" + username
            };

            // Add an Accept header for JSON format.
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync(usrUrl.ToString()).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(user);

                if (!result.ContainsKey("email"))
                    throw new ApplicationException("No User Found");

                return result;

            }
            else
            {
                throw new ApplicationException("No User Found");
            }

        }

        public async Task Logout()
        {
            await _localStorage.ClearAsync();
            tokenProvider.AccessToken = string.Empty;
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

    }
}
