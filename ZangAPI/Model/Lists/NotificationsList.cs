using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Notification list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{Notification}" />
    public class NotificationsList : ZangObjectsList<Notification>
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