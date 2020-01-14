﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    public class CnamLookupsList : ObjectsList<CnamLookup>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "cnam_lookups")]
        public override ICollection<CnamLookup> Elements { get; set; }
    }
}