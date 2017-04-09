using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Fraud control rules list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{FraudControlRule}" />
    public class FraudControlRuleList : ZangObjectsList<FraudControlRule>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "frauds")]
        public override ICollection<FraudControlRule> Elements { get; set; }
    }
}