using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// Call class
    /// </summary>
    /// <seealso cref="BaseZangObject" />
    public class Call : BaseZangObject
    {
        /// <summary>
        /// Gets the parent call sid.
        /// </summary>
        /// <value>
        /// The parent call sid.
        /// </value>
        [JsonProperty(PropertyName = "parent_call_sid")]
        public string ParentCallSid { get; set; }

        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }

        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the phone number sid.
        /// </summary>
        /// <value>
        /// The phone number sid.
        /// </value>
        [JsonProperty(PropertyName = "phone_number_sid")]
        public string PhoneNumberSid { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(StringEnumConverter))]

        public CallStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        [JsonProperty(PropertyName = "start_time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        [JsonProperty(PropertyName = "end_time")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        [JsonProperty(PropertyName = "direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CallDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the answered by.
        /// </summary>
        /// <value>
        /// The answered by.
        /// </value>
        [JsonProperty(PropertyName = "answered_by")]
        public AnsweredBy AnsweredBy { get; set; }

        /// <summary>
        /// Gets or sets the forwarded from.
        /// </summary>
        /// <value>
        /// The forwarded from.
        /// </value>
        [JsonProperty(PropertyName = "forwarded_from")]
        public string ForwardedFrom { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [caller identifier blocked].
        /// </summary>
        /// <value>
        /// <c>true</c> if [caller identifier blocked]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "caller_id_blocked")]
        public bool CallerIdBlocked { get; set; }

        /// <summary>
        /// Gets or sets the call subresource uris.
        /// </summary>
        /// <value>
        /// The call subresource uris.
        /// </value>
        [JsonProperty(PropertyName = "subresource_uris")]
        public CallSubresourceUris CallSubresourceUris { get; set; }

        /// <summary>
        /// Gets or sets the recordings count.
        /// </summary>
        /// <value>
        /// The recordings count.
        /// </value>
        [JsonProperty(PropertyName = "recordings_count")]
        public int? RecordingsCount { get; set; }

        /// <summary>
        /// Gets or sets the duration billed.
        /// </summary>
        /// <value>
        /// The duration billed.
        /// </value>
        [JsonProperty(PropertyName = "duration_billed")]
        public int DurationBilled { get; set; }
    }
}
