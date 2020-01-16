namespace AvayaCPaaS.InboundXml.Enums
{
    /// <summary>
    /// The enumerator for the language values.
    /// </summary>
    public enum GatherInputEnum
    {
        speech,
        dtmf,
        speech_dtmf
    }

    /// <summary>
    /// Extensions for the language enum.
    /// </summary>
    public static class GatherInputEnumExtensions
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this GatherInputEnum value)
        {
            // if value is speech_dtmf - returns custom value
            if (value == GatherInputEnum.speech_dtmf)
                return "speech dtmf";
            else
                // else - returns normal to string
                return value.ToString();
        }
    }
}
