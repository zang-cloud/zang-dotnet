using System.Runtime.Serialization;

namespace ZangAPI.Model.Enums
{
    /// <summary>
    /// Call direction
    /// </summary>
    public enum CallDirection
    {
        /// <summary>
        /// The inbound
        /// </summary>
        INBOUND,

        /// <summary>
        /// The outbound api
        /// </summary>
        [EnumMember(Value = "outbound-api")]
        OUTBOUND_API,

        /// <summary>
        /// The outbound dial
        /// </summary>
        [EnumMember(Value = "outbound-dial")]
        OUTBOUND_DIAL,

        /// <summary>
        /// The unknown
        /// </summary>
        UNKNOWN
    }
}
