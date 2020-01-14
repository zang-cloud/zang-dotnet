﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Fraud control rules list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{FraudControlRuleElement}" />
    public class FraudControlRulesList : ObjectsList<FraudControlRuleElement>
    {
        /// <summary>
        /// Gets or sets the blocked.
        /// </summary>
        /// <value>
        /// The blocked.
        /// </value>
        public List<FraudControlRule> Blocked { get; set; }

        /// <summary>
        /// Gets or sets the authorized.
        /// </summary>
        /// <value>
        /// The authorized.
        /// </value>
        public List<FraudControlRule> Authorized { get; set; }

        /// <summary>
        /// Gets or sets the whitelisted.
        /// </summary>
        /// <value>
        /// The whitelisted.
        /// </value>
        public List<FraudControlRule> Whitelisted { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "frauds")]
        public override ICollection<FraudControlRuleElement> Elements { get; set; }
    }
}