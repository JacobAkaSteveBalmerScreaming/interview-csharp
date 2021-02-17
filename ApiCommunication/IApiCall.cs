using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCommunication
{
    public interface IApiCall
    {
        Task<string> GetAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null);
        Task<T> GetAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null);

        Task<string> PutAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null);
        Task<T> PutAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null);

        Task<string> DeleteAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null);
        Task<T> DeleteAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null);

        Task<string> PostAsync(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null);
        Task<T> PostAsync<T>(string url, object requestData, Dictionary<string, string> headerDict = null, string accessToken = null);
    }
}
