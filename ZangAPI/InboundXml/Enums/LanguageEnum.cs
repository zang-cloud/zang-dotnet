namespace ZangAPI.InboundXml.Enums
{
    /// <summary>
    /// The enumerator for the language values.
    /// </summary>
    public enum LanguageEnum
    {
        en,
        enGb,
        es,
        fr,
        it,
        de
    }

    /// <summary>
    /// Extensions for the language enum.
    /// </summary>
    public static class LanguageEnumExtensions
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this LanguageEnum value)
        {
            // if value is british - returns custom value
            if (value == LanguageEnum.enGb)
                return "en-gb";
            else
                // else - returns normal to string
                return value.ToString();
        }
    }
}
