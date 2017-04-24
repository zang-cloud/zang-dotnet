using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Incoming phone number list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{IncomingPhoneNumber}" />
    public class IncomingPhoneNumbersList : ZangObjectsList<IncomingPhoneNumber>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "incoming_phone_numbers")]
        public override ICollection<IncomingPhoneNumber> Elements { get; set; }
    }
}
