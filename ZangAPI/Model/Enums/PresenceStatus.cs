using System.Runtime.Serialization;

namespace AvayaCPaaS.Model.Enums
{
    /// <summary>
    /// Presence status
    /// </summary>
    public enum PresenceStatus
    {
        /// <summary>
        /// The initialize
        /// </summary>
        [EnumMember(Value = "init")]
        INIT,

        /// <summary>
        /// The idle
        /// </summary>
        [EnumMember(Value = "idle")]
        IDLE,

        /// <summary>
        /// The logged in
        /// </summary>
        [EnumMember(Value = "loggedin")]
        LOGGED_IN,

        /// <summary>
        /// The logged out
        /// </summary>
        [EnumMember(Value = "loggedout")]
        LOGGED_OUT
    }
}
