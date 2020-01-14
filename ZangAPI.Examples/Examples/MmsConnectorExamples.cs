using System;
using System.Linq;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with mms messages
    /// </summary>
    public class MmsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of sending sms message
        /// </summary>
        public void SendMms()
        {
            try
            {
                // Send sms message using sms connector
                var mmsMessage = service.MmsConnector.SendMms("+123456",
                    "https://media.giphy.com/media/zZJzLrxmx5ZFS/giphy.gif", body: "This is MMS sent from CPaaS",
                    from: "+654321");
                Console.WriteLine(mmsMessage.MediaUrl);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
