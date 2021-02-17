using ApiCommunication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Reflection;
using ConfigClient;

namespace RestApi
{
    /// <summary>
    /// This class was written to provide all the controllers will a standard 
    /// set of objects that will be commonly needed for routine operation.
    /// </summary>
    public abstract class ApiBase : ControllerBase
    {
        protected readonly IApiCall _ApiCall;
        protected readonly IConfiguration _Configuration;
        protected readonly IConfigProvider _ConfigProvider;

        public ApiBase(IApiCall apiCall, IConfiguration config, IConfigProvider configProvider)
        {
            _ApiCall = apiCall;
            _Configuration = config;
            _ConfigProvider = configProvider;
        }

        /// <summary>
        /// Generates a error message name in the form of: 'Exception caught in FooController::GetBar()'
        /// </summary>
        protected string GenerateExceptionMessage(MethodBase method)
        {
            return $"Exception caught in {GetType().Name}::{method.Name}()";
        }

        /// <summary>
        /// Basic Telemetry tags to maintain consistent metric tagging.
        /// </summary>
        protected string[] GetTelemetryTags(MethodBase method)
        {
            return new string[] {
                $"controller:{GetType().Name.Replace("Controller", string.Empty).ToLower()}",
                $"route:{method.Name.ToLower()}"
            };
        }

        /// <summary>
        /// Basic Telemetry tags plus HTTP status code to maintain consistent metric tagging.
        /// </summary>
        protected string[] GetTelemetryTags(MethodBase method, HttpStatusCode statusCode)
        {
            return new string[] {
                $"controller:{GetType().Name.Replace("Controller", string.Empty).ToLower()}",
                $"route:{method.Name.ToLower()}",
                $"status_code:{(int)statusCode}",
            };
        }
    }
}
