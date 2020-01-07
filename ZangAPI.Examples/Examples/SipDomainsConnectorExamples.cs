using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with sip domains, mapped credentials lists and mapped ip access control lists
    /// </summary>
    public class SipDomainsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing domain
        /// </summary>
        public void ViewDomain()
        {
            try
            {
                // View domain using sip domains connector
                var domain = service.SipDomainsConnector.ViewDomain("TestDomainSid");
                Console.WriteLine(domain.DomainName);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing domains
        /// </summary>
        public void ListDomains()
        {
            try
            {
                // List domains using sip domains connector
                var domains = service.SipDomainsConnector.ListDomains();
                Console.WriteLine(domains.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of creating domain
        /// </summary>
        public void CreateDomain()
        {
            try
            {
                // Create domain using sip domains connector
                var domain = service.SipDomainsConnector.CreateDomain(AccountSid, "testdomain.com", friendlyName:"TestDomain", hearbeatUrl:"hearbeatUrl");
                Console.WriteLine(domain.Sid);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of updating domain
        /// </summary>
        public void UpdateDomain()
        {
            try
            {
                // Update domain using sip domains connector
                var domain = service.SipDomainsConnector.UpdateDomain(AccountSid, "testdomain.com", friendlyName: "TestDomainNewName", hearbeatUrl: "hearbeatUrl");
                Console.WriteLine(domain.FriendlyName);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting domain
        /// </summary>
        public void DeleteDomain()
        {
            try
            {
                // Delete domain using sip domains connector
                var domain = service.SipDomainsConnector.DeleteDomain("TestDomainSid");
                Console.WriteLine(domain.Sid);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of mapping credentials list
        /// </summary>
        public void MapCredentialsList()
        {
            try
            {
                // Map credentials listView domain using sip domains connector
                var mappedCredentialList = service.SipDomainsConnector.MapCredentialsList("TestDomainSid", "TestCredentialsListSid");
                Console.WriteLine(mappedCredentialList.FriendlyName);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing mapped credentials lists
        /// </summary>
        public void ListMappedCredentialsLists()
        {
            try
            {
                // List mapped credentials lists using sip domains connector
                var mappedCredentialsLists = service.SipDomainsConnector.ListMappedCredentialsLists("TestDomainSid");
                Console.WriteLine(mappedCredentialsLists.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting mapped credentials list
        /// </summary>
        public void DeleteMappedCredentialsList()
        {
            try
            {
                // Delete mapped credentials list using sip domains connector
                var mappedCredentialsList = service.SipDomainsConnector.DeleteMappedCredentialsList("TestDomainSid",
                    "TestCredentialsListSid");
                Console.WriteLine(mappedCredentialsList.Sid);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of mapping ip access control list
        /// </summary>
        public void MapIpAccessControlList()
        {
            try
            {
                // Map ip access control list using sip domains connector
                var mappedIpAccessControlList = service.SipDomainsConnector.MapIpAccessControlList("TestDomainSid", "TestIpAccessControlListSid");
                Console.WriteLine(mappedIpAccessControlList.FriendlyName);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing mapped ip access control lists
        /// </summary>
        public void ListMappedIpAccessControlLists()
        {
            try
            {
                // List mapped ip access control lists using sip domains connector
                var mappedIpAccessControlLists = service.SipDomainsConnector.ListMappedIpAccessControlLists("TestDomainSid");
                Console.WriteLine(mappedIpAccessControlLists.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting mapped ip access control list
        /// </summary>
        public void DeleteMappedIpAccessControlList()
        {
            try
            {
                // Delete mapped ip access control list using sip domains connector
                var mappedCredentialsList = service.SipDomainsConnector.DeleteMappedIpAccessControlList("TestDomainSid",
                    "TestIpAccessControlListSid");
                Console.WriteLine(mappedCredentialsList.Sid);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
