using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZangAPI.Model.Lists
{
    /// <summary>
    /// Credentials lists list
    /// </summary>
    /// <seealso cref="ZangAPI.Model.Lists.ZangObjectsList{CredentialList}" />
    public class CredentialsListsList : ZangObjectsList<CredentialList>
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
