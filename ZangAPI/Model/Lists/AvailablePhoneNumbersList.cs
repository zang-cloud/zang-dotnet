using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Available phone number list 
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{AvailablePhoneNumber}" />
    public class AvailablePhoneNumbersList : ObjectsList<AvailablePhoneNumber>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "available_phone_numbers")]
        public override ICollection<AvailablePhoneNumber> Elements { get; set; }
    }
}