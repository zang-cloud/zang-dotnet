using Newtonsoft.Json;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Model
{
    /// <summary>
    /// Account
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Model.BaseObject" />
    public class Account : BaseObject
    {
        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [JsonProperty(PropertyName = "friendly_name")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        public AccountStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the account balance.
        /// </summary>
        /// <value>
        /// The account balance.
        /// </value>
        [JsonProperty(PropertyName = "account_balance")]
        public decimal AccountBalance { get; set; }

        /// <summary>
        /// Gets or sets the maximum outbound limit.
        /// </summary>
        /// <value>
        /// The maximum outbound limit.
        /// </value>
        [JsonProperty(PropertyName = "max_outbound_limit")]
        public string MaxOutboundLimit { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty(PropertyName = "type")]
        public AccountType Type { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        [JsonProperty(PropertyName = "time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the subresource uris.
        /// </summary>
        /// <value>
        /// The subresource uris.
        /// </value>
        [JsonProperty(PropertyName = "subresource_uris")]
        public AccountSubresourceUris AccountSubresourceUris { get; set; }
    }
}
