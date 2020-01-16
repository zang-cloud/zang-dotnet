using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with available phone numbers
    /// </summary>
    public class AvailablePhoneNumbersConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of listing available phone numbers
        /// </summary>
        public void ListAvailableNumbers()
        {
            try
            {
                // List available phone numbers using available phone numbers connector
                var numbers = service.AvailablePhoneNumbersConnector.ListAvailableNumbers("HR", AvailablePhoneNumberType.TOLLFREE, 
                    "123", "043", "Region", "43000");
                Console.WriteLine(numbers.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
