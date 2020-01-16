using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with applications
    /// </summary>
    public class ApplicationsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing application
        /// </summary>
        public void ViewApplication()
        {
            try
            {
                // View application using applications connector
                var application = service.ApplicationsConnector.ViewApplication("TestApplicationSid");
                Console.WriteLine(application.ClientCount);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing applications
        /// </summary>
        public void ListApplications()
        {
            try
            {
                // List applications using applications connector
                var applications = service.ApplicationsConnector.ListApplications(AccountSid, "TestApplication", 0, 10);
                Console.WriteLine(applications.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of creating application
        /// </summary>
        public void CreateApplication()
        {
            try
            {
                // Create application using applications connector
                var application = service.ApplicationsConnector.CreateApplication(AccountSid, "TestApplication", "voiceUrl", smsFallbackUrl: "smsFallbackUrl", statusCallback: "statusCallback", 
                    statusCallbackMethod: HttpMethod.GET);
                Console.WriteLine(application.Sid);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of updating application
        /// </summary>
        public void UpdateApplication()
        {
            try
            {
                // Update application using applications connector
                var application = service.ApplicationsConnector.UpdateApplication("TestAccountSid", "TestApplicationSid",
                    "TestApplication", "voiceUrl", HttpMethod.POST, "voiceFallbackUrl", HttpMethod.GET, true, "smsUrl",
                    HttpMethod.POST, "smsFallbackUrl", HttpMethod.GET, "heartbeatUrl", HttpMethod.GET, "statusCallback",
                    HttpMethod.POST, "hangupCallback", HttpMethod.GET);
                Console.WriteLine(application.VoiceFallbackUrl);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting application
        /// </summary>
        public void DeleteApplication()
        {
            try
            {
                // Delete application using applications connector
                var application = service.ApplicationsConnector.DeleteApplication("TestApplicationSid");
                Console.WriteLine(application.Sid);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
