using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using AvayaCPaaS.ConnectionManager;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model;
using AvayaCPaaS.Model.Lists;

namespace AvayaCPaaS.Connectors
{
    /// <summary>
    /// Sip credentials connector - used for all forms of communication with the Sip Credentials endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Connectors.AConnector" />
    public class SipCredentialsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SipCredentialsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public SipCredentialsConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        /// <summary>
        /// View SIP domain credentials information
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="credentialSid">Credential SID.</param>
        /// <returns>Returns credential</returns>
        public Credential ViewCredential(string accountSid, string clSid, string credentialSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials/{credentialSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Credential>(response);
        }

        /// <summary>
        /// View SIP domain credentials information. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="credentialSid">Credential SID.</param>
        /// <returns>Returns credential</returns>
        public Credential ViewCredential(string clSid, string credentialSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewCredential(accountSid, clSid, credentialSid);
        }

        /// <summary>
        /// Show info on all credentials attached to a particular credentials list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <returns>Returns credentials list</returns>
        public CredentialsList ListCredentials(string accountSid, string clSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialsList>(response);
        }

        /// <summary>
        /// Show info on all credentials attached to a particular credentials list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="clSid">Credentials list SID.</param>
        /// <returns>Returns credentials list</returns>
        public CredentialsList ListCredentials(string clSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListCredentials(accountSid, clSid);
        }

        /// <summary>
        /// Create SIP domain credentials
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="username">The username used to identify this credential.</param>
        /// <param name="password">The password used to authenticate this credential.</param>
        /// <returns>Returns created credential</returns>
        public Credential CreateCredential(string accountSid, string clSid, string username, string password)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials.json");

            // Mark obligatory parameters
            Require.Argument("Username", username);
            Require.Argument("Password", password);

            // Add CreateCredential query and body parameters
            this.SetParamsForCreateCredential(request, username, password);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Credential>(response);
        }

        /// <summary>
        /// Create SIP domain credentials. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="username">The username used to identify this credential.</param>
        /// <param name="password">The password used to authenticate this credential.</param>
        /// <returns>Returns created credential</returns>
        public Credential CreateCredential(string clSid, string username, string password)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateCredential(accountSid, clSid, username, password);
        }

        /// <summary>
        /// Updates SIP domain credentials
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="credentialSid">Credential SID.</param>
        /// <param name="password">The password used to authenticate this credential.</param>
        /// <returns>Returns updated credential</returns>
        public Credential UpdateCredential(string accountSid, string clSid, string credentialSid, string password)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials/{credentialSid}.json");

            // Mark obligatory parameters
            Require.Argument("Password", password);

            // Add UpdateCredential query and body parameters
            request.AddParameter("Password", password);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Credential>(response);
        }

        /// <summary>
        /// Updates SIP domain credentials. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="credentialSid">Credential SID..</param>
        /// <param name="password">The password used to authenticate this credential.</param>
        /// <returns>Returns updated credential</returns>
        public Credential UpdateCredential(string clSid, string credentialSid, string password)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateCredential(accountSid, clSid, credentialSid, password);
        }

        /// <summary>
        /// Deletes SIP domain credentials
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="credentialSid">Credential SID.</param>
        /// <returns>Returns deleted credential</returns>
        public Credential DeleteCredential(string accountSid, string clSid, string credentialSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials/{credentialSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Credential>(response);
        }

        /// <summary>
        /// Deletes SIP domain credentials. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="credentialSid">Credential SID.</param>
        /// <returns>Returns deleted credential</returns>
        public Credential DeleteCredential(string clSid, string credentialSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteCredential(accountSid, clSid, credentialSid);
        }

        /// <summary>
        /// View info on SIP domain credentials list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <returns>Returns credentials list</returns>
        public CredentialList ViewCredentialsList(string accountSid, string clSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/CredentialLists/{clSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialList>(response);
        }

        /// <summary>
        /// View info on SIP domain credentials list
        /// </summary>
        /// <param name="clSid">Credentials list SID.</param>
        /// <returns></returns>
        public CredentialList ViewCredentialsList(string clSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewCredentialsList(accountSid, clSid);
        }

        /// <summary>
        /// Show info on SIP domain credentials lists
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <returns>Returns credentials lists list</returns>
        public CredentialListsList ListCredentialsLists(string accountSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/CredentialLists.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialListsList>(response);
        }

        /// <summary>
        /// Show info on SIP domain credentials lists. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <returns>Returns credentials lists list</returns>
        public CredentialListsList ListCredentialsLists()
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListCredentialsLists(accountSid);
        }

        /// <summary>
        /// Creates SIP domain credentials list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="friendlyName">A human readable name for this credential list.</param>
        /// <returns>Returns created credentials list</returns>
        public CredentialList CreateCredentialsList(string accountSid, string friendlyName)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/CredentialLists.json");

            // Mark obligatory parameters
            Require.Argument("FriendlyName", friendlyName);

            // Add CreateCredential query and body parameters
            request.AddParameter("FriendlyName", friendlyName);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialList>(response);
        }

        /// <summary>
        /// Creates SIP domain credentials list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="friendlyName">A human readable name for this credential list.</param>
        /// <returns>Returns created credentials list</returns>
        public CredentialList CreateCredentialsList(string friendlyName)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateCredentialsList(accountSid, friendlyName);
        }

        /// <summary>
        /// Updates info for credentials list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="friendlyName">A human readable name for this credential list.</param>
        /// <returns>Returns updated credentials list</returns>
        public CredentialList UpdateCredentialsList(string accountSid, string clSid, string friendlyName = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/CredentialLists/{clSid}.json");

            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialList>(response);
        }

        /// <summary>
        /// Updates info for credentials list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="clSid">Credentials list SID.</param>
        /// <param name="friendlyName">A human readable name for this credential list.</param>
        /// <returns>Returns updated credentials list</returns>
        public CredentialList UpdateCredentialsList(string clSid, string friendlyName = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateCredentialsList(accountSid, clSid, friendlyName);
        }

        /// <summary>
        /// Deletes SIP domain credentials list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <returns>Returns deleted credentials list</returns>
        public CredentialList DeleteCredentialsList(string accountSid, string clSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/SIP/CredentialLists/{clSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialList>(response);
        }

        /// <summary>
        /// Deletes SIP domain credentials list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="clSid">Credentials list SID.</param>
        /// <returns>Returns deleted credentials list</returns>
        public CredentialList DeleteCredentialsList(string clSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteCredentialsList(accountSid, clSid);
        }

        /// <summary>
        /// Sets the parameters for create credential.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        private void SetParamsForCreateCredential(IRestRequest request, string username, string password)
        {
            request.AddParameter("Username", username);
            request.AddParameter("Password", password);
        }
    }
}
