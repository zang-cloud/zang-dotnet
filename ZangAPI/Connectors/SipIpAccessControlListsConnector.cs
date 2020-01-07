﻿using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Sip IP access control lists connector - used for all forms of communication with the Sip IP Access Control Lists endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class SipIpAccessControlListsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SipIpAccessControlListsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public SipIpAccessControlListsConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        /// <summary>
        /// View information for IP access control list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="ipAccessControlListSid">IP access control list SID.</param>
        /// <returns>Returns IP access control list</returns>
        public IpAccessControlList ViewIpAccessControlList(string accountSid, string ipAccessControlListSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAccessControlList>(response);
        }

        /// <summary>
        /// View information for IP access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="ipAccessControlListSid">IP access control list SID.</param>
        /// <returns>Returns IP access control list</returns>
        public IpAccessControlList ViewIpAccessControlList(string ipAccessControlListSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewIpAccessControlList(accountSid, ipAccessControlListSid);
        }

        /// <summary>
        /// List all IP access control lists associated with a particular account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns IP access control lists list</returns>
        public IpAccessControlListsList ListIpAccessControlLists(string accountSid, int? page = null,
            int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists.json");

            // Add ListApplications query and body parameters
            this.SetParamsForListIpAccessControlLists(request, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAccessControlListsList>(response);
        }

        /// <summary>
        /// List all IP access control lists associated with a particular account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns IP access control lists list</returns>
        public IpAccessControlListsList ListIpAccessControlLists(int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListIpAccessControlLists(accountSid, page, pageSize);
        }

        /// <summary>
        /// Create IP access control list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="friendlyName">A human-readable name associated with this IP ACL.</param>
        /// <returns>Returns created IP access control list</returns>
        public IpAccessControlList CreateIpAccessControlList(string accountSid, string friendlyName)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists.json");

            // Mark obligatory parameters
            Require.Argument("FriendlyName", friendlyName);

            // Add CreateIpAccessControlList query and body parameters
            request.AddParameter("FriendlyName", friendlyName);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAccessControlList>(response);
        }

        /// <summary>
        /// Create IP access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="friendlyName">A human-readable name associated with this IP ACL.</param>
        /// <returns>Returns created IP access control list</returns>
        public IpAccessControlList CreateIpAccessControlList(string friendlyName)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateIpAccessControlList(accountSid, friendlyName);
        }

        /// <summary>
        /// Updates information for IP access control list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="ipAccessControlListSid">IP access control list SID.</param>
        /// <param name="friendlyName">A human-readable name associated with this domain.</param>
        /// <returns>Returns updated IP access control list</returns>
        public IpAccessControlList UpdateIpAccessControlList(string accountSid, string ipAccessControlListSid,
            string friendlyName)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json");

            // Mark obligatory parameters
            Require.Argument("FriendlyName", friendlyName);

            // Add UpdateIpAccessControlList query and body parameters
            request.AddParameter("FriendlyName", friendlyName);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAccessControlList>(response);
        }

        /// <summary>
        /// Updates information for IP access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="ipAccessControlListSid">IP access control list SID.</param>
        /// <param name="friendlyName">A human-readable name associated with this domain.</param>
        /// <returns>Returns updated IP access control list</returns>
        public IpAccessControlList UpdateIpAccessControlList(string ipAccessControlListSid, string friendlyName)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateIpAccessControlList(accountSid, ipAccessControlListSid, friendlyName);
        }

        /// <summary>
        /// Deletes IP access control list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="ipAccessControlListSid">IP access control list SID.</param>
        /// <returns>Returns deleted IP access control list</returns>
        public IpAccessControlList DeleteIpAccessControlList(string accountSid, string ipAccessControlListSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAccessControlList>(response);
        }

        /// <summary>
        /// Deletes IP access control list
        /// </summary>
        /// <param name="ipAccessControlListSid">IP access control list SID.</param>
        /// <returns>Returns deleted IP  access control list</returns>
        public IpAccessControlList DeleteIpAccessControlList(string ipAccessControlListSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteIpAccessControlList(accountSid, ipAccessControlListSid);
        }

        /// <summary>
        /// View information on access control list IP address
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <param name="ipSid">Access control list IP address SID.</param>
        /// <returns>Returns access control list IP address</returns>
        public IpAddress ViewAccessControlListIp(string accountSid, string aclSid, string ipSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddress>(response);
        }

        /// <summary>
        /// View information on access control list IP address. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <param name="ipSid">Access control list IP address SID.</param>
        /// <returns>Returns access control list IP address</returns>
        public IpAddress ViewAccessControlListIp(string aclSid, string ipSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewAccessControlListIp(accountSid, aclSid, ipSid);
        }

        /// <summary>
        /// Lists IP addresses attached to some IP access control list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <returns>Returns list of access control list IPs</returns>
        public IpAddressesList ListAccessControlListIps(string accountSid, string aclSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddressesList>(response);
        }

        /// <summary>
        /// Lists IP addresses attached to some IP access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <returns>Returns list of access control list IPs</returns>
        public IpAddressesList ListAccessControlListIps(string aclSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListAccessControlListIps(accountSid, aclSid);
        }

        /// <summary>
        /// Add new IP for access control list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <param name="friendlyName">A human-readable name associated with this IP ACL.</param>
        /// <param name="ipAddress">An IP address from which you wish to accept traffic. At this time, only IPv4 supported.</param>
        /// <returns>Returns added access control list IP address</returns>
        public IpAddress AddAccessControlListIp(string accountSid, string aclSid, string friendlyName, string ipAddress)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses.json");

            // Mark obligatory parameters
            Require.Argument("FriendlyName", friendlyName);
            Require.Argument("IpAddress", ipAddress);

            // Add AddAccessControlListIp query and body parameters
            this.SetParamsForAddOrUpdateAccessControlListIp(request, friendlyName, ipAddress);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddress>(response);
        }

        /// <summary>
        /// Add new IP for access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <param name="friendlyName">A human-readable name associated with this IP ACL.</param>
        /// <param name="ipAddress">An IP address from which you wish to accept traffic. At this time, only IPv4 supported.</param>
        /// <returns>Returns added access control list IP address</returns>
        public IpAddress AddAccessControlListIp(string aclSid, string friendlyName, string ipAddress)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.AddAccessControlListIp(accountSid, aclSid, friendlyName, ipAddress);
        }

        /// <summary>
        /// Updates IP address for IP access control list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <param name="ipSid">Access control list IP address SID.</param>
        /// <param name="friendlyName">A human-readable name associated with this IP ACL.</param>
        /// <param name="ipAddress">An IP address from which you wish to accept traffic. At this time, only IPv4 supported.</param>
        /// <returns>Returns updated access control list IP address</returns>
        public IpAddress UpdateAccessControlListIp(string accountSid, string aclSid, string ipSid,
            string friendlyName = null, string ipAddress = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json");

            // Add UpdateAccessControlListIp query and body parameters
            this.SetParamsForAddOrUpdateAccessControlListIp(request, friendlyName, ipAddress);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddress>(response);
        }

        /// <summary>
        /// Updates IP address for IP access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <param name="ipSid">Access control list IP address SID.</param>
        /// <param name="friendlyName">A human-readable name associated with this IP ACL.</param>
        /// <param name="ipAddress">An IP address from which you wish to accept traffic. At this time, only IPv4 supported.</param>
        /// <returns>Returns updated access control list IP address</returns>
        public IpAddress UpdateAccessControlListIp(string aclSid, string ipSid, string friendlyName = null,
            string ipAddress = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateAccessControlListIp(accountSid, aclSid, ipSid, friendlyName, ipAddress);
        }

        /// <summary>
        /// Deletes IP address from IP access control list
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <param name="ipSid">Access control list IP address SID.</param>
        /// <returns>Returns deleted access control list IP address</returns>
        public IpAddress DeleteAccessControlListIp(string accountSid, string aclSid, string ipSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddress>(response);
        }

        /// <summary>
        /// Deletes IP address from IP access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">IP access control list SID.</param>
        /// <param name="ipSid">Access control list IP address SID.</param>
        /// <returns>Returns deleted access control list IP address</returns>
        public IpAddress DeleteAccessControlListIp(string aclSid, string ipSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteAccessControlListIp(accountSid, aclSid, ipSid);
        }

        /// <summary>
        /// Sets the parameters for list ip access control lists.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListIpAccessControlLists(IRestRequest request, int? page, int? pageSize)
        {
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }

        /// <summary>
        /// Sets the parameters for add acl ip.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="ipAddress">The ip address.</param>
        private void SetParamsForAddOrUpdateAccessControlListIp(IRestRequest request, string friendlyName,
            string ipAddress)
        {
            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
            if (ipAddress.HasValue()) request.AddParameter("IpAddress", ipAddress);
        }
    }
}