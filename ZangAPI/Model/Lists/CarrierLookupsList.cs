using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Carrier lookups list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{CarrierLookup}" />
    public class CarrierLookupsList : ObjectsList<CarrierLookup>
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
