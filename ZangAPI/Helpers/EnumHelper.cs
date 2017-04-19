using System;
using System.Linq;
using System.Runtime.Serialization;
using ZangAPI.Model.Enums;

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
            //todo kad je drugacije ili ovo ili enum members
            if (e is AvailablePhoneNumberType)
            {
                var eString = e.ToString().ToLower();
                return eString.First().ToString().ToUpper() + eString.Substring(1);
            }

            var lowercase = e.ToString().ToLower();
            return lowercase.Replace("_", "-");
        }

        public static string GetEnumMemberValue(Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var enumMembers = memInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
            return ((EnumMemberAttribute)enumMembers[0]).Value;
        }
    }
}
