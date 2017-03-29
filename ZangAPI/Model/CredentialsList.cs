using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Credential list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class CredentialsList : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the credentials count.
        /// </summary>
        /// <value>
        /// The credentials count.
        /// </value>
        [JsonProperty(PropertyName = "credentials_count")]
        public int CredentialsCount { get; set; }

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
