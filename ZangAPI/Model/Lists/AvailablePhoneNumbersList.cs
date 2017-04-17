using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Available phone number list 
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{AvailablePhoneNumber}" />
    public class AvailablePhoneNumbersList : ZangObjectsList<AvailablePhoneNumber>
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