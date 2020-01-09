using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with application clients
    /// </summary>
    public class ApplicationClientsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing application client
        /// </summary>
        public void ViewApplicationClient()
        {
            try
            {
                // View application client using application clients connector
                var applicationClient = service.ApplicationClientsConnector.ViewApplicationClient("TestApplicationSid",
                    "TestApplicationClientSid");
                Console.WriteLine(applicationClient.Nickname);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing application clients
        /// </summary>
        public void ListApplicationClients()
        {
            try
            {
                // List application clients using application clients connector
                var applicationClients = service.ApplicationClientsConnector.ListApplicationClients(AccountSid,
                    "TestApplicationSid");
                Console.WriteLine(applicationClients.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of creating application client
        /// </summary>
        public void CreateApplicationClient()
        {
            try
            {
                // Create application client using application clients connector
                var applicationClient = service.ApplicationClientsConnector.CreateApplicationClient("TestApplicationSid",
                    "MyApplicationClient");
                Console.WriteLine(applicationClient.Sid);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
