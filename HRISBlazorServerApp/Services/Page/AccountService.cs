using AutoMapper;
using Blazored.LocalStorage;
using HRIS.Infrastructure.Identity;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Models;
using HRISBlazorServerApp.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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


        public AccountService(UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                              HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage
                           , IConfiguration configuration,
                           IHttpContextAccessor HttpContextAccessor,
                           TokenProvider _tokenProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _config = configuration;
            httpContextAccessor = HttpContextAccessor;
            tokenProvider = _tokenProvider;
        }

        public async Task<LoginResult> Login(LoginRequest loginRequest)
        {
            UriBuilder url = new UriBuilder(_config.GetValue<string>("HRISBaseUrl"))
            {
                Path = "api/Authorization/Login",
                Query = "username=" + loginRequest.Username + "&password=" + loginRequest.Password
            };

            var response = await _httpClient.PostAsJsonAsync<LoginResult>(url.ToString(), null);

            LoginResult loginResult = new LoginResult();

            if (response.IsSuccessStatusCode)
            {
                loginResult = JsonConvert.DeserializeObject<LoginResult>(await response.Content.ReadAsStringAsync());

                tokenProvider.AccessToken = loginResult.Token;

                var user = await GetUserDetails(loginRequest.Username);

                if (user.ContainsKey("email"))
                {
                    //await _localStorage.SetItemAsync("authToken", tokenProvider.AccessToken);//loginResult.Token);

                    ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user["email"].ToString());

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

                    return loginResult;
                }
            }

            throw new Exception("Invalid Login");
        }


        private  async Task<Dictionary<string, string>> GetUserDetails(string username)
        {
            UriBuilder usrUrl = new UriBuilder(_config.GetValue<string>("HRISBaseUrl"))
            {
                Path = "api/Authorization/GetUserByUsername",
                Query = "username=" + username
            };

            // Add an Accept header for JSON format.
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync(usrUrl.ToString()).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(products);

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
            //await _localStorage.RemoveItemAsync("authToken");
            tokenProvider.AccessToken = string.Empty;
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

    }
}
