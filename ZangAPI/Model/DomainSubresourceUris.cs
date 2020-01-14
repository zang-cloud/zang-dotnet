using Newtonsoft.Json;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// Domain subresource uris
    /// </summary>
    public class DomainSubresourceUris
    {
        /// <summary>
        /// Gets or sets the ip access control list mappings.
        /// </summary>
        /// <value>
        /// The ip access control list mappings.
        /// </value>
        [JsonProperty(PropertyName = "ip_access_control_list_mappings")]
        public string IpAccessControlListMappings { get; set; }

        /// <summary>
        /// Gets or sets the credential list mappings.
        /// </summary>
        /// <value>
        /// The credential list mappings.
        /// </value>
        [JsonProperty(PropertyName = "credential_list_mappings")]
        public string CredentialListMappings { get; set; }
    }
}
