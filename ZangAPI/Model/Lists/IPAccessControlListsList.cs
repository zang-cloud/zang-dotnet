using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// IP access control lists list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{IpAccessControlList}" />
    public class IpAccessControlListsList : ZangObjectsList<IpAccessControlList>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "ip_access_control")]
        public override ICollection<IpAccessControlList> Elements { get; set; }
    }
}