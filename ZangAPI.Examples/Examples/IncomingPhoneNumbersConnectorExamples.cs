using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with incoming phone numbers
    /// </summary>
    public class IncomingPhoneNumbersConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing incoming phone number
        /// </summary>
        public void ViewIncomingNumber()
        {
            try
            {
                // View incoming phone number using incoming phone numbers connector
                var incomingNumber = service.IncomingPhoneNumbersConnector.ViewIncomingNumber("TestIncomingPhoneNumberSid");
                Console.WriteLine(incomingNumber.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing incoming phone numbers
        /// </summary>
        public void ListIncomingNumbers()
        {
            try
            {
                // List incoming phone numbers using incoming phone numbers connector
                var incomingNumbers = service.IncomingPhoneNumbersConnector.ListIncomingNumbers(contains:"123", friendlyName:"NumberFriendlyName");
                Console.WriteLine(incomingNumbers.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of purchasing incoming phone number
        /// </summary>
        public void PurchaseIncomingNumber()
        {
            try
            {
                // Purchase incoming phone number using incoming phone numbers connector
                var incomingNumber = service.IncomingPhoneNumbersConnector.PurchaseIncomingNumber(areaCode: "123", voiceCallerIdLookup: true);
                Console.WriteLine(incomingNumber.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of updating incoming phone number
        /// </summary>
        public void UpdateIncomingNumber()
        {
            try
            {
                // Update incoming phone number using incoming phone numbers connector
                var incomingNumber = service.IncomingPhoneNumbersConnector.UpdateIncomingNumber("TestIncomingPhoneNumberSid", friendlyName:"NewFriendlyName");
                Console.WriteLine(incomingNumber.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting incoming phone number
        /// </summary>
        public void DeleteIncomingNumber()
        {
            try
            {
                // Delete incoming phone number using incoming phone numbers connector
                var incomingNumber = service.IncomingPhoneNumbersConnector.DeleteIncomingNumber("TestIncomingPhoneNumberSid");
                Console.WriteLine(incomingNumber.PhoneNumber);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
