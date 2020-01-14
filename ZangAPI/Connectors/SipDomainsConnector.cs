using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using AvayaCPaaS.ConnectionManager;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model;
using AvayaCPaaS.Model.Enums;
using AvayaCPaaS.Model.Lists;

namespace AvayaCPaaS.Connectors
{
    /// <summary>
    /// Sip domains connector - used for all forms of communication with the Sip Domains endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Connectors.AConnector" />
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
        /// Get info on your SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <returns>Returns domain</returns>
        public Domain ViewDomain(string accountSid, string domainSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Domain>(response);
        }

        /// <summary>
        /// Get info on your SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <returns>Returns domain</returns>
        public Domain ViewDomain(string domainSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewDomain(accountSid, domainSid);
        }

        /// <summary>
        /// List all your SIP domains
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <returns>Returns domains list</returns>
        public DomainsList ListDomains(string accountSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/Domains.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<DomainsList>(response);
        }

        /// <summary>
        /// List all your SIP domains. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <returns>Returns domains list</returns>
        public DomainsList ListDomains()
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListDomains(accountSid);
        }

        /// <summary>
        /// Create new SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainName">An address on Avaya CPaaS uniquely associated with your account and through which all your SIP traffic is routed.</param>
        /// <param name="friendlyName">A human-readable name associated with this domain.</param>
        /// <param name="voiceUrl">The URL requested when a call is received by your domain.</param>
        /// <param name="voiceMethod">The HTTP method used when requesting the VoiceUrl.</param>
        /// <param name="voiceFallbackUrl">The URL requested if the VoiceUrl fails.</param>
        /// <param name="voiceFallbackMethod">The HTTP method used when requesting the VoiceFallbackUrl.</param>
        /// <param name="hearbeatUrl">URL that can be requested every 60 seconds during the call to notify of elapsed time and pass other general information.</param>
        /// <param name="hearbeatMethod">Specifies the HTTP method used to request HeartbeatUrl.</param>
        /// <param name="voiceStatusCallback">The URL that Avaya CPaaS will use to send you status notifications regarding your SIP call.</param>
        /// <param name="voiceStatusCallbackMethod">The HTTP method used when requesting the VoiceStatusCallback.</param>
        /// <returns>Returns created domain</returns>
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
            request.AddParameter("DomainName", domainName);

            this.SetParamsForCreateOrUpdateDomain(request, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl,
                voiceFallbackMethod, hearbeatUrl,
                hearbeatMethod, voiceStatusCallback, voiceStatusCallbackMethod);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Domain>(response);
        }

        /// <summary>
        /// Create new SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainName">An address on Avaya CPaaS uniquely associated with your account and through which all your SIP traffic is routed.</param>
        /// <param name="friendlyName">A human-readable name associated with this domain.</param>
        /// <param name="voiceUrl">The URL requested when a call is received by your domain.</param>
        /// <param name="voiceMethod">The HTTP method used when requesting the VoiceUrl.</param>
        /// <param name="voiceFallbackUrl">The URL requested if the VoiceUrl fails.</param>
        /// <param name="voiceFallbackMethod">The HTTP method used when requesting the VoiceFallbackUrl.</param>
        /// <param name="hearbeatUrl">URL that can be requested every 60 seconds during the call to notify of elapsed time and pass other general information.</param>
        /// <param name="hearbeatMethod">Specifies the HTTP method used to request HeartbeatUrl.</param>
        /// <param name="voiceStatusCallback">The URL that Avaya CPaaS will use to send you status notifications regarding your SIP call.</param>
        /// <param name="voiceStatusCallbackMethod">The HTTP method used when requesting the VoiceStatusCallback.</param>
        /// <returns>Returns created domain</returns>
        public Domain CreateDomain(string domainName, string friendlyName = null, string voiceUrl = null,
            HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST,
            string hearbeatUrl = null, HttpMethod hearbeatMethod = HttpMethod.POST,
            string voiceStatusCallback = null, HttpMethod voiceStatusCallbackMethod = HttpMethod.POST)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateDomain(accountSid, domainName, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl,
                voiceFallbackMethod, hearbeatUrl,
                hearbeatMethod, voiceStatusCallback, voiceStatusCallbackMethod);
        }

        /// <summary>
        /// Updates SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="friendlyName">A human-readable name associated with this domain.</param>
        /// <param name="voiceUrl">The URL requested when a call is received by your domain.</param>
        /// <param name="voiceMethod">The HTTP method used when requesting the VoiceUrl.</param>
        /// <param name="voiceFallbackUrl">The URL requested if the VoiceUrl fails.</param>
        /// <param name="voiceFallbackMethod">The HTTP method used when requesting the VoiceFallbackUrl.</param>
        /// <param name="hearbeatUrl">URL that can be requested every 60 seconds during the call to notify of elapsed time and pass other general information.</param>
        /// <param name="hearbeatMethod">Specifies the HTTP method used to request HeartbeatUrl.</param>
        /// <param name="voiceStatusCallback">The URL that Avaya CPaaS will use to send you status notifications regarding your SIP call.</param>
        /// <param name="voiceStatusCallbackMethod">The HTTP method used when requesting the VoiceStatusCallback.</param>
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
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}.json");

            // Add UpdateDomain query and body parameters
            this.SetParamsForCreateOrUpdateDomain(request, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl,
                voiceFallbackMethod, hearbeatUrl,
                hearbeatMethod, voiceStatusCallback, voiceStatusCallbackMethod);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Domain>(response);
        }

        /// <summary>
        /// Updates SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="friendlyName">A human-readable name associated with this domain.</param>
        /// <param name="voiceUrl">The URL requested when a call is received by your domain.</param>
        /// <param name="voiceMethod">The HTTP method used when requesting the VoiceUrl.</param>
        /// <param name="voiceFallbackUrl">The URL requested if the VoiceUrl fails.</param>
        /// <param name="voiceFallbackMethod">The HTTP method used when requesting the VoiceFallbackUrl.</param>
        /// <param name="hearbeatUrl">URL that can be requested every 60 seconds during the call to notify of elapsed time and pass other general information.</param>
        /// <param name="hearbeatMethod">Specifies the HTTP method used to request HeartbeatUrl.</param>
        /// <param name="voiceStatusCallback">The URL that Avaya CPaaS will use to send you status notifications regarding your SIP call.</param>
        /// <param name="voiceStatusCallbackMethod">The HTTP method used when requesting the VoiceStatusCallback.</param>
        /// <returns>Returns updated domain</returns>
        public Domain UpdateDomain(string domainSid, string friendlyName = null, string voiceUrl = null,
            HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST,
            string hearbeatUrl = null, HttpMethod hearbeatMethod = HttpMethod.POST,
            string voiceStatusCallback = null, HttpMethod voiceStatusCallbackMethod = HttpMethod.POST)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateDomain(accountSid, domainSid, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl,
                voiceFallbackMethod, hearbeatUrl,
                hearbeatMethod, voiceStatusCallback, voiceStatusCallbackMethod);
        }

        /// <summary>
        /// Deletes SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <returns>Returns deleted domain</returns>
        public Domain DeleteDomain(string accountSid, string domainSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Domain>(response);
        }

        /// <summary> 
        /// Deletes SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <returns>Returns deleted domain</returns>
        public Domain DeleteDomain(string domainSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteDomain(accountSid, domainSid);
        }

        /// <summary>
        /// Maps credentials list to a SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="credentialListSid">The SID of the credential list that you wish to associate with this domain.</param>
        /// <returns>Returns mapped credentials list</returns>
        public CredentialList MapCredentialsList(string accountSid, string domainSid, string credentialListSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings.json");

            // Mark obligatory parameters
            Require.Argument("CredentialListSid", credentialListSid);

            // Add MapCredentialsList query and body parameters
            request.AddParameter("CredentialListSid", credentialListSid);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialList>(response);
        }

        /// <summary>
        /// Maps credentials list to a SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="credentialListSid">The SID of the credential list that you wish to associate with this domain.</param>
        /// <returns>Returns mapped credentials list</returns>
        public CredentialList MapCredentialsList(string domainSid, string credentialListSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.MapCredentialsList(accountSid, domainSid, credentialListSid);
        }

        /// <summary>
        /// Shows info on credential lists attached to a SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <returns>Returns mapped credential lists list</returns>
        public CredentialListsList ListMappedCredentialsLists(string accountSid, string domainSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialListsList>(response);
        }

        /// <summary>
        /// Shows info on credential lists attached to a SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <returns>Returns mapped credential lists list</returns>
        public CredentialListsList ListMappedCredentialsLists(string domainSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListMappedCredentialsLists(accountSid, domainSid);
        }

        /// <summary>
        /// Deletes a credential list mapped to some SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <returns>Returns deleted credentials list</returns>
        public CredentialList DeleteMappedCredentialsList(string accountSid, string domainSid, string clSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings/{clSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CredentialList>(response);
        }

        /// <summary>
        /// Deletes a credential list mapped to some SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="clSid">Credentials list SID.</param>
        /// <returns>Returns deleted credentials list</returns>
        public CredentialList DeleteMappedCredentialsList(string domainSid, string clSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteMappedCredentialsList(accountSid, domainSid, clSid);
        }

        /// <summary>
        /// Maps IP access control list to a SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="ipAccessControlListSid">The Sid of the IP ACL that you wish to associate with this domain.</param>
        /// <returns>Returns mapped IP access control list</returns>
        public IpAccessControlList MapIpAccessControlList(string accountSid, string domainSid,
            string ipAccessControlListSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings.json");

            // Mark obligatory parameters
            Require.Argument("IpAccessControlListSid", ipAccessControlListSid);

            // Add MapIPAccessControlList query and body parameters
            request.AddParameter("IpAccessControlListSid", ipAccessControlListSid);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAccessControlList>(response);
        }

        /// <summary>
        /// Maps IP access control list to a SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="ipAccessControlListSid">The Sid of the IP ACL that you wish to associate with this domain.</param>
        /// <returns>Returns mapped IP access control list</returns>
        public IpAccessControlList MapIpAccessControlList(string domainSid, string ipAccessControlListSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.MapIpAccessControlList(accountSid, domainSid, ipAccessControlListSid);
        }

        /// <summary>
        /// Shows info on IP access control lists attached to a SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <returns>Returns mapped IP access control lists list</returns>
        public IpAccessControlListsList ListMappedIpAccessControlLists(string accountSid, string domainSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAccessControlListsList>(response);
        }

        /// <summary>
        /// Shows info on IP access control lists attached to a SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <returns>Returns mapped IP access control lists list</returns>
        public IpAccessControlListsList ListMappedIpAccessControlLists(string domainSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListMappedIpAccessControlLists(accountSid, domainSid);
        }

        /// <summary>
        /// Detaches an IP access control list from a SIP domain
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="alSid">IP access control list SID.</param>
        /// <returns>Returns deleted IP access control list</returns>
        public IpAccessControlList DeleteMappedIpAccessControlList(string accountSid, string domainSid, string alSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings/{alSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAccessControlList>(response);
        }

        /// <summary>
        /// Detaches an IP access control list from a SIP domain. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="domainSid">Domain SID.</param>
        /// <param name="alSid">IP access control list SID.</param>
        /// <returns>Returns deleted IP access control list</returns>
        public IpAccessControlList DeleteMappedIpAccessControlList(string domainSid, string alSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteMappedIpAccessControlList(accountSid, domainSid, alSid);
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
        private void SetParamsForCreateOrUpdateDomain(IRestRequest request, string friendlyName, string voiceUrl,
            HttpMethod voiceMethod, string voiceFallbackUrl, HttpMethod voiceFallbackMethod, string hearbeatUrl,
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