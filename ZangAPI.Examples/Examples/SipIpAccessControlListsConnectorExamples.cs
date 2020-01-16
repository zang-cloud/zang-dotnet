using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with sip ip access control lists and access control list ips
    /// </summary>
    public class SipIpAccessControlListsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing ip access control list
        /// </summary>
        public void ViewIpAccessControlList()
        {
            try
            {
                // View ip access control list using sip ip access control lists connector
                var ipAccessControlList = service.SipIpAccessControlListsConnector.ViewIpAccessControlList("TestIpAccessControlListSid");
                Console.WriteLine(ipAccessControlList.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing ip access control lists
        /// </summary>
        public void ListIpAccessControlLists()
        {
            try
            {
                // List ip access control lists using sip ip access control lists connector
                var ipAccessControlLists = service.SipIpAccessControlListsConnector.ListIpAccessControlLists(1, 50);
                Console.WriteLine(ipAccessControlLists.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of creating ip access control list
        /// </summary>
        public void CreateIpAccessControlList()
        {
            try
            {
                // Create ip access control list using sip ip access control lists connector
                var ipAccessControlList = service.SipIpAccessControlListsConnector.CreateIpAccessControlList("IpAclListFriendlyName");
                Console.WriteLine(ipAccessControlList.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of updating ip access control list
        /// </summary>
        public void UpdateIpAccessControlList()
        {
            try
            {
                // Update ip access control list using sip ip access control lists connector
                var ipAccessControlList = service.SipIpAccessControlListsConnector.UpdateIpAccessControlList("TestIpAccessControlListSid",
                    "IpAclListNewFriendlyName");
                Console.WriteLine(ipAccessControlList.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting ip access control list
        /// </summary>
        public void DeleteIpAccessControlList()
        {
            try
            {
                // Delete ip access control list using sip ip access control lists connector
                var ipAccessControlList = service.SipIpAccessControlListsConnector.DeleteIpAccessControlList("TestIpAccessControlListSid");
                Console.WriteLine(ipAccessControlList.Sid);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of viewing access control list ip
        /// </summary>
        public void ViewAccessControlListIp()
        {
            try
            {
                // View access control list ip using sip ip access control lists connector
                var accessControlListIp = service.SipIpAccessControlListsConnector.ViewAccessControlListIp("TestIpAccessControlListSid", "TestIpAddressSid");
                Console.WriteLine(accessControlListIp.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing access control list ips
        /// </summary>
        public void ListAccessControlListIps()
        {
            try
            {
                // List access control list ips using sip ip access control lists connector
                var accessControlListIps = service.SipIpAccessControlListsConnector.ListAccessControlListIps("TestIpAccessControlListSid");
                Console.WriteLine(accessControlListIps.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of adding access control list ip
        /// </summary>
        public void AddAccessControlListIp()
        {
            try
            {
                // Add access control list ip using sip ip access control lists connector
                var accessControlListIp = service.SipIpAccessControlListsConnector.AddAccessControlListIp("TestIpAccessControlListSid", "IpAddressFriendlyName",
                    "10.0.0.1");
                Console.WriteLine(accessControlListIp.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of updating access control list ip
        /// </summary>
        public void UpdateAccessControlListIp()
        {
            try
            {
                // Update access control list ip using sip ip access control lists connector
                var accessControlListIp = service.SipIpAccessControlListsConnector.UpdateAccessControlListIp("TestIpAccessControlListSid", "TestIpAddressSid",
                    "IpAddressNewFriendlyName", "10.0.0.2");
                Console.WriteLine(accessControlListIp.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting access control list ip
        /// </summary>
        public void DeleteAccessControlListIp()
        {
            try
            {
                // Delete access control list ip using sip ip access control lists connector
                var accessControlListIp = service.SipIpAccessControlListsConnector.DeleteAccessControlListIp("TestIpAccessControlListSid", "TestIpAddressSid");
                Console.WriteLine(accessControlListIp.Sid);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
