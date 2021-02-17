using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCommunication
{
    public class ApiCallFake : IApiCall
    {
        public ApiCallFake() { }

        public Task<string> GetAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return Task.FromResult(string.Empty);
        }
        public Task<T> GetAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return Task.FromResult<T>(default);
        }

        public Task<string> DeleteAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return Task.FromResult(string.Empty);
        }
        public Task<T> DeleteAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return Task.FromResult<T>(default);
        }

        public Task<string> PostAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return Task.FromResult(string.Empty);
        }
        public Task<T> PostAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return Task.FromResult<T>(default);
        }

        public Task<string> PutAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return Task.FromResult(string.Empty);
        }
        public Task<T> PutAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null)
        {
            return Task.FromResult<T>(default);
        }
    }
}