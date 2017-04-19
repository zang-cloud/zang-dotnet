using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Recording
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class Recording : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the call sid.
        /// </summary>
        /// <value>
        /// The call sid.
        /// </value>
        [JsonProperty(PropertyName = "call_sid")]
        public string CallSid { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [JsonProperty(PropertyName = "duration")]
        public int Duration{ get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the recording URL.
        /// </summary>
        /// <value>
        /// The recording URL.
        /// </value>
        [JsonProperty(PropertyName = "recording_url")]
        public string RecordingUrl { get; set; }
    }
}
