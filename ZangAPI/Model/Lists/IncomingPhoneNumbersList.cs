using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Incoming phone number list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{IncomingPhoneNumber}" />
    public class IncomingPhoneNumbersList : ObjectsList<IncomingPhoneNumber>
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
