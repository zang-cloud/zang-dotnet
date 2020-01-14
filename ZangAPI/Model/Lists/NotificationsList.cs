using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Notification list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{Notification}" />
    public class NotificationsList : ObjectsList<Notification>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "notifications")]
        public override ICollection<Notification> Elements { get; set; }
    }
}