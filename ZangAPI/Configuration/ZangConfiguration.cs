using System.Net;
using System.Security;

namespace AvayaCPaaS.Configuration
{
    /// <summary>
    /// API configuration
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Configuration.IAPIConfiguration" />
    public class APIConfiguration : IAPIConfiguration
    {
        /// <summary>
        /// Gets the account sid.
        /// </summary>
        /// <value>
        /// The account sid.
        /// </value>
        public string AccountSid { get; set; }

        /// <summary>
        /// Gets or sets the secure authentication token.
        /// </summary>
        /// <value>
        /// The secure authentication token.
        /// </value>
        private SecureString SecureAuthToken { get; set; }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <value>
        /// The authentication token.
        /// </value>
        public string AuthToken
        {
            get { return new NetworkCredential(string.Empty, SecureAuthToken).Password; }
            set { SecureAuthToken = new NetworkCredential(string.Empty, value).SecurePassword; }
        }

        /// <summary>
        /// Gets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets the proxy host.
        /// </summary>
        /// <value>
        /// The proxy host.
        /// </value>
        public string ProxyHost { get; set; }

        /// <summary>
        /// Gets the proxy port.
        /// </summary>
        /// <value>
        /// The proxy port.
        /// </value>
        public string ProxyPort { get; set; }

        /// <summary>
        /// Gets a value indicating whether [use proxy].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use proxy]; otherwise, <c>false</c>.
        /// </value>
        public bool UseProxy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="APIConfiguration"/> class.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="authToken">The authentication token.</param>
        public APIConfiguration(string accountSid, string authToken)
        {
            this.AccountSid = accountSid;
            this.AuthToken = authToken;
            this.BaseUrl = "https://api.zang.io/v2";
            this.ProxyHost = "";
            this.ProxyPort = "";
            this.UseProxy = false;

            this.BaseUrl = "https://api.zangsandbox.com/v2";

            this.AccountSid = "AC0e03eb32757eefca279543ba966ff0ba"; // igor
            this.AuthToken = "3324538708654cacb6922393685ecc58";

            this.AccountSid = "AC42889084121be293a4d64dba8fda3337"; // sv@esna
            this.AuthToken = "9fb6e6effb7a452086fd7b0def54dd0f";

            this.AccountSid = "AC0e03eb326bda53e9c03d4d71a5fff8a4"; // sv@ava
            this.AuthToken = "3fd9db72a34d45c1bd70144e806f9334";



        }
    }
}