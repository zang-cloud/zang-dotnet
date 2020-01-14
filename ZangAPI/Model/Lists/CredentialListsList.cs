using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// Credentials lists list
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.Lists.ObjectsList{CredentialList}" />
    public class CredentialListsList : ObjectsList<CredentialList>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        [JsonProperty(PropertyName = "credential_lists")]
        public override ICollection<CredentialList> Elements { get; set; }
    }
}
