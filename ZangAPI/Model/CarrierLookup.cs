using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Carrier lookup
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class CarrierLookup : BaseZangObject
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
        /// Gets or sets the network.
        /// </summary>
        /// <value>
        /// The network.
        /// </value>
        [JsonProperty(PropertyName = "network")]
        public string Network { get; set; }

        /// <summary>
        /// Gets or sets the sponsor network.
        /// </summary>
        /// <value>
        /// The sponsor network.
        /// </value>
        [JsonProperty(PropertyName = "sponsor_network")]
        public string SponsorNetwork { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CarrierLookup"/> is mobile.
        /// </summary>
        /// <value>
        ///   <c>true</c> if mobile; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "mobile")]
        public bool Mobile { get; set; }

        //todo ovoga nema u dokumentaciji
        /// <summary>
        /// Gets or sets a value indicating whether [sponsor mobile].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sponsor mobile]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "sponsor_mobile")]
        public bool SponsorMobile { get; set; }

        /// <summary>
        /// Gets or sets the carrier identifier.
        /// </summary>
        /// <value>
        /// The carrier identifier.
        /// </value>
        [JsonProperty(PropertyName = "carrier_id")]
        public int CarrierId { get; set; }

        /// <summary>
        /// Gets or sets the sponsor carrier identifier.
        /// </summary>
        /// <value>
        /// The sponsor carrier identifier.
        /// </value>
        [JsonProperty(PropertyName = "sponsor_carrier_id")]
        public int SponsorCarrierId { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the sponsor country code.
        /// </summary>
        /// <value>
        /// The sponsor country code.
        /// </value>
        [JsonProperty(PropertyName = "sponsor_country_code")]
        public string SponsorCountryCode { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The MNC.
        /// </value>
        [JsonProperty(PropertyName = "mnc")]
        public string Mnc { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The MCC.
        /// </value>
        [JsonProperty(PropertyName = "mcc")]
        public string Mcc { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
    }
}