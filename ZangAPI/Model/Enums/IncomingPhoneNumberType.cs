using System.Runtime.Serialization;

namespace AvayaCPaaS.Model.Enums
{
    /// <summary>
    /// Incoming phone number type
    /// </summary>
    public enum IncomingPhoneNumberType
    {
        /// <summary>
        /// The local
        /// </summary>
        LOCAL,

        /// <summary>
        /// The international
        /// </summary>
        INTERNATIONAL,

        /// <summary>
        /// The tollfree
        /// </summary>
        [EnumMember(Value = "toll-free")]
        TOLL_FREE,

        /// <summary>
        /// Ported
        /// </summary>
        PORTED,

    }
}
