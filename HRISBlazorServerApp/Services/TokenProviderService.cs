using HRISBlazorServerApp.APISettings;
using HRISBlazorServerApp.Exceptions;
using HRISBlazorServerApp.Interfaces;
using HRISBlazorServerApp.Models;
using HRISBlazorServerApp.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HRISBlazorServerApp.Services
{
    public class TokenProviderService : ITokenProviderService
    {
        private readonly TokenProvider tokenProvider;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IConfiguration _config;

        public TokenProviderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config= configuration;
        }

        public async Task<bool> IsValidToken(string token)
        {
            var _url = $"api/authentication/IsValidToken/{token}";

            var result = await GetAsync<bool>(_url.ToString(), true);

            return result;
        }

        public async Task<LoginResult> RefreshToken(string username)
        {
            var _url = $"api/authentication/refreshtoken?username={username}";

            var _result = await PostAsync<LoginResult>(_url.ToString(), null);

            return _result; ;
        }

        private async Task<ApplicationUser> GetUserDetails(string username)
        {
            var _url = $"api/authentication/GetUserByUsername?username={username}";

            var user = await GetAsync<ApplicationUser>(_url.ToString(), true);

            if (string.IsNullOrEmpty(user.Email))
                throw new ApplicationException("No User Found");

            return user;
        }

        private async Task PrepareAuthenticatedClient()
        {
            var _serviceAPIOpts = _config.GetSection(nameof(ServiceAPIOptions)).Get<ServiceAPIOptions>();

            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_serviceAPIOpts.ApiBaseUrl);
                _httpClient.Timeout = TimeSpan.FromSeconds(_serviceAPIOpts.RequestTimeout);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private async Task ThrowAPIErrorException(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException("Unauthorized");

            var _errorDetails = await response.Content.ReadFromJsonAsync<ApiError>();

            if (_errorDetails.Status == StatusCodes.Status400BadRequest)
                if (_errorDetails.Error?.Any() ?? false)
                    throw new ValidationException(_errorDetails.Error);
                else
                    throw new ValidationException();

            else if (_errorDetails.Status == StatusCodes.Status404NotFound)
                throw new NotFoundException(_errorDetails.Detail);

            else if (_errorDetails.Status == StatusCodes.Status403Forbidden)
                throw new ForbiddenAccessException();

            else
                throw new Exception("API Error: " + response.RequestMessage.RequestUri.AbsoluteUri);
        }

        private async Task<TValue> GetAsync<TValue>(string url, bool useDefaultOnNotFound)
        {
            await PrepareAuthenticatedClient();
            var _response = await _httpClient.GetAsync(url);

            if (_response.StatusCode == System.Net.HttpStatusCode.NotFound && useDefaultOnNotFound)
                return default(TValue);

            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);

            var _results = await _response.Content.ReadFromJsonAsync<TValue>();
            return _results;
        }

        protected async Task<TValue> PostAsync<TValue>(string url, TValue model)
        {
            return await PostAsync<TValue, TValue>(url, model);
        }

        protected async Task<TOutput> PostAsync<TValue, TOutput>(string url, TValue model)
        {
            await PrepareAuthenticatedClient();
            var _response = await _httpClient.PostAsJsonAsync(url, model);

            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);

            var _results = await _response.Content.ReadFromJsonAsync<TOutput>();
            return _results;
        }




    }
}
