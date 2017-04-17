using RestSharp;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Application clients connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class ApplicationClientsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationClientsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public ApplicationClientsConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Views the application client.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">The application sid.</param>
        /// <param name="clientSid">The client sid.</param>
        /// <returns>Returns application client</returns>
        public ApplicationClient ViewApplicationClient(string accountSid, string applicationSid, string clientSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Applications/{applicationSid}/Clients/{clientSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ApplicationClient>(response);
        }

        /// <summary>
        /// Views the application client. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">The application sid.</param>
        /// <param name="clientSid">The client sid.</param>
        /// <returns>Returns application client</returns>
        public ApplicationClient ViewApplicationClient(string applicationSid, string clientSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewApplicationClient(accountSid, applicationSid, clientSid);
        }

        /// <summary>
        /// Lists the application clients.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">The application sid.</param>
        /// <returns>Returns application client list</returns>
        public ApplicationClientsList ListApplicationClients(string accountSid, string applicationSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Applications/{applicationSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ApplicationClientsList>(response);
        }

        /// <summary>
        /// Lists the application clients. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">The application sid.</param>
        /// <returns>Returns application client list</returns>
        public ApplicationClientsList ListApplicationClients(string applicationSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListApplicationClients(accountSid, applicationSid);
        }

        /// <summary>
        /// Creates the application client.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">The application sid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <returns>Returns created application client</returns>
        public ApplicationClient CreateApplicationClient(string accountSid, string applicationSid, string nickname)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Applications/{applicationSid}/Tokens.json");

            // Mark obligatory parameters
            Require.Argument("Nickname", nickname);

            // Add CreateApplicationClient parameter
            request.AddParameter("Nickname", nickname);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ApplicationClient>(response);
        }

        /// <summary>
        /// Creates the application client. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">The application sid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <returns>Returns created application client</returns>
        public ApplicationClient CreateApplicationClient(string applicationSid, string nickname)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateApplicationClient(accountSid, applicationSid, nickname);
        }

        
    }
}
