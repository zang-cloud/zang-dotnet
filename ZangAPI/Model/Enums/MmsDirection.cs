using System.Runtime.Serialization;

namespace AvayaCPaaS.Model.Enums
{
    /// <summary>
    /// MMS direction
    /// </summary>
    public enum MmsDirection
    {
        /// <summary>
        /// The outbound
        /// </summary>
        OUTBOUND,

        /// <summary>
        /// The outbound api
        /// </summary>
        [EnumMember(Value = "outbound-api")]
        OUTBOUND_API,

        /// <summary>
        /// The incoming
        /// </summary>
        INCOMING,

        /// <summary>
        /// The outbound call
        /// </summary>
        [EnumMember(Value = "outbound-call")]
        OUTBOUND_CALL,

        /// <summary>
        /// The outbound reply
        /// </summary>
        [EnumMember(Value = "outbound-reply")]
        OUTBOUND_REPLY
    }
}
