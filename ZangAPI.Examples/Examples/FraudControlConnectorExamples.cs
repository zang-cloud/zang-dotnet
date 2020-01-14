using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with fraud control
    /// </summary>
    public class FraudControlConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of listing fraud control resources
        /// </summary>
        public void ListFraudControlResources()
        {
            try
            {
                // List fraud control resources using fraud control connector
                var fraudControlResources = service.FraudControlConnector.ListFraudControlResources();
                Console.WriteLine(fraudControlResources.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of blocking destination
        /// </summary>
        public void BlockDestination()
        {
            try
            {
                // Block destination using fraud control connector
                var fraudControlRule = service.FraudControlConnector.BlockDestination("HR", false, true, false);
                Console.WriteLine(fraudControlRule.CountryName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of authorizing destination
        /// </summary>
        public void AuthorizeDestination()
        {
            try
            {
                // Authorize destination using fraud control connector
                var fraudControlRule = service.FraudControlConnector.AuthorizeDestination("HR", false, true, false);
                Console.WriteLine(fraudControlRule.CountryName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of extending destination's authorization
        /// </summary>
        public void ExtendDestinationAuthorization()
        {
            try
            {
                // Extend authorization using fraud control connector
                var fraudControlRule = service.FraudControlConnector.ExtendDestinationAuthorization("HR");
                Console.WriteLine(fraudControlRule.CountryName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of whitelisting destination
        /// </summary>
        public void WhitelistDestination()
        {
            try
            {
                // Whitelist destination using fraud control connector
                var fraudControlRule = service.FraudControlConnector.WhitelistDestination("HR", false, true, false);
                Console.WriteLine(fraudControlRule.CountryName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
