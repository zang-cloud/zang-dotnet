using Newtonsoft.Json;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// Domain
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class Domain : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the type of the authentication.
        /// </summary>
        /// <value>
        /// The type of the authentication.
        /// </value>
        [JsonProperty(PropertyName = "auth_type")]
        public string AuthType { get; set; }

        /// <summary>
        /// Gets or sets the voice status callback method.
        /// </summary>
        /// <value>
        /// The voice status callback method.
        /// </value>
        [JsonProperty(PropertyName = "voice_status_callback_method")]
        public HttpMethod VoiceStatusCallbackMethod { get; set; }

        /// <summary>
        /// Gets or sets the voice hearbeat callback.
        /// </summary>
        /// <value>
        /// The voice hearbeat callback.
        /// </value>
        [JsonProperty(PropertyName = "voice_heartbeat_callback")]
        public string VoiceHearbeatCallback { get; set; }

        /// <summary>
        /// Gets or sets the subresource uris.
        /// </summary>
        /// <value>
        /// The subresource uris.
        /// </value>
        [JsonProperty(PropertyName = "subresource_uris")]
        public DomainSubresourceUris SubresourceUris { get; set; }

        /// <summary>
        /// Gets or sets the domain sip URL.
        /// </summary>
        /// <value>
        /// The domain sip URL.
        /// </value>
        [JsonProperty(PropertyName = "domain_sip_url")]
        public string DomainSipUrl { get; set; }

        /// <summary>
        /// Gets or sets the voice status callback URL.
        /// </summary>
        /// <value>
        /// The voice status callback URL.
        /// </value>
        [JsonProperty(PropertyName = "voice_status_callback_url")]
        public string VoiceStatusCallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [JsonProperty(PropertyName = "friendly_name")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the domain.
        /// </summary>
        /// <value>
        /// The name of the domain.
        /// </value>
        [JsonProperty(PropertyName = "domain_name")]
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the voice URL.
        /// </summary>
        /// <value>
        /// The voice URL.
        /// </value>
        [JsonProperty(PropertyName = "voice_url")]
        public string VoiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the voice heartbeat callback method.
        /// </summary>
        /// <value>
        /// The voice heartbeat callback method.
        /// </value>
        [JsonProperty(PropertyName = "voice_heartbeat_callback_method")]
        public HttpMethod VoiceHeartbeatCallbackMethod { get; set; }

        /// <summary>
        /// Gets or sets the voice fallback method.
        /// </summary>
        /// <value>
        /// The voice fallback method.
        /// </value>
        [JsonProperty(PropertyName = "voice_fallback_method")]
        public HttpMethod VoiceFallbackMethod { get; set; }

        /// <summary>
        /// Gets or sets the voice method.
        /// </summary>
        /// <value>
        /// The voice method.
        /// </value>
        [JsonProperty(PropertyName = "voice_method")]
        public HttpMethod VoiceMethod { get; set; }

        /// <summary>
        /// Gets or sets the voice fallback URL.
        /// </summary>
        /// <value>
        /// The voice fallback URL.
        /// </value>
        [JsonProperty(PropertyName = "voice_fallback_url")]
        public string VoiceFallbackUrl { get; set; }
    }
}