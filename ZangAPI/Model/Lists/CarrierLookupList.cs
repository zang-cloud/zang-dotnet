using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Carrier lookups list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{CarrierLookup}" />
    public class CarrierLookupList : ZangObjectsList<CarrierLookup>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "carrier_lookups")]
        public override ICollection<CarrierLookup> Elements { get; set; }
    }
}
