using System;

namespace ZangAPI.Helpers
{
    /// <summary>
    /// Helper methods for handling enumerators
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Parses the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>Returns T from string</returns>
        public static T ParseEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Gets the enum value.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns>Returns enum string value</returns>
        public static string GetEnumValue(Enum e)
        {
            var lowercase = e.ToString().ToLower();
            return lowercase.Replace("_", "-");
        }
    }
}
