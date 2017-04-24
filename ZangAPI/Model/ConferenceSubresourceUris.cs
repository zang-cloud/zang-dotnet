using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Conference subresources uris
    /// </summary>
    public class ConferenceSubresourceUris
    {
        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        /// <value>
        /// The participants.
        /// </value>
        [JsonProperty(PropertyName = "participants")]
        public string Participants { get; set; }
    }
}
