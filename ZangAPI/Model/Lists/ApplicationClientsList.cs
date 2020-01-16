using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Application client list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{ApplicationClient}" />
    public class ApplicationClientsList : ObjectsList<ApplicationClient>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "clients")]
        public override ICollection<ApplicationClient> Elements { get; set; }
    }
}
