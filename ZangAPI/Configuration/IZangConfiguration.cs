namespace AvayaCPaaS.Configuration
{
    /// <summary>
    /// CPaaS configuration interface
    /// </summary>
    public interface IAPIConfiguration
    {
        /// <summary>
        /// Gets the account sid.
        /// </summary>
        /// <value>
        /// The account sid.
        /// </value>
        string AccountSid { get; }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <value>
        /// The authentication token.
        /// </value>
        string AuthToken { get; }

        /// <summary>
        /// Gets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        string BaseUrl { get; }

        /// <summary>
        /// Gets the proxy host.
        /// </summary>
        /// <value>
        /// The proxy host.
        /// </value>
        string ProxyHost { get; }

        /// <summary>
        /// Gets the proxy port.
        /// </summary>
        /// <value>
        /// The proxy port.
        /// </value>
        string ProxyPort { get; }

        /// <summary>
        /// Gets a value indicating whether [use proxy].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use proxy]; otherwise, <c>false</c>.
        /// </value>
        bool UseProxy { get; }
    }
}
