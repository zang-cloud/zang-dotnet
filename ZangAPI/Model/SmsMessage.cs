using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// SMS message
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class SmsMessage : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the date sent.
        /// </summary>
        /// <value>
        /// The date sent.
        /// </value>
        [JsonProperty(PropertyName = "date_sent")]
        public DateTime DateSent { get; set; }

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
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        [JsonProperty(PropertyName = "direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SmsDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
    }
}
