using HRISBlazorServerApp.APISettings;
using HRISBlazorServerApp.Exceptions;
using HRISBlazorServerApp.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace HRISBlazorServerApp.Services
{
    public class ApiServiceBase
    {
        private readonly TokenProvider tokenProvider;
        protected readonly HttpClient HttpClient;
        private readonly IConfiguration _config;

        public ApiServiceBase(TokenProvider tokenProvider, HttpClient httpClient, IConfiguration config)
        {
            this.tokenProvider = tokenProvider;
            HttpClient = httpClient;
            _config = config;
        }

        public async virtual Task PrepareAuthenticatedClient()
        {
            var _serviceAPIOpts = _config.GetSection(nameof(ServiceAPIOptions)).Get<ServiceAPIOptions>();
            var _accessToken = tokenProvider.AccessToken;
            if(HttpClient.BaseAddress == null)
            {
                HttpClient.BaseAddress = new Uri(_serviceAPIOpts.ApiBaseUrl);
                HttpClient.Timeout = TimeSpan.FromSeconds(_serviceAPIOpts.RequestTimeout);
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        public async Task ThrowAPIErrorException(HttpResponseMessage response)
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

        protected async Task<TValue> PostAsync<TValue>(string url, TValue model)
        {
            return await PostAsync<TValue, TValue>(url, model);
        }

        protected async Task<TOutput> PostAsync<TValue, TOutput>(string url, TValue model)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.PostAsJsonAsync(url, model);

            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);

            var _results = await _response.Content.ReadFromJsonAsync<TOutput>();
            return _results;
        }

        protected async Task DeleteAsync(string url)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.DeleteAsync(url);
            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);
            _response.EnsureSuccessStatusCode();
        }

        protected async Task<TValue> GetAsync<TValue>(string url, bool useDefaultOnNotFound)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.GetAsync(url);

            if (_response.StatusCode == System.Net.HttpStatusCode.NotFound && useDefaultOnNotFound)
                return default(TValue);

            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);

            var _results = await _response.Content.ReadFromJsonAsync<TValue>();
            return _results;
        }

        protected async Task<TValue> GetAsync<TValue>(string url)
        {
            return await GetAsync<TValue>(url, false);
        }

        protected async Task<TOutput> PutAsync<TValue, TOutput>(string url, TValue model)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.PutAsJsonAsync(url, model);
            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);
            var _results = await _response.Content.ReadFromJsonAsync<TOutput>();
            return _results;
        }

        protected async Task<TValue> PutAsync<TValue>(string url, TValue model)
        {
            await PrepareAuthenticatedClient();
            var _response = await HttpClient.PutAsJsonAsync(url, model);
            if (!_response.IsSuccessStatusCode)
                await ThrowAPIErrorException(_response);
            var _results = await _response.Content.ReadFromJsonAsync<TValue>();
            return _results;
        }
    }
}

