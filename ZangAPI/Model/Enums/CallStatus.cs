using System.Runtime.Serialization;

namespace ZangAPI.Model.Enums
{
    /// <summary>
    /// Call status
    /// </summary>
    public enum CallStatus
    {
        /// <summary>
        /// The queued
        /// </summary>
        QUEUED,

        /// <summary>
        /// The ringing
        /// </summary>
        RINGING,

        /// <summary>
        /// 
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
        FAILED,

        /// <summary>
        /// The busy
        /// </summary>
        BUSY,

        /// <summary>
        /// The no answer
        /// </summary>
        [EnumMember(Value = "no-answer")]
        NO_ANSWER,

        /// <summary>
        /// The unknown
        /// </summary>
        UNKNOWN,

        /// <summary>
        /// The pre-queued
        /// </summary>
        [EnumMember(Value = "pre-queued")]
        PRE_QUEUED,

        /// <summary>
        /// The canceled
        /// </summary>
        CANCELED
    }
}
