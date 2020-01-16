using System;
using System.Linq;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with sms messages
    /// </summary>
    public class SmsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of sending sms message
        /// </summary>
        public void SendSms()
        {
            try
            {
                // Send sms message using sms connector
                var smsMessage = service.SmsConnector.SendSms("+123", "This is an SMS message", from:"456");
                Console.WriteLine(smsMessage.Body);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of viewing sms message
        /// </summary>
        public void ViewSmsMessage()
        {
            try
            {
                // View sms message using sms connector
                var smsMessage = service.SmsConnector.ViewSmsMessage("SmsMessageSid");
                Console.WriteLine(smsMessage.Body);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing sms messages
        /// </summary>
        public void ListSmsMessages()
        {
            try
            {
                // List sms messages using sms connector
                var smsMessages = service.SmsConnector.ListSmsMessages();
                Console.WriteLine(smsMessages.Elements.Last().To);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
