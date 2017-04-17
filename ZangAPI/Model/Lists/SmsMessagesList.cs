using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// List of SMS messages
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{SmsMessage}" />
    public class SmsMessagesList : ZangObjectsList<SmsMessage>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "sms_messages")]
        public override ICollection<SmsMessage> Elements { get; set; }
    }
}
