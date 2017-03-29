using System;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Exceptions;
using ZangAPI.Model;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// SMS connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class SmsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public SmsConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="to">To.</param>
        /// <param name="body">The body.</param>
        /// <param name="from">From.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="allowMultiple">if set to <c>true</c> [allow multiple].</param>
        /// <returns>Returns response in SmsMessage</returns>
        public SmsMessage SendSms(string accountSid, string to, string body, string from = null,
            string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST, bool allowMultiple = false)
        { 
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/SMS/Messages.json");

            // Mark obligatory parameters
            Require.Argument("To", to);
            Require.Argument("Body", body);

            // Add SendSmsparameters
            this.SetParamsForSendSms(request, to, body, from, statusCallback, statusCallbackMethod, allowMultiple);

            // Send request
            var response = client.Execute(request);

            //todo
            // Check for errors
            if ((int) response.StatusCode >= 400)
            {
                throw JsonConvert.DeserializeObject<ZangException>(response.Content);
            }

            // Return SmsMessage
            return JsonConvert.DeserializeObject<SmsMessage>(response.Content);
        }

        /// <summary>
        /// Views the SMS message.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="smsMessageSid">The SMS message sid.</param>
        /// <returns>Returns sms message</returns>
        public SmsMessage ViewSmsMessage(string accountSid, string smsMessageSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SMS/Messages/{smsMessageSid}.json");

            // Send request
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<SmsMessage>(response.Content);
        }

        /// <summary>
        /// Lists the SMS messages.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="dateSentGte">The date sent gte.</param>
        /// <param name="dateSentLt">The date sent lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns sms list</returns>
        public SmsList ListSmsMessages(string accountSid, string to = null, string from = null,
            DateTime dateSentGte = default(DateTime), DateTime dateSentLt = default(DateTime), int page = 0,
            int pageSize = 0)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/SMS/Messages.json");

            // Set parameters for list sms messages
            this.SetParamsForListSmsMessages(request, to, from, dateSentGte, dateSentLt, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<SmsList>(response.Content);
        }

        /// <summary>
        /// Sets the parameters for send SMS.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="to">To.</param>
        /// <param name="body">The body.</param>
        /// <param name="from">From.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="allowMultiple">if set to <c>true</c> [allow multiple].</param>
        private void SetParamsForSendSms(IRestRequest request, string to, string body, string from,
            string statusCallback, HttpMethod statusCallbackMethod, bool allowMultiple)
        {
            request.AddParameter("To", to);
            request.AddParameter("Body", body);

            if (from.HasValue()) request.AddParameter("From", from);
            if (statusCallback.HasValue()) request.AddParameter("StatusCallback", statusCallback);
            request.AddParameter("StatusCallbackMethod", statusCallbackMethod.ToString().ToUpper());
            request.AddParameter("AllowMultiple", allowMultiple);
        }

        /// <summary>
        /// Sets the parameters for list SMS messages.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="dateSentGte">The date sent gte.</param>
        /// <param name="dateSentLt">The date sent lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListSmsMessages(IRestRequest request, string to, string from, DateTime dateSentGte,
            DateTime dateSentLt, int page, int pageSize)
        {
            if (to.HasValue()) request.AddQueryParameter("To", to);
            if (from.HasValue()) request.AddQueryParameter("From", from);
            if (dateSentGte != default(DateTime))
                request.AddQueryParameter("DateSent", dateSentGte.ToString("yyyy-MM-dd"));
            if (dateSentLt != default(DateTime))
                request.AddQueryParameter("DateSent", dateSentLt.ToString("yyyy-MM-dd"));
            if (page != 0) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != 0) request.AddQueryParameter("PageSize", pageSize.ToString());
        }      
    }
}