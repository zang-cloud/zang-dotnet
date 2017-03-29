using Newtonsoft.Json;

namespace ZangAPI.Model
{
    /// <summary>
    /// Subresource uris
    /// </summary>
    public class SubresourceUris
    {
        /// <summary>
        /// Gets or sets the available phone numbers.
        /// </summary>
        /// <value>
        /// The available phone numbers.
        /// </value>
        [JsonProperty(PropertyName = "available_phone_numbers")]
        public string AvailablePhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the calls.
        /// </summary>
        /// <value>
        /// The calls.
        /// </value>
        [JsonProperty(PropertyName = "calls")]
        public string Calls { get; set; }

        /// <summary>
        /// Gets or sets the conferences.
        /// </summary>
        /// <value>
        /// The conferences.
        /// </value>
        [JsonProperty(PropertyName = "conferences")]
        public string Conferences { get; set; }

        /// <summary>
        /// Gets or sets the incoming phone numbers.
        /// </summary>
        /// <value>
        /// The incoming phone numbers.
        /// </value>
        [JsonProperty(PropertyName = "incoming_phone_numbers")]
        public string IncomingPhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the notifications.
        /// </summary>
        /// <value>
        /// The notifications.
        /// </value>
        [JsonProperty(PropertyName = "notifications")]
        public string Notifications { get; set; }

        /// <summary>
        /// Gets or sets the recordings.
        /// </summary>
        /// <value>
        /// The recordings.
        /// </value>
        [JsonProperty(PropertyName = "recordings")]
        public string Recordings { get; set; }

        /// <summary>
        /// Gets or sets the SMS messages.
        /// </summary>
        /// <value>
        /// The SMS messages.
        /// </value>
        [JsonProperty(PropertyName = "sms_messages")]
        public string SmsMessages { get; set; }

        /// <summary>
        /// Gets or sets the transcriptions.
        /// </summary>
        /// <value>
        /// The transcriptions.
        /// </value>
        [JsonProperty(PropertyName = "transcriptions")]
        public string Transcriptions { get; set; }

        /// <summary>
        /// Gets or sets the transactions.
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        [JsonProperty(PropertyName = "transactions")]
        public string Transactions { get; set; }

        /// <summary>
        /// Gets or sets the applications.
        /// </summary>
        /// <value>
        /// The applications.
        /// </value>
        [JsonProperty(PropertyName = "applications")]
        public string Applications { get; set; }

        /// <summary>
        /// Gets or sets the fraud.
        /// </summary>
        /// <value>
        /// The fraud.
        /// </value>
        [JsonProperty(PropertyName = "fraud")]
        public string Fraud { get; set; }

        /// <summary>
        /// Gets or sets the cnam.
        /// </summary>
        /// <value>
        /// The cnam.
        /// </value>
        [JsonProperty(PropertyName = "cnam")]
        public string Cnam { get; set; }

        /// <summary>
        /// Gets or sets the bna.
        /// </summary>
        /// <value>
        /// The bna.
        /// </value>
        [JsonProperty(PropertyName = "bna")]
        public string Bna { get; set; }

        /// <summary>
        /// Gets or sets the carrier.
        /// </summary>
        /// <value>
        /// The carrier.
        /// </value>
        [JsonProperty(PropertyName = "carrier")]
        public string Carrier { get; set; }

        /// <summary>
        /// Gets or sets the usages.
        /// </summary>
        /// <value>
        /// The usages.
        /// </value>
        [JsonProperty(PropertyName = "usages")]
        public string Usages { get; set; }
    }
}
