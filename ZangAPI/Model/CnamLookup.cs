using Newtonsoft.Json;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// Lookup helper
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.BaseObject" />
    public class CnamLookups : BaseObject
    {
        [JsonProperty(PropertyName = "cnam_lookups")]
        public CnamLookup[] LookupArray { get; set; }

    }

    /// <summary>
    /// Cnam lookup
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.BaseObject" />
    public class CnamLookup : BaseObject
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
