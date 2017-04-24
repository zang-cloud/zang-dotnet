using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Application client list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{ApplicationClient}" />
    public class ApplicationClientsList : ZangObjectsList<ApplicationClient>
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
