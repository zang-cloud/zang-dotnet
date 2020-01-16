using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with application clients
    /// </summary>
    public class ApplicationClientsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

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
            catch (CPaaSException e)
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
            catch (CPaaSException e)
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
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
