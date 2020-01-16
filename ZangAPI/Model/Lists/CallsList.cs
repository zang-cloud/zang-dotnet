using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Call list
    /// </summary>
    public class CallsList : ObjectsList<Call>
    {
        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "calls")]
        public override ICollection<Call> Elements { get; set; }
    }
}
