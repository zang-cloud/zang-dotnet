using RestSharp;
using RestSharp.Validation;
using AvayaCPaaS.ConnectionManager;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model;
using AvayaCPaaS.Model.Lists;

namespace AvayaCPaaS.Connectors
{
    /// <summary>
    /// Application clients connector - used for all forms of communication with the Application Clients endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Connectors.AConnector" />
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
        /// View all information about an application client
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">Application SID.</param>
        /// <param name="clientSid">Application Client SID.</param>
        /// <returns>Returns application client</returns>
        public ApplicationClient ViewApplicationClient(string accountSid, string applicationSid, string clientSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Applications/{applicationSid}/Clients/{clientSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ApplicationClient>(response);
        }

        /// <summary>
        /// View all information about an application client. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">Application SID.</param>
        /// <param name="clientSid">Application Client SID.</param>
        /// <returns>Returns application client</returns>
        public ApplicationClient ViewApplicationClient(string applicationSid, string clientSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewApplicationClient(accountSid, applicationSid, clientSid);
        }

        /// <summary>
        /// Lists available application clients
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">Application SID.</param>
        /// <returns>Returns application client list</returns>
        public ApplicationClientsList ListApplicationClients(string accountSid, string applicationSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Applications/{applicationSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ApplicationClientsList>(response);
        }

        /// <summary>
        /// Lists available application clients. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">Application SID.</param>
        /// <returns>Returns application client list</returns>
        public ApplicationClientsList ListApplicationClients(string applicationSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListApplicationClients(accountSid, applicationSid);
        }

        /// <summary>
        /// Creates a new application client for your application
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">Application SID.</param>
        /// <param name="nickname">The name used to identify this application client.</param>
        /// <returns>Returns created application client</returns>
        public ApplicationClient CreateApplicationClient(string accountSid, string applicationSid, string nickname)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Applications/{applicationSid}/Tokens.json");

            // Mark obligatory parameters
            Require.Argument("Nickname", nickname);

            // Add CreateApplicationClient parameter
            request.AddParameter("Nickname", nickname);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ApplicationClient>(response);
        }

        /// <summary>
        /// Creates a new application client for your application. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">Application SID.</param>
        /// <param name="nickname">The name used to identify this application client.</param>
        /// <returns>Returns created application client</returns>
        public ApplicationClient CreateApplicationClient(string applicationSid, string nickname)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateApplicationClient(accountSid, applicationSid, nickname);
        }
    }
}