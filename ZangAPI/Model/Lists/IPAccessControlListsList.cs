using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// IP access control lists list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{IpAccessControlList}" />
    public class IpAccessControlListsList : ObjectsList<IpAccessControlList>
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