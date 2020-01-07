using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;
using ZangAPI.Model.Enums;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with usages
    /// </summary>
    public class UsagesConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing usage
        /// </summary>
        public void ViewUsage()
        {
            try
            {
                // View usage using usages connector
                var usage = service.UsagesConnector.ViewUsage("TestUsageSid");
                Console.WriteLine(usage.AverageCost);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing usages
        /// </summary>
        public void ListUsages()
        {
            try
            {
                // List usages using usages connector
                var usages = service.UsagesConnector.ListUsages(year:2017, month:5, product:Product.INBOUND_CALL, page: 3, pageSize: 40);
                Console.WriteLine(usages.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
