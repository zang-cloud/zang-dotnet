using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Application list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{Application}" />
    public class ApplicationsList : ZangObjectsList<Application>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "applications")]
        public override ICollection<Application> Elements { get; set; }
    }
}