using ApiCommunication;
using ConfigClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Response;
using System;
using System.Reflection;

namespace RestApi
{
    [ApiController]
    [Route("api/v1/health")]
    public class HealthCheckController : ApiBase
    {
        public HealthCheckController(IApiCall apiCall, IConfiguration config, IConfigProvider configProvider) : base(apiCall, config, configProvider) { }

        /// <summary>
        /// This returns a simple payload to the caller that is intended to be a heartbeat check.
        /// It will return a payload indicating success with a message of 'OK'.
        /// </summary>
        [HttpGet]
        [Route("status")]
        public ActionResult<ResponseBase> Status()
        {
            try
            {
                return Ok(new ResponseBase(true, "OK"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ResponseBase.GENERIC_ERROR);
            }
        }

        /// <summary>
        /// The method returns the current server UTC time in a ISO-8601 string format.
        /// </summary>
        [HttpGet]
        [Route("time")]
        public ActionResult<ResponseBase> ServerTime()
        {
            try
            {
                return Ok(new ResponseBase(true, DateTime.UtcNow.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ResponseBase.GENERIC_ERROR);
            }
        }

        /// <summary>
        /// Gets the current version of the API
        /// </summary>
        [HttpGet]
        [Route("version")]
        public ActionResult<ResponseBase> AppVersion()
        {
            try
            {
                return Ok(new ResponseBase(true, Assembly.GetEntryAssembly().GetName().Version.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ResponseBase.GENERIC_ERROR);
            }
        }

        /// <summary>
        /// Gets the current version of the API
        /// </summary>
        [HttpGet]
        [Route("environment")]
        public ActionResult<ResponseBase> Environment()
        {
            try
            {
                return Ok(new ResponseBase(true, _Configuration["Environment"]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ResponseBase.GENERIC_ERROR);
            }
        }
    }
}
