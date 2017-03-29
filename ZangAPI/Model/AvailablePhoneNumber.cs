using Newtonsoft.Json;
using ZangAPI.Model.Enums;

namespace ZangAPI.Model
{
    /// <summary>
    /// Available phone number
    /// </summary>
    public class AvailablePhoneNumber
    {
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [unblock support].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [unblock support]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "unblock_support")]
        public bool UnblockSupport { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [JsonProperty(PropertyName = "friendly_name")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the iso country.
        /// </summary>
        /// <value>
        /// The iso country.
        /// </value>
        [JsonProperty(PropertyName = "iso_country")]
        public string IsoCountry { get; set; }

        /// <summary>
        /// Gets or sets the monthly cost.
        /// </summary>
        /// <value>
        /// The monthly cost.
        /// </value>
        [JsonProperty(PropertyName = "monthly_cost")]
        public decimal MonthlyCost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [voice enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [voice enabled]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "voice_enabled")]
        public bool VoiceEnabled { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the setup cost.
        /// </summary>
        /// <value>
        /// The setup cost.
        /// </value>
        [JsonProperty(PropertyName = "setup_cost")]
        public decimal SetupCost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [supports forwarded from].
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports forwarded from]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "supports_forwarded_from")]
        public bool SupportsForwardedFrom { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty(PropertyName = "type")]
        public PhoneNumberType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [SMS enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [SMS enabled]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "sms_enabled")]
        public bool SmsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the per minute cost.
        /// </summary>
        /// <value>
        /// The per minute cost.
        /// </value>
        [JsonProperty(PropertyName = "per_minute_cost")]
        public decimal PerMinuteCost { get; set; }
    }
}