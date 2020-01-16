using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Application list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{Application}" />
    public class ApplicationsList : ObjectsList<Application>
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