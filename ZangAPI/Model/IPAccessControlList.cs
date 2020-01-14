using Newtonsoft.Json;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// IP access control list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.BaseObject" />
    public class IpAccessControlList : BaseObject
    {
        /// <summary>
        /// Gets or sets the ip addresses count.
        /// </summary>
        /// <value>
        /// The ip addresses count.
        /// </value>
        [JsonProperty(PropertyName = "ip_addresses_count")]
        public int IpAddressesCount{ get; set; }

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
