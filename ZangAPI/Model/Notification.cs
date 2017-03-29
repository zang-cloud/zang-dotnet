using System;
using Newtonsoft.Json;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// Notification
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class Notification : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the call sid.
        /// </summary>
        /// <value>
        /// The call sid.
        /// </value>
        [JsonProperty(PropertyName = "call_sid")]
        public string CallSid { get; set; }

        /// <summary>
        /// Gets or sets the log.
        /// </summary>
        /// <value>
        /// The log.
        /// </value>
        [JsonProperty(PropertyName = "log")]
        public int Log { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        [JsonProperty(PropertyName = "error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the more information.
        /// </summary>
        /// <value>
        /// The more information.
        /// </value>
        [JsonProperty(PropertyName = "more_info")]
        public string MoreInfo { get; set; }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        /// <value>
        /// The message text.
        /// </value>
        [JsonProperty(PropertyName = "message_text")]
        public string MessageText { get; set; }

        /// <summary>
        /// Gets or sets the message date.
        /// </summary>
        /// <value>
        /// The message date.
        /// </value>
        [JsonProperty(PropertyName = "message_date")]
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// Gets or sets the response body.
        /// </summary>
        /// <value>
        /// The response body.
        /// </value>
        [JsonProperty(PropertyName = "response_body")]
        public string ResponseBody { get; set; }

        /// <summary>
        /// Gets or sets the request method.
        /// </summary>
        /// <value>
        /// The request method.
        /// </value>
        [JsonProperty(PropertyName = "request_method")]
        public HttpMethod RequestMethod { get; set; }

        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        /// <value>
        /// The request URL.
        /// </value>
        [JsonProperty(PropertyName = "request_url")]
        public string RequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the request variables.
        /// </summary>
        /// <value>
        /// The request variables.
        /// </value>
        [JsonProperty(PropertyName = "request_variables")]
        public string RequestVariables { get; set; }

        /// <summary>
        /// Gets or sets the response headers.
        /// </summary>
        /// <value>
        /// The response headers.
        /// </value>
        [JsonProperty(PropertyName = "response_headers")]
        public string ResponseHeaders { get; set; }
    }
}
