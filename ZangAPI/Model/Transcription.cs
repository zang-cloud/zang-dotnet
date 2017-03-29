using Newtonsoft.Json;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// Transcription
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class Transcription : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        public TranscriptionStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty(PropertyName = "type")]
        public TranscriptionType Type { get; set; }

        /// <summary>
        /// Gets or sets the audio URL.
        /// </summary>
        /// <value>
        /// The audio URL.
        /// </value>
        [JsonProperty(PropertyName = "audio_url")]
        public string AudioUrl { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the transcription text.
        /// </summary>
        /// <value>
        /// The transcription text.
        /// </value>
        [JsonProperty(PropertyName = "transcription_text")]
        public string TranscriptionText { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the transcribe callback.
        /// </summary>
        /// <value>
        /// The transcribe callback.
        /// </value>
        [JsonProperty(PropertyName = "transcribe_callback")]
        public string TranscribeCallback { get; set; }

        /// <summary>
        /// Gets or sets the callback method.
        /// </summary>
        /// <value>
        /// The callback method.
        /// </value>
        [JsonProperty(PropertyName = "callback_method")]
        public HttpMethod CallbackMethod { get; set; }
    }
}
