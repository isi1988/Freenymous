

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Freenymous.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Freenymous.Front.Services
{
    public interface IHttpService
    {
        Task<string?> GetString(string uri);
        Task<T?> Get<T>(string uri);

        Task Get(string uri);

        Task<T?> Post<T>(string uri, object? value);

        Task Post(string uri, object value);

        Task Post(string uri, MultipartFormDataContent content);

        Task<T?> Patch<T>(string uri, object? value);

        Task Patch(string uri, object value);

        Task<T?> Delete<T>(string uri);

        Task Delete(string uri);
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(
            HttpClient httpClient
        )
        {
            _httpClient = httpClient;
        }

        public async Task<T?> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequest<T>(request);
        }

        public async Task Get(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            await SendRequest(request);
        }

        public async Task<string?> GetString(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequestString(request);
        }

        public async Task<T?> Delete<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendRequest<T>(request);
        }

        public async Task Delete(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            await SendRequest(request);
        }

        public async Task<T?> Post<T>(string uri, object? value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequest<T>(request);
        }


        public async Task Post(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await SendRequest(request);
        }

        public async Task Patch(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await SendRequest(request);
        }

        public async Task Post(string uri, MultipartFormDataContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = content;
            await SendRequest(request);
        }


        public async Task<T?> Patch<T>(string uri, object? value)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequest<T>(request);
        }

        // helper methods

        private async Task<T?> SendRequest<T>(HttpRequestMessage request)
        {


            try
            {
                using var response = await _httpClient.SendAsync(request);


                // auto logout on 401 response
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return default;
                }

                // throw exception on error response
                if (response.IsSuccessStatusCode)
                {
                    var str = await response.Content.ReadAsStringAsync();
                    var convertor = new StringEnumConverter();
                    return JsonConvert.DeserializeObject<T>(str, convertor);
                }

                return default;
            }
            catch (Exception e)
            {
                return default;
            }
        }

        private async Task<string?> SendRequestString(HttpRequestMessage request)
        {
            try
            {
                using var response = await _httpClient.SendAsync(request);

                // auto logout on 401 response
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return default;
                }

                // throw exception on error response
                if (response.IsSuccessStatusCode)
                {
                    var str = await response.Content.ReadAsStringAsync();
                    return str;
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return null;
        }

        private async Task SendRequest(HttpRequestMessage request)
        {

            try
            {
                using var response = await _httpClient.SendAsync(request);

                // auto logout on 401 response
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return;
                }

                // throw exception on error response
                if (response.IsSuccessStatusCode) return;
            }
            catch (Exception e)
            {
            }

        }
    }
}