using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Conference list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{Conference}" />
    public class ConferenceList : ZangObjectsList<Conference>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "conferences")]
        public override ICollection<Conference> Elements { get; set; }
    }
}
