using RestSharp;
using RestSharp.Extensions;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Accounts connector - used for all forms of communication with the Accounts endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class AccountsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public AccountsConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// See all the information associated with an account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <returns>Returns account</returns>
        public Account ViewAccount(string accountSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Account>(response);
        }

        /// <summary>
        /// See all the information associated with an account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <returns>Returns account</returns>
        public Account ViewAccount()
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewAccount(accountSid);
        }

        /// <summary>
        /// Updates account information.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="friendlyName">The custom alias for your account.</param>
        /// <returns>Returns updated account</returns>
        public Account UpdateAccount(string accountSid, string friendlyName = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}.json");

            // Set body parameter
            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Account>(response);
        }

        /// <summary>
        /// Updates account information. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="friendlyName">The custom alias for your account.</param>
        /// <returns>Returns updated account</returns>
        public Account UpdateAccount(string friendlyName = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateAccount(accountSid, friendlyName);
        }
    }
}
