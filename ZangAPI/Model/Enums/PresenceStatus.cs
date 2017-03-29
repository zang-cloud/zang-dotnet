using System.Runtime.Serialization;

namespace ZangAPI.Model.Enums
{
    /// <summary>
    /// Presence status
    /// </summary>
    public enum PresenceStatus
    {
        /// <summary>
        /// The initialize
        /// </summary>
        INIT,

        /// <summary>
        /// The idle
        /// </summary>
        IDLE,

        /// <summary>
        /// The logged in
        /// </summary>
        [EnumMember(Value = "logged-in")]
        LOGGED_IN,

        /// <summary>
        /// The logged out
        /// </summary>
        [EnumMember(Value = "logged-out")]
        LOGGED_OUT
    }
}
