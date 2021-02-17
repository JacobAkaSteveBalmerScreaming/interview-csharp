using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiCommunication
{
    public class ApiCall : IApiCall
    {
        private const int DEFAULT_TIMEOUT_IN_MS = 10 * 1000;

        private static HttpClient _HttpClient;

        public ApiCall() : this(DEFAULT_TIMEOUT_IN_MS) { }

        public ApiCall(int timeoutInMilliseconds)
        {
            if (timeoutInMilliseconds < 1)
                throw new Exception("Timeout cannot be less than 1 millisecond");

            _HttpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(timeoutInMilliseconds),
            };
        }

        public async Task<string> GetAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return await SendHttpRequest(HttpMethod.Get, url, requestData, headerDict, accessToken);
        }

        public async Task<T> GetAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            var buffer = await SendHttpRequest(HttpMethod.Get, url, requestData, headerDict, accessToken);
            return JsonConvert.DeserializeObject<T>(buffer);
        }

        public async Task<string> PutAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return await SendHttpRequest(HttpMethod.Put, url, requestData, headerDict, accessToken);
        }

        public async Task<T> PutAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            var buffer = await SendHttpRequest(HttpMethod.Put, url, requestData, headerDict, accessToken);
            return JsonConvert.DeserializeObject<T>(buffer);
        }

        public async Task<string> DeleteAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return await SendHttpRequest(HttpMethod.Delete, url, requestData, headerDict, accessToken);
        }

        public async Task<T> DeleteAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            var buffer = await SendHttpRequest(HttpMethod.Delete, url, requestData, headerDict, accessToken);
            return JsonConvert.DeserializeObject<T>(buffer);
        }

        public async Task<string> PostAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return await SendHttpRequest(HttpMethod.Post, url, requestData, headerDict, accessToken);
        }

        public async Task<T> PostAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            var buffer = await SendHttpRequest(HttpMethod.Post, url, requestData, headerDict, accessToken);
            return JsonConvert.DeserializeObject<T>(buffer);
        }

        public async Task<string> SendHttpRequest(HttpMethod method, string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty");

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new ArgumentException("URL is not valid");

            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    _HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                }

                if (headerDict == null)
                {
                    if (requestData != null)
                    {
                        string buffer = JsonConvert.SerializeObject(requestData);
                        requestMessage.Content = new StringContent(buffer, Encoding.UTF8, "application/json");
                    }
                }
                else
                {
                    foreach (var param in headerDict)
                    {
                        if (param.Key.Equals("ContentType"))
                            requestMessage.Content = new StringContent(requestData.ToString(), Encoding.UTF8, param.Value);
                        else
                            requestMessage.Headers.Add(param.Key, param.Value);
                    }
                }

                var response = await _HttpClient.SendAsync(requestMessage).ConfigureAwait(false);
                string outputData = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var x = new Exception($"REST call returned a response of {response.StatusCode}");
                    x.Data.Add("response", outputData);
                    x.Data.Add("request", requestData);
                    throw x;
                }

                return outputData;
            }
        }
    }
}

