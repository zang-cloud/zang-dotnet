using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// IP access control list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class IPAccessControlList : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the ip addresses count.
        /// </summary>
        /// <value>
        /// The ip addresses count.
        /// </value>
        [JsonProperty(PropertyName = "ip_addresses_count")]
        public int IPAddressesCount{ get; set; }

        /// <summary>
        /// Gets or sets the subresource uris.
        /// </summary>
        /// <value>
        /// The subresource uris.
        /// </value>
        [JsonProperty(PropertyName = "subresource_uris")]
        public CredentialSubresourceUris SubresourceUris { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [JsonProperty(PropertyName = "friendly_name")]
        public string FriendlyName { get; set; }
    }
}
