using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Ip address
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class IpAddress : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        [JsonProperty(PropertyName = "ip_address")]
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [JsonProperty(PropertyName = "friendly_name")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the subresource uris.
        /// </summary>
        /// <value>
        /// The subresource uris.
        /// </value>
        [JsonProperty(PropertyName = "subresource_uris")]
        public EmptySubresourceUris SubresourceUris { get; set; }
    }
}
