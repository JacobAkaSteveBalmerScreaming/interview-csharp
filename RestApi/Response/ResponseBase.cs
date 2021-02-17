using Newtonsoft.Json;

namespace Response
{
    public class ResponseBase
    {
        public const string ERROR_SERVER_ERROR = "A server error occurred";
        public const string ERROR_MISSING_PARAMETERS = "One or more parameters are missing or invalid";
        public const string ERROR_UNAUTHORIZED = "Unauthorized";

        public static readonly ResponseBase DEFAULT_SUCCESS = new ResponseBase(true, string.Empty, string.Empty);
        public static readonly ResponseBase DEFAULT_FAILURE = new ResponseBase(false, string.Empty);
        public static readonly ResponseBase GENERIC_ERROR = new ResponseBase(false, ERROR_SERVER_ERROR);
        public static readonly ResponseBase MISSING_OR_INVALID_PARAMETERS = new ResponseBase(false, ERROR_MISSING_PARAMETERS);
        public static readonly ResponseBase UNAUTHORIZED = new ResponseBase(false, ERROR_UNAUTHORIZED);

        /// <summary>
        /// Boolean flag indicating if the call was ultimately successful or not.
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// This message string ins intended to be a user friendly string indicating the result of 
        /// the request. It is optional, and can be used for either success or failure messages.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// This is an optional error code that can be passed forward
        /// to a user to expedite investigation of user issues.
        /// </summary>
        [JsonProperty(PropertyName = "errorCode")]
        public string ErrorCode { get; set; }

        public ResponseBase() { }

        public ResponseBase(bool success, string message, string errorCode = null)
        {
            Success = success;
            Message = message;
            ErrorCode = errorCode;
        }
    }
}