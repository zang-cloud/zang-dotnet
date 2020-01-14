using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// Conference
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.BaseObject" />
    public class Conference : BaseObject
    {
        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [JsonProperty(PropertyName = "friendly_name")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the active participants count.
        /// </summary>
        /// <value>
        /// The active participants count.
        /// </value>
        [JsonProperty(PropertyName = "active_participants_count")]
        public int ActiveParticipantsCount { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConferenceStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the run time.
        /// </summary>
        /// <value>
        /// The run time.
        /// </value>
        [JsonProperty(PropertyName = "run_time")]
        public int RunTime { get; set; }

        /// <summary>
        /// Gets or sets the subresource uris.
        /// </summary>
        /// <value>
        /// The subresource uris.
        /// </value>
        [JsonProperty(PropertyName = "subresource_uris")]
        public ConferenceSubresourceUris SubresourceUris { get; set; }
    }
}
