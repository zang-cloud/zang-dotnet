using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// MMS connector - used for all forms of communication with the Mms endpoint of the Zang REST API
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class MmsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MmsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public MmsConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Sends MMS message
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="to">Must be an MMS capable number. The value does not have to be in any specific format.</param>
        /// <param name="body">Text of the MMS to be sent.</param>
        /// <param name="from">Must be a Zang number associated with your account. The value does not have to be in any specific format.</param>
        /// <param name="statusCallback">The URL that will be sent information about the MMS.Url length is limited to 200 characters.</param>
        /// <param name="mediaUrl">URL of an image to be sent in the message.</param>
        /// <returns>Returns created mms message</returns>
        public MmsMessage SendMms(string accountSid, string to, string body, String mediaUrl, string from = null,
            string statusCallback = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/MMS/Messages.json");

            // Mark obligatory parameters
            Require.Argument("To", to);
            Require.Argument("Body", body);

            // Add SendMms query and body parameters
            this.SetParamsForSendMms(request, to, body, mediaUrl, from, statusCallback);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<MmsMessage>(response);
        }

        /// <summary>
        /// Sends MMS message. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="to">Must be an MMS capable number. The value does not have to be in any specific format.</param>
        /// <param name="body">Text of the MMS to be sent.</param>
        /// <param name="from">Must be a Zang number associated with your account. The value does not have to be in any specific format.</param>
        /// <param name="statusCallback">The URL that will be sent information about the MMS.Url length is limited to 200 characters.</param>
        /// <param name="mediaUrl">URL of an image to be sent in the message.</param>
        /// <returns>Returns created mms message</returns>
        public MmsMessage SendMms(string to, string mediaUrl, string body = null, string from = null,
            string statusCallback = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.SendMms(accountSid, to, mediaUrl, body, from, statusCallback);
        }

        /// <summary>
        /// Sets the parameters for send MMS.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="to">To.</param>
        /// <param name="body">The body.</param>
        /// <param name="from">From.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="mediaUrl">URL of an image to be sent in the message.</param>
        private void SetParamsForSendMms(IRestRequest request, string to, string mediaUrl, string body, string from,
            string statusCallback)
        {
            request.AddParameter("To", to);
            request.AddParameter("MediaUrl", mediaUrl);

            if (body.HasValue()) request.AddParameter("Body", body);
            if (from.HasValue()) request.AddParameter("From", from);
            if (statusCallback.HasValue()) request.AddParameter("StatusCallback", statusCallback);
        }

    }
}