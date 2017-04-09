using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// IP access control lists list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{IPAccessControlList}" />
    public class IPAccessControlListsList : ZangObjectsList<IPAccessControlList>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "ip_access_control")]
        public override ICollection<IPAccessControlList> Elements { get; set; }
    }
}