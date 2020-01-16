using Newtonsoft.Json;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Model
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
        /// Gets or sets the lata.
        /// </summary>
        /// <value>
        /// The lata.
        /// </value>
        [JsonProperty(PropertyName = "lata")]
        public string Lata { get; set; }

        /// <summary>
        /// Gets or sets the rate center.
        /// </summary>
        /// <value>
        /// The rate center.
        /// </value>
        [JsonProperty(PropertyName = "rate_center")]
        public string RateCenter { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }


        /// <summary>
        /// Gets or sets the npa.
        /// </summary>
        /// <value>
        /// The npa.
        /// </value>
        [JsonProperty(PropertyName = "npa")]
        public string Npa { get; set; }

        /// <summary>
        /// Gets or sets the exchange.
        /// </summary>
        /// <value>
        /// The exchange.
        /// </value>
        [JsonProperty(PropertyName = "exchange")]
        public string Exchange { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

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
        public AvailablePhoneNumberType Type { get; set; }

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