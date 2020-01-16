using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    public class CredentialsList : ObjectsList<Credential>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "credentials")]
        public override ICollection<Credential> Elements { get; set; }
    }
}