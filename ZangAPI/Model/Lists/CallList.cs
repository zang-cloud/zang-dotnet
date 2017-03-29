using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Call list
    /// </summary>
    public class CallList : ZangObjectsList<Call>
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
