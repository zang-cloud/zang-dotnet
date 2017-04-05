using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Transcription list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{Transcription}" />
    public class TranscriptionList : ZangObjectsList<Transcription>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "transcriptions")]
        public override ICollection<Transcription> Elements { get; set; }
    }
}