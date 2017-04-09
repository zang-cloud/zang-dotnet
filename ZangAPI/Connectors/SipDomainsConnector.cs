using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Sip domains connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class SipDomainsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SipDomainsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public SipDomainsConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Views the domain.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <returns>Returns domain</returns>
        public Domain ViewDomain(string accountSid, string domainSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/Domains/{domainSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Domain>(response);
        }

        /// <summary>
        /// Views the domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <returns>Returns domain</returns>
        public Domain ViewDomain(string domainSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewDomain(accountSid, domainSid);
        }

        /// <summary>
        /// Lists the domains.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <returns>Returns domains list</returns>
        public DomainList ListDomains(string accountSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/Domains.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<DomainList>(response);
        }

        /// <summary>
        /// Lists the domains. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <returns>Returns domains list</returns>
        public DomainList ListDomains()
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListDomains(accountSid);
        }

        /// <summary>
        /// Creates the domain.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="voiceStatusCallback">The voice status callback.</param>
        /// <param name="voiceStatusCallbackMethod">The voice status callback method.</param>
        /// <returns>Returns domain</returns>
        public Domain CreateDomain(string accountSid, string domainName,
            string friendlyName = null, string voiceUrl = null, HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST, 
            string hearbeatUrl = null, HttpMethod hearbeatMethod = HttpMethod.POST, 
            string voiceStatusCallback = null, HttpMethod voiceStatusCallbackMethod = HttpMethod.POST)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SIP/Domains.json");

            // Mark obligatory parameters
            Require.Argument("DomainName", domainName);

            // Add CreateDomain query and body parameters
            this.SetParamsForCreateOrUpdateDomain(request, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl, voiceFallbackMethod, hearbeatUrl,
                hearbeatMethod, voiceStatusCallback, voiceStatusCallbackMethod);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Domain>(response);
        }

        /// <summary>
        /// Creates the domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="voiceStatusCallback">The voice status callback.</param>
        /// <param name="voiceStatusCallbackMethod">The voice status callback method.</param>
        /// <returns>Returns domain</returns>
        public Domain CreateDomain(string domainName, string friendlyName = null, string voiceUrl = null, HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST,
            string hearbeatUrl = null, HttpMethod hearbeatMethod = HttpMethod.POST,
            string voiceStatusCallback = null, HttpMethod voiceStatusCallbackMethod = HttpMethod.POST)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateDomain(accountSid, domainName, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl, voiceFallbackMethod, hearbeatUrl,
                hearbeatMethod, voiceStatusCallback, voiceStatusCallbackMethod);
        }

        /// <summary>
        /// Updates the domain.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="voiceStatusCallback">The voice status callback.</param>
        /// <param name="voiceStatusCallbackMethod">The voice status callback method.</param>
        /// <returns>Returns updated domain</returns>
        public Domain UpdateDomain(string accountSid, string domainSid,
            string friendlyName = null, string voiceUrl = null, HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST,
            string hearbeatUrl = null, HttpMethod hearbeatMethod = HttpMethod.POST,
            string voiceStatusCallback = null, HttpMethod voiceStatusCallbackMethod = HttpMethod.POST)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SIP/Domains/{domainSid}.json");

            // Add UpdateDomain query and body parameters
            this.SetParamsForCreateOrUpdateDomain(request, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl, voiceFallbackMethod, hearbeatUrl,
                hearbeatMethod, voiceStatusCallback, voiceStatusCallbackMethod);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Domain>(response);
        }

        /// <summary>
        /// Updates the domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="voiceStatusCallback">The voice status callback.</param>
        /// <param name="voiceStatusCallbackMethod">The voice status callback method.</param>
        /// <returns>Returns updated domain</returns>
        public Domain UpdateDomain(string domainSid, string friendlyName = null, string voiceUrl = null, HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST,
            string hearbeatUrl = null, HttpMethod hearbeatMethod = HttpMethod.POST,
            string voiceStatusCallback = null, HttpMethod voiceStatusCallbackMethod = HttpMethod.POST)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateDomain(accountSid, domainSid, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl, voiceFallbackMethod, hearbeatUrl,
                hearbeatMethod, voiceStatusCallback, voiceStatusCallbackMethod);
        }

        /// <summary>
        /// Deletes the domain.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <returns>Returns deleted domain</returns>
        public Domain DeleteDomain(string accountSid, string domainSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE, $"Accounts/{accountSid}/SIP/Domains/{domainSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Domain>(response);
        }

        /// <summary> 
        /// Deletes the domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <returns>Returns deleted domain</returns>
        public Domain DeleteDomain(string domainSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteDomain(accountSid, domainSid);
        }

        /// <summary>
        /// Maps the credentials list.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="credentialListSid">The credential list sid.</param>
        /// <returns>Returns credentials list</returns>
        public CredentialsList MapCredentialsList(string accountSid, string domainSid, string credentialListSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings.json");

            // Mark obligatory parameters
            Require.Argument("CredentialListSid", credentialListSid);

            // Add MapCredentialsList query and body parameters
            request.AddParameter("CredentialListSid", credentialListSid);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialsList>(response);
        }

        /// <summary>
        /// Maps the credentials list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="credentialListSid">The credential list sid.</param>
        /// <returns>Returns credentials list</returns>
        public CredentialsList MapCredentialsList(string domainSid, string credentialListSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.MapCredentialsList(accountSid, domainSid, credentialListSid);
        }

        /// <summary>
        /// Lists the credential lists.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <returns>Returns credential lists list</returns>
        public CredentialsListsList ListCredentialLists(string accountSid, string domainSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialsListsList>(response);
        }

        /// <summary>
        /// Lists the credential lists. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <returns>Returns credential lists list</returns>
        public CredentialsListsList ListCredentialLists(string domainSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListCredentialLists(accountSid, domainSid);
        }

        /// <summary>
        /// Deletes the credentials list.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="clSid">The cl sid.</param>
        /// <returns>Returns deleted credentials list</returns>
        public CredentialsList DeleteCredentialsList(string accountSid, string domainSid, string clSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE, $"Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings/{clSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialsList>(response);
        }

        /// <summary>
        /// Deletes the credentials list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="clSid">The cl sid.</param>
        /// <returns>Returns deleted credentials list</returns>
        public CredentialsList DeleteCredentialsList(string domainSid, string clSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteCredentialsList(accountSid, domainSid, clSid);
        }

        /// <summary>
        /// Maps the ip access control list.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="ipAccessControlListSid">The ip access control list sid.</param>
        /// <returns>Returns IP access control list</returns>
        public IPAccessControlList MapIPAccessControlList(string accountSid, string domainSid, string ipAccessControlListSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings.json");

            // Mark obligatory parameters
            Require.Argument("IpAccessControlListSid", ipAccessControlListSid);

            // Add MapIPAccessControlList query and body parameters
            request.AddParameter("IpAccessControlListSid", ipAccessControlListSid);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IPAccessControlList>(response);
        }

        /// <summary>
        /// Maps the ip access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="ipAccessControlListSid">The ip access control list sid.</param>
        /// <returns>Returns IP access control list</returns>
        public IPAccessControlList MapIPAccessControlList(string domainSid, string ipAccessControlListSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.MapIPAccessControlList(accountSid, domainSid, ipAccessControlListSid);
        }

        /// <summary>
        /// Lists the ip access control lists.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <returns>Returns IP access control lists list</returns>
        public IPAccessControlListsList ListIPAccessControlLists(string accountSid, string domainSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IPAccessControlListsList>(response);
        }

        /// <summary>
        /// Lists the ip access control lists. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <returns>Returns IP access control lists list</returns>
        public IPAccessControlListsList ListIPAccessControlLists(string domainSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListIPAccessControlLists(accountSid, domainSid);
        }

        /// <summary>
        /// Deletes the ip access control list.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="alSid">The al sid.</param>
        /// <returns>Returns deleted IP access control list</returns>
        public IPAccessControlList DeleteIPAccessControlList(string accountSid, string domainSid, string alSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE, $"Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings/{alSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IPAccessControlList>(response);
        }

        /// <summary>
        /// Deletes the ip access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">The domain sid.</param>
        /// <param name="alSid">The al sid.</param>
        /// <returns>Returns deleted IP access control list</returns>
        public IPAccessControlList DeleteIPAccessControlList(string domainSid, string alSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteIPAccessControlList(accountSid, domainSid, alSid);
        }

        /// <summary>
        /// Sets the parameters for create or update domain.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="voiceStatusCallback">The voice status callback.</param>
        /// <param name="voiceStatusCallbackMethod">The voice status callback method.</param>
        private void SetParamsForCreateOrUpdateDomain(IRestRequest request, string friendlyName, string voiceUrl, HttpMethod voiceMethod,
            string voiceFallbackUrl, HttpMethod voiceFallbackMethod, string hearbeatUrl,
            HttpMethod hearbeatMethod, string voiceStatusCallback, HttpMethod voiceStatusCallbackMethod)
        {
            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
            if (voiceUrl.HasValue()) request.AddParameter("VoiceUrl", voiceUrl);
            request.AddParameter("VoiceMethod", voiceMethod.ToString().ToUpper());
            if (voiceFallbackUrl.HasValue()) request.AddParameter("VoiceFallbackUrl", voiceFallbackUrl);
            request.AddParameter("VoiceFallbackMethod", voiceFallbackMethod.ToString().ToUpper());
            if (hearbeatUrl.HasValue()) request.AddParameter("HeartbeatUrl", hearbeatUrl);
            request.AddParameter("HeartbeatMethod", hearbeatMethod.ToString().ToUpper());
            if (voiceStatusCallback.HasValue()) request.AddParameter("VoiceStatusCallback", voiceStatusCallback);
            request.AddParameter("VoiceStatusCallbackMethod", voiceStatusCallbackMethod.ToString().ToUpper());
        }
    }
}
