using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Recording list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{Recording}" />
    public class RecordingList : ZangObjectsList<Recording>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "recordings")]
        public override ICollection<Recording> Elements { get; set; }
    }
}