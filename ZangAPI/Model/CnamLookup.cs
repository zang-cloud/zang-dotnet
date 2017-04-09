using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Cnam lookup
    /// </summary>
    /// <seealso cref="ZangAPI.Model.BaseZangObject" />
    public class CnamLookup : BaseZangObject
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
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

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
