using AutoMapper;
using Blazored.LocalStorage;
using HRISBlazorServerApp.APISettings;
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
    public class AccountService : ApiServiceBase, IAccountService
    {
        private readonly TokenProvider tokenProvider;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AccountService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage,
                           TokenProvider _tokenProvider) 
            : base(_tokenProvider, httpClient)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            tokenProvider = _tokenProvider;
        }

        public async Task<LoginResult> Login(LoginRequest loginRequest)
        {
            var _url = $"api/authentication/login";

            _httpClient.DefaultRequestHeaders.Authorization = null;

            tokenProvider.AccessToken = string.Empty;

            var _result = await base.PostAsync<LoginRequest, LoginResult>(_url.ToString(), loginRequest);

            tokenProvider.AccessToken = _result.Token;

            var user = await GetUserDetails(loginRequest.Username);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user.Email);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _result.Token);

            return _result;
        }


        private async Task<ApplicationUser> GetUserDetails(string username)
        {
            var _url = $"api/authentication/GetUserByUsername?username={username}";

            var user = await base.GetAsync<ApplicationUser>(_url.ToString(), true);

            if (string.IsNullOrEmpty(user.Email))
                throw new ApplicationException("No User Found");

            return user;
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
