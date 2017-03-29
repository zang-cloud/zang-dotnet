using System;
using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Base Zang model object
    /// </summary>
    public class BaseZangObject
    {
    
        /// <summary>
        /// Gets the sid.
        /// </summary>
        /// <value>
        /// The sid.
        /// </value>
        [JsonProperty(PropertyName = "sid")]
        public string Sid { get; set; }

        /// <summary>
        /// Gets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        [JsonProperty(PropertyName = "date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets the date updated.
        /// </summary>
        /// <value>
        /// The date updated.
        /// </value>
        [JsonProperty(PropertyName = "date_updated")]
        public DateTime DateUpdated { get; set; }

        /// <summary>
        /// Gets the account sid.
        /// </summary>
        /// <value>
        /// The account sid.
        /// </value>
        [JsonProperty(PropertyName = "account_sid")]
        public string AccountSid { get; set; }

        /// <summary>
        /// Gets the API version.
        /// </summary>
        /// <value>
        /// The API version.
        /// </value>
        [JsonProperty(PropertyName = "api_version")]
        public string ApiVersion { get; set; }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}
