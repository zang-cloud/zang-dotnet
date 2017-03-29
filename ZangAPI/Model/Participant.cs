using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Participant
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class Participant : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the conference sid.
        /// </summary>
        /// <value>
        /// The conference sid.
        /// </value>
        [JsonProperty(PropertyName = "conference_sid")]
        public string ConferenceSid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Participant"/> is muted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if muted; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "muted")]
        public bool Muted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Participant"/> is deaf.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deaf; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "deaf")]
        public bool Deaf { get; set; }

        /// <summary>
        /// Gets or sets the name of the caller.
        /// </summary>
        /// <value>
        /// The name of the caller.
        /// </value>
        [JsonProperty(PropertyName = "caller_name")]
        public string CallerName { get; set; }

        /// <summary>
        /// Gets or sets the caller number.
        /// </summary>
        /// <value>
        /// The caller number.
        /// </value>
        [JsonProperty(PropertyName = "caller_number")]
        public string CallerNumber { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }
    }
}

