using System;
using Newtonsoft.Json;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// Fraud control rule
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class FraudControlRule : BaseZangObject
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty(PropertyName = "type")]
        public FraudControlType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is lock.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is lock; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "is_lock")]
        public bool IsLock { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [mobile enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [mobile enabled]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "mobile_enabled")]
        public bool MobileEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [landline enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [landline enabled]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "landline_enabled")]
        public bool LandlineEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [SMS enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [SMS enabled]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "sms_enabled")]
        public bool SmsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the country prefix.
        /// </summary>
        /// <value>
        /// The country prefix.
        /// </value>
        [JsonProperty(PropertyName = "country_prefix")]
        public string CountryPrefix { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        [JsonProperty(PropertyName = "expiration_date")]
        public DateTime ExpirationDate { get; set; }
    }
}