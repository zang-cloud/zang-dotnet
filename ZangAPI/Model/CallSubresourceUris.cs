using Newtonsoft.Json;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// Call subresource uris
    /// </summary>
    public class CallSubresourceUris
    {
        /// <summary>
        /// Gets or sets the notifications.
        /// </summary>
        /// <value>
        /// The notifications.
        /// </value>
        [JsonProperty(PropertyName = "notifications")]
        public string Notifications { get; set; }

        /// <summary>
        /// Gets or sets the recordings.
        /// </summary>
        /// <value>
        /// The recordings.
        /// </value>
        [JsonProperty(PropertyName = "recordings")]
        public string Recordings { get; set; }
    }
}
