using RestSharp;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Sip IP access control lists connector
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
        /// Views the ip access control list.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="ipAccessControlListSid">The ip access control list sid.</param>
        /// <returns>Returns IP access control list</returns>
        public IPAccessControlList ViewIPAccessControlList(string accountSid, string ipAccessControlListSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IPAccessControlList>(response);
        }

        /// <summary>
        /// Views the ip access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="ipAccessControlListSid">The ip access control list sid.</param>
        /// <returns>Returns IP access control list</returns>
        public IPAccessControlList ViewIPAccessControlList(string ipAccessControlListSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewIPAccessControlList(accountSid, ipAccessControlListSid);
        }

        /// <summary>
        /// Lists the ip access control lists.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns IP access control lists list</returns>
        public IPAccessControlListsList ListIPAccessControlLists(string accountSid, int? page = null,
            int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/IpAccessControlLists.json");

            // Add ListApplications query and body parameters
            this.SetParamsForListIPAccessControlLists(request, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IPAccessControlListsList>(response);
        }

        /// <summary>
        /// Lists the ip access control lists. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns IP access control lists list</returns>
        public IPAccessControlListsList ListIPAccessControlLists(int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListIPAccessControlLists(accountSid, page, pageSize);
        }

        /// <summary>
        /// Creates the ip access control list.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>Returns created IP access control list</returns>
        public IPAccessControlList CreateIPAccessControlList(string accountSid, string friendlyName)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SIP/IpAccessControlLists.json");

            // Mark obligatory parameters
            Require.Argument("FriendlyName", friendlyName);

            // Add CreateIPAccessControlList query and body parameters
            request.AddParameter("FriendlyName", friendlyName);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IPAccessControlList>(response);
        }

        /// <summary>
        /// Creates the ip access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>Returns created IP access control list</returns>
        public IPAccessControlList CreateIPAccessControlList(string friendlyName)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateIPAccessControlList(accountSid, friendlyName);
        }

        /// <summary>
        /// Updates the ip access control list.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="ipAccessControlListSid">The ip access control list sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>Returns updated IP access control list</returns>
        public IPAccessControlList UpdateIPAccessControlList(string accountSid, string ipAccessControlListSid, string friendlyName)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json");

            // Mark obligatory parameters
            Require.Argument("FriendlyName", friendlyName);

            // Add UpdateIPAccessControlList query and body parameters
            request.AddParameter("FriendlyName", friendlyName);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IPAccessControlList>(response);
        }

        /// <summary>
        /// Updates the ip access control list. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="ipAccessControlListSid">The ip access control list sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>Returns updated IP access control list</returns>
        public IPAccessControlList UpdateIPAccessControlList(string ipAccessControlListSid, string friendlyName)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateIPAccessControlList(accountSid, ipAccessControlListSid, friendlyName);
        }

        /// <summary>
        /// Deletes the ip access control list.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="ipAccessControlListSid">The ip access control list sid.</param>
        /// <returns>Returns deleted IP access control list</returns>
        public IPAccessControlList DeleteIPAccessControlList(string accountSid, string ipAccessControlListSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE, $"Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IPAccessControlList>(response);
        }

        /// <summary>
        /// Deletes the ip access control list.
        /// </summary>
        /// <param name="ipAccessControlListSid">The ip access control list sid.</param>
        /// <returns>Returns deleted IP  access control list</returns>
        public IPAccessControlList DeleteIPAccessControlList(string ipAccessControlListSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteIPAccessControlList(accountSid, ipAccessControlListSid);
        }

        /// <summary>
        /// Views the acl ip.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">The acl sid.</param>
        /// <param name="ipSid">The ip sid.</param>
        /// <returns>Returns IP address</returns>
        public IpAddress ViewAclIp(string accountSid, string aclSid, string ipSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddress>(response);
        }

        /// <summary>
        /// Views the acl ip. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">The acl sid.</param>
        /// <param name="ipSid">The ip sid.</param>
        /// <returns>Returns IP address</returns>
        public IpAddress ViewAclIp(string aclSid, string ipSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewAclIp(accountSid, aclSid, ipSid);
        }

        /// <summary>
        /// Lists the acl ips.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">The acl sid.</param>
        /// <returns>Returns list of access control list IPs</returns>
        public IpAddressList ListAclIps(string accountSid, string aclSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddressList>(response);
        }

        /// <summary>
        /// Lists the acl ips. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">The acl sid.</param>
        /// <returns>Returns list of access control list IPs</returns>
        public IpAddressList ListAclIps(string aclSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListAclIps(accountSid, aclSid);
        }

        /// <summary>
        /// Adds the acl ip.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">The acl sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>Returns access control list IP</returns>
        public IpAddress AddAclIp(string accountSid, string aclSid, string friendlyName, string ipAddress)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses.json");

            // Mark obligatory parameters
            Require.Argument("FriendlyName", friendlyName);
            Require.Argument("IpAddress", ipAddress);

            // Add AddAclIp query and body parameters
            request.AddParameter("FriendlyName", friendlyName);
            request.AddParameter("IpAddress", ipAddress);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddress>(response);
        }

        /// <summary>
        /// Adds the acl ip. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">The acl sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>Returns access control list IP</returns>
        public IpAddress AddAclIp(string aclSid, string friendlyName, string ipAddress)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.AddAclIp(accountSid, aclSid, friendlyName, ipAddress);
        }

        /// <summary>
        /// Updates the acl ip.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">The acl sid.</param>
        /// <param name="ipSid">The ip sid.</param>
        /// <returns>Returns updated access control list IP</returns>
        public IpAddress UpdateAclIp(string accountSid, string aclSid, string ipSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddress>(response);
        }

        /// <summary>
        /// Updates the acl ip. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">The acl sid.</param>
        /// <param name="ipSid">The ip sid.</param>
        /// <returns>Returns updated access control list IP</returns>
        public IpAddress UpdateAclIp(string aclSid, string ipSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateAclIp(accountSid, aclSid, ipSid);
        }

        /// <summary>
        /// Deletes the acl ip.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="aclSid">The acl sid.</param>
        /// <param name="ipSid">The ip sid.</param>
        /// <returns>Returns deleted access control list IP</returns>
        public IpAddress DeleteAclIp(string accountSid, string aclSid, string ipSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE, $"Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IpAddress>(response);
        }

        /// <summary>
        /// Deletes the acl ip. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="aclSid">The acl sid.</param>
        /// <param name="ipSid">The ip sid.</param>
        /// <returns>Returns deleted access control list IP</returns>
        public IpAddress DeleteAclIp(string aclSid, string ipSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteAclIp(accountSid, aclSid, ipSid);
        }

        /// <summary>
        /// Sets the parameters for list ip access control lists.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListIPAccessControlLists(IRestRequest request, int? page, int? pageSize)
        {
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}
