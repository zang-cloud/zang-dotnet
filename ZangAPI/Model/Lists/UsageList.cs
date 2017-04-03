using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Usage list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{Usage}" />
    public class UsageList : ZangObjectsList<Usage>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "usages")]
        public override ICollection<Usage> Elements { get; set; }
    }
}
