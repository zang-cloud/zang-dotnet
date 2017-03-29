using System.Runtime.Serialization;

namespace ZangAPI.Model.Enums
{
    /// <summary>
    /// Conference status
    /// </summary>
    public enum ConferenceStatus
    {
        /// <summary>
        /// The initialize
        /// </summary>
        INIT,

        /// <summary>
        /// The in progress
        /// </summary>
        [EnumMember(Value = "in-progress")]
        IN_PROGRESS,

        /// <summary>
        /// The completed
        /// </summary>
        COMPLETED
    }
}
