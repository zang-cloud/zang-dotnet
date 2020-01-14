using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Bna lookups list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{BnaLookup}" />
    public class BnaLookupsList : ObjectsList<BnaLookup>
    {
        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "bna_lookups")]
        public override ICollection<BnaLookup> Elements { get; set; }
    }
}