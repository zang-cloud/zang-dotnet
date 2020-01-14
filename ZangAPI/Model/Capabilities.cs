using Newtonsoft.Json;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// Capabilities
    /// </summary>
    public class Capabilities
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Capabilities"/> is voice.
        /// </summary>
        /// <value>
        ///   <c>true</c> if voice; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "voice")]
        public bool Voice { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        ///   <c>true</c> if SMS; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "sms")]
        public bool Sms { get; set; }
    }
}
