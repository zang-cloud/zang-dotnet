using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Recording list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{Recording}" />
    public class RecordingsList : ObjectsList<Recording>
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