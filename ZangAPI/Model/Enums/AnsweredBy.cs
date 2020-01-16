using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace AvayaCPaaS.Model.Enums
{
    /// <summary>
    /// Answered by
    /// </summary>
    public enum AnsweredBy
    {
        /// <summary>
        /// The human
        /// </summary>
        HUMAN,

        /// <summary>
        /// The machine
        /// </summary>
        MACHINE,

        /// <summary>
        /// </summary>
        TBD,

        /// <summary>
        /// The unknown
        /// </summary>
        UNKNOWN,

        /// <summary>
        /// The nobody
        /// </summary>
        NOBODY,

        /// <summary>
        /// Trunc
        /// </summary>
        TRUNC,

        /// <summary>
        /// Telapi
        /// </summary>
        TELAPI
    }
}
