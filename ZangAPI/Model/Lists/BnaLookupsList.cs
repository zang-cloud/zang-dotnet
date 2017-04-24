using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Bna lookups list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{BnaLookup}" />
    public class BnaLookupsList : ZangObjectsList<BnaLookup>
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