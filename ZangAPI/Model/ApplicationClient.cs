using Newtonsoft.Json;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// Application client
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.BaseObject" />
    public class ApplicationClient : BaseObject
    {
        /// <summary>
        /// Gets or sets the presence status.
        /// </summary>
        /// <value>
        /// The presence status.
        /// </value>
        [JsonProperty(PropertyName = "presence_status")]
        public PresenceStatus PresenceStatus { get; set; }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>
        /// The nickname.
        /// </value>
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the client password.
        /// </summary>
        /// <value>
        /// The client password.
        /// </value>
        [JsonProperty(PropertyName = "client_password")]
        public string ClientPassword { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        [JsonProperty(PropertyName = "session_id")]
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the application sid.
        /// </summary>
        /// <value>
        /// The application sid.
        /// </value>
        [JsonProperty(PropertyName = "application_sid")]
        public string ApplicationSid { get; set; }

        /// <summary>
        /// Gets or sets the remote ip.
        /// </summary>
        /// <value>
        /// The remote ip.
        /// </value>
        [JsonProperty(PropertyName = "remote_ip")]
        public string RemoteIp { get; set; }
    }
}
