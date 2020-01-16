using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace AvayaCPaaS.Model.Enums
{
    /// <summary>
    /// Transcription status
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TranscriptionStatus
    {
        /// <summary>
        /// The in progress
        /// </summary>
        [EnumMember(Value = "in-progress")]
        IN_PROGRESS, 

        /// <summary>
        /// The completed
        /// </summary>
        COMPLETED,

        /// <summary>
        /// The failed
        /// </summary>
        FAILED
    }
}
