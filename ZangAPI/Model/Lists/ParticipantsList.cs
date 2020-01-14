using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Participant list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{Participant}" />
    public class ParticipantsList : ObjectsList<Participant>
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
