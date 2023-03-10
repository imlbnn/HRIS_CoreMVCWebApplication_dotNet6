using AutoMapper;
using Blazored.LocalStorage;
using HRIS.Application.Common.Models;
using HRIS.Infrastructure.Identity;
using HRIS.Net6_CQRSApproach.Model;
using HRISBlazorServerApp.Interfaces;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Model;
using HRISBlazorServerApp.Providers;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HRISBlazorServerApp.Services.Page
{
    public class AccountService : IAccountService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountService(HttpClient httpClient,
                               AuthenticationStateProvider authenticationStateProvider,
                               ILocalStorageService localStorage,
                               UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> Login(LoginRequest loginRequest)
        {
            var existingUser = await _userManager.FindByNameAsync(loginRequest.Username);

            if (existingUser == null)
            {
                return false;
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);

            if (!isCorrect)
            {
                return false;
            }

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(existingUser.Email.ToString());

//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwtToken);

            return true;
        }


        public async Task Logout()
        {
            await _localStorage.ClearAsync();
            //await _localStorage.RemoveItemAsync("authToken");
            //tokenProvider.AccessToken = string.Empty;
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            //_httpClient.DefaultRequestHeaders.Authorization = null;
        }




    }
}
