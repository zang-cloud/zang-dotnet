using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Participant list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{Participant}" />
    public class ParticipantsList : ZangObjectsList<Participant>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "participants")]
        public override ICollection<Participant> Elements { get; set; }
    }
}
