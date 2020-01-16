using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Transcription list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{Transcription}" />
    public class TranscriptionsList : ObjectsList<Transcription>
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