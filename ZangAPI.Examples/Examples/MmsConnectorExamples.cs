using System;
using System.Linq;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Zang service to work with mms messages
    /// </summary>
    public class MmsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of sending sms message
        /// </summary>
        public void SendMms()
        {
            try
            {
                // Send sms message using sms connector
                var mmsMessage = service.MmsConnector.SendMms("+123456",
                    "https://media.giphy.com/media/zZJzLrxmx5ZFS/giphy.gif", body: "This is MMS sent from Zang",
                    from: "+654321");
                Console.WriteLine(mmsMessage.MediaUrl);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
