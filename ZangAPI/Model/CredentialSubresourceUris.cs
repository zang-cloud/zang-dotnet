using Newtonsoft.Json;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// Credential subresource uris
    /// </summary>
    public class CredentialSubresourceUris
    {
        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        [JsonProperty(PropertyName = "credentials")]
        public string Credentials { get; set; }
    }
}
