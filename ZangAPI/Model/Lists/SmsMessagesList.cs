using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// List of SMS messages
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{SmsMessage}" />
    public class SmsMessagesList : ObjectsList<SmsMessage>
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
