using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// IP addresses list
    /// </summary>
    /// <seealso cref="IpAddress" />
    public class IpAddressesList : ZangObjectsList<IpAddress>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "ip_addresses")]
        public override ICollection<IpAddress> Elements { get; set; }
    }
}
