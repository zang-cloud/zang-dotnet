namespace ZangAPI.Model
{
    /// <summary>
    /// Fraud control rule element
    /// </summary>
    public class FraudControlRuleElement
    {
        /// <summary>
        /// Gets or sets the blocked.
        /// </summary>
        /// <value>
        /// The blocked.
        /// </value>
        public FraudControlRule Blocked { get; set; }

        /// <summary>
        /// Gets or sets the authorized.
        /// </summary>
        /// <value>
        /// The authorized.
        /// </value>
        public FraudControlRule Authorized { get; set; }

        /// <summary>
        /// Gets or sets the whitelisted.
        /// </summary>
        /// <value>
        /// The whitelisted.
        /// </value>
        public FraudControlRule Whitelisted { get; set; }
    }
}
