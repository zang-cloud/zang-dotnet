using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;
using ZangAPI.Model.Enums;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Zang service to work with available phone numbers
    /// </summary>
    public class AvailablePhoneNumbersConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

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
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
