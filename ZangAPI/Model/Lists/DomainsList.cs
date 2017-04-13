using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Domains list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{Domain}" />
    public class DomainsList : ZangObjectsList<Domain>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "domains")]
        public override ICollection<Domain> Elements { get; set; }
    }
}
