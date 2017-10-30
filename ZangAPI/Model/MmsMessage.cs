using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// MMS message
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class MmsMessage : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the date sent.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        [JsonProperty(PropertyName = "date_created")]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the media url.
        /// </summary>
        /// <value>
        /// The media url.
        /// </value>
        [JsonProperty(PropertyName = "media_url")]
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        public MmsStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        [JsonProperty(PropertyName = "direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MmsDirection Direction { get; set; }
        
    }
}
