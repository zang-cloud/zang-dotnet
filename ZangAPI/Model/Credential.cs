using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class Credential : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the subresource uris.
        /// </summary>
        /// <value>
        /// The subresource uris.
        /// </value>
        [JsonProperty(PropertyName = "subresource_uris")]
        public EmptySubresourceUris SubresourceUris { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [JsonProperty(PropertyName = "friendly_name")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the credential list.
        /// </summary>
        /// <value>
        /// The credential list.
        /// </value>
        [JsonProperty(PropertyName = "credential_list")]
        public string CredentialList { get; set; }
    }
}