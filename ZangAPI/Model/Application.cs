using Newtonsoft.Json;
using RestSharp;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// Application
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class Application : BaseZangObject
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
        /// Gets or sets the voice URL.
        /// </summary>
        /// <value>
        /// The voice URL.
        /// </value>
        [JsonProperty(PropertyName = "voice_url")]
        public string VoiceUrl { get; set; }

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

        /// <summary>
        /// Gets or sets the voice fallback method.
        /// </summary>
        /// <value>
        /// The voice fallback method.
        /// </value>
        [JsonProperty(PropertyName = "voice_fallback_method")]
        public HttpMethod VoiceFallbackMethod { get; set; }

        /// <summary>
        /// Gets or sets the client count.
        /// </summary>
        /// <value>
        /// The client count.
        /// </value>
        [JsonProperty(PropertyName = "client_count")]
        public int ClientCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [voice caller identifier lookup].
        /// </summary>
        /// <value>
        /// <c>true</c> if [voice caller identifier lookup]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "voice_caller_id_lookup")]
        public bool VoiceCallerIdLookup { get; set; }

        /// <summary>
        /// Gets or sets the SMS URL.
        /// </summary>
        /// <value>
        /// The SMS URL.
        /// </value>
        [JsonProperty(PropertyName = "sms_url")]
        public string SmsUrl { get; set; }

        /// <summary>
        /// Gets or sets the SMS method.
        /// </summary>
        /// <value>
        /// The SMS method.
        /// </value>
        [JsonProperty(PropertyName = "sms_method")]
        public HttpMethod SmsMethod { get; set; }

        /// <summary>
        /// Gets or sets the SMS fallback URL.
        /// </summary>
        /// <value>
        /// The SMS fallback URL.
        /// </value>
        [JsonProperty(PropertyName = "sms_fallback_url")]
        public string SmsFallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the SMS fallback method.
        /// </summary>
        /// <value>
        /// The SMS fallback method.
        /// </value>
        [JsonProperty(PropertyName = "sms_fallback_method")]
        public HttpMethod SmsFallbackMethod { get; set; }

        /// <summary>
        /// Gets or sets the heartbeat URL.
        /// </summary>
        /// <value>
        /// The heartbeat URL.
        /// </value>
        [JsonProperty(PropertyName = "heartbeat_url")]
        public string HeartbeatUrl { get; set; }

        /// <summary>
        /// Gets or sets the heartbeat method.
        /// </summary>
        /// <value>
        /// The heartbeat method.
        /// </value>
        [JsonProperty(PropertyName = "heartbeat_method")]
        public HttpMethod HeartbeatMethod { get; set; }


        /// <summary>
        /// Gets or sets the status callback.
        /// </summary>
        /// <value>
        /// The status callback.
        /// </value>
        [JsonProperty(PropertyName = "status_callback")]
        public string StatusCallback { get; set; }

        /// <summary>
        /// Gets or sets the status callback method.
        /// </summary>
        /// <value>
        /// The status callback method.
        /// </value>
        [JsonProperty(PropertyName = "status_callback_method")]
        public HttpMethod StatusCallbackMethod { get; set; }

        /// <summary>
        /// Gets or sets the hangup callback.
        /// </summary>
        /// <value>
        /// The hangup callback.
        /// </value>
        [JsonProperty(PropertyName = "hangup_callback")]
        public string HangupCallback { get; set; }

        /// <summary>
        /// Gets or sets the hangup callback method.
        /// </summary>
        /// <value>
        /// The hangup callback method.
        /// </value>
        [JsonProperty(PropertyName = "hangup_callback_method")]
        public HttpMethod HangupCallbackMethod { get; set; }
    }
}