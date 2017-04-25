using RestSharp;
using RestSharp.Extensions;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Incoming numbers connector - used for all forms of communication with the Incoming Phone Numbers endpoint of the Zang REST API
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class IncomingPhoneNumbersConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncomingPhoneNumbersConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public IncomingPhoneNumbersConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Shows info on an incoming phone number
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="incomingNumberSid">Incoming number SID.</param>
        /// <returns>Returns incoming phone number</returns>
        public IncomingPhoneNumber ViewIncomingNumber(string accountSid, string incomingNumberSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/IncomingPhoneNumbers/{incomingNumberSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IncomingPhoneNumber>(response);
        }

        /// <summary>
        /// Shows info on an incoming phone number. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="incomingNumberSid">Incoming number SID.</param>
        /// <returns>Returns incoming phone number</returns>
        public IncomingPhoneNumber ViewIncomingNumber(string incomingNumberSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewIncomingNumber(accountSid, incomingNumberSid);
        }

        /// <summary>
        /// Shows info on all incoming numbers associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="contains">List numbers containing certain digits.</param>
        /// <param name="friendlyName">Specifies that only IncomingPhoneNumber resources matching the input FriendlyName should be returned in the list request.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns incoming phone number list</returns>
        public IncomingPhoneNumbersList ListIncomingNumbers(string accountSid, string contains = null,
            string friendlyName = null,
            int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/IncomingPhoneNumbers.json");

            // Add ListIncomingNumbers query and body parameters
            this.SetParamsForListIncomingNumbers(request, contains, friendlyName, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IncomingPhoneNumbersList>(response);
        }

        /// <summary>
        /// Shows info on all incoming numbers associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="contains">List numbers containing certain digits.</param>
        /// <param name="friendlyName">Specifies that only IncomingPhoneNumber resources matching the input FriendlyName should be returned in the list request.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns incoming phone number list</returns>
        public IncomingPhoneNumbersList ListIncomingNumbers(string contains = null, string friendlyName = null,
            int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListIncomingNumbers(accountSid, contains, friendlyName, page, pageSize);
        }

        /// <summary>
        /// Purchases a new incoming number
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="phoneNumber">A specific available phone number you wish to add.</param>
        /// <param name="areaCode">The area code from which a random available number will be added.</param>
        /// <param name="friendlyName">User generated name for the incoming number.</param>
        /// <param name="voiceUrl">The URL returning InboundXML incoming calls should execute when connected.</param>
        /// <param name="voiceMethod">Specifies the HTTP method used to request the VoiceUrl once incoming call connects.</param>
        /// <param name="voiceFallbackUrl">URL used if any errors occur during execution of InboundXML on a call or at initial request of the VoiceUrl.</param>
        /// <param name="voiceFallbackMethod">Specifies the HTTP method used to request the VoiceFallbackUrl once incoming call connects.</param>
        /// <param name="voiceCallerIdLookup">Look up the caller’s caller-ID name from the CNAM database(additional charges apply).</param>
        /// <param name="smsUrl">The URL returning InboundXML incoming phone numbers should execute when receiving an SMS.</param>
        /// <param name="smsMethod">Specifies the HTTP method used to request the SmsUrl once an incoming SMS is received.</param>
        /// <param name="smsFallbackUrl">URL used if any errors occur during execution of InboundXML from an SMS or at initial request of the SmsUrl.</param>
        /// <param name="smsFallbackMethod">Specifies the HTTP method used to request the SmsFallbackUrl.</param>
        /// <param name="heartbeatUrl">URL that can be used to monitor the phone number.</param>
        /// <param name="heartbeatMethod">The HTTP method Zang will use when requesting the HeartbeatURL.</param>
        /// <param name="statusCallback">URL that can be requested to receive notification when and how incoming call has ended.</param>
        /// <param name="statusCallbackMethod">The HTTP method Zang will use when requesting the HangupCallback URL.</param>
        /// <param name="hangupCallback">This is a StatusCallback clone that will be phased out in future versions.</param>
        /// <param name="hangupCallbackMethod">This is a StatusCallbackMethod clone that will be phased out in future versions.</param>
        /// <param name="voiceApplicationSid">The SID of the Voice Application you wish to associate with this incoming number.</param>
        /// <param name="smsApplicationSid">The SID of the SMS Application you wish to associate with this incoming number.</param>
        /// <returns>Returns purchased incoming number</returns>
        public IncomingPhoneNumber PurchaseIncomingNumber(string accountSid, string phoneNumber = null,
            string areaCode = null, string friendlyName = null, string voiceUrl = null,
            HttpMethod voiceMethod = HttpMethod.POST, string voiceFallbackUrl = null,
            HttpMethod voiceFallbackMethod = HttpMethod.POST, bool voiceCallerIdLookup = false, string smsUrl = null,
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null,
            HttpMethod smsFallbackMethod = HttpMethod.POST, string heartbeatUrl = null,
            HttpMethod heartbeatMethod = HttpMethod.POST,
            string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST,
            string hangupCallback = null, HttpMethod hangupCallbackMethod = HttpMethod.POST,
            string voiceApplicationSid = null, string smsApplicationSid = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/IncomingPhoneNumbers.json");

            // Add PurchaseIncomingNumber query and body parameters
            this.SetParamsForPurchaseIncomingNumber(request, phoneNumber, areaCode, friendlyName, voiceUrl, voiceMethod,
                voiceFallbackUrl, voiceFallbackMethod, voiceCallerIdLookup, smsUrl, smsMethod,
                smsFallbackUrl, smsFallbackMethod, heartbeatUrl, heartbeatMethod, statusCallback, statusCallbackMethod,
                hangupCallback, hangupCallbackMethod, voiceApplicationSid, smsApplicationSid);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IncomingPhoneNumber>(response);
        }

        /// <summary>
        /// Purchases a new incoming number. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="phoneNumber">A specific available phone number you wish to add.</param>
        /// <param name="areaCode">The area code from which a random available number will be added.</param>
        /// <param name="friendlyName">User generated name for the incoming number.</param>
        /// <param name="voiceUrl">The URL returning InboundXML incoming calls should execute when connected.</param>
        /// <param name="voiceMethod">Specifies the HTTP method used to request the VoiceUrl once incoming call connects.</param>
        /// <param name="voiceFallbackUrl">URL used if any errors occur during execution of InboundXML on a call or at initial request of the VoiceUrl.</param>
        /// <param name="voiceFallbackMethod">Specifies the HTTP method used to request the VoiceFallbackUrl once incoming call connects.</param>
        /// <param name="voiceCallerIdLookup">Look up the caller’s caller-ID name from the CNAM database(additional charges apply).</param>
        /// <param name="smsUrl">The URL returning InboundXML incoming phone numbers should execute when receiving an SMS.</param>
        /// <param name="smsMethod">Specifies the HTTP method used to request the SmsUrl once an incoming SMS is received.</param>
        /// <param name="smsFallbackUrl">URL used if any errors occur during execution of InboundXML from an SMS or at initial request of the SmsUrl.</param>
        /// <param name="smsFallbackMethod">Specifies the HTTP method used to request the SmsFallbackUrl.</param>
        /// <param name="heartbeatUrl">URL that can be used to monitor the phone number.</param>
        /// <param name="heartbeatMethod">The HTTP method Zang will use when requesting the HeartbeatURL.</param>
        /// <param name="statusCallback">URL that can be requested to receive notification when and how incoming call has ended.</param>
        /// <param name="statusCallbackMethod">The HTTP method Zang will use when requesting the HangupCallback URL.</param>
        /// <param name="hangupCallback">This is a StatusCallback clone that will be phased out in future versions.</param>
        /// <param name="hangupCallbackMethod">This is a StatusCallbackMethod clone that will be phased out in future versions.</param>
        /// <param name="voiceApplicationSid">The SID of the Voice Application you wish to associate with this incoming number.</param>
        /// <param name="smsApplicationSid">The SID of the SMS Application you wish to associate with this incoming number.</param>
        /// <returns>Returns purchased incoming number</returns>
        public IncomingPhoneNumber PurchaseIncomingNumber(string phoneNumber = null, string areaCode = null,
            string friendlyName = null, string voiceUrl = null,
            HttpMethod voiceMethod = HttpMethod.POST, string voiceFallbackUrl = null,
            HttpMethod voiceFallbackMethod = HttpMethod.POST, bool voiceCallerIdLookup = false, string smsUrl = null,
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null,
            HttpMethod smsFallbackMethod = HttpMethod.POST, string heartbeatUrl = null,
            HttpMethod heartbeatMethod = HttpMethod.POST,
            string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST,
            string hangupCallback = null, HttpMethod hangupCallbackMethod = HttpMethod.POST,
            string voiceApplicationSid = null, string smsApplicationSid = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.PurchaseIncomingNumber(accountSid, phoneNumber, areaCode, friendlyName, voiceUrl, voiceMethod,
                voiceFallbackUrl, voiceFallbackMethod, voiceCallerIdLookup, smsUrl, smsMethod,
                smsFallbackUrl, smsFallbackMethod, heartbeatUrl, heartbeatMethod, statusCallback, statusCallbackMethod,
                hangupCallback, hangupCallbackMethod, voiceApplicationSid, smsApplicationSid);
        }

        /// <summary>
        /// Updates an incoming phone number data
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="incomingPhoneNumberSid">34 characters long unique incoming phone number identifier</param>
        /// <param name="friendlyName">User generated name for the incoming number.</param>
        /// <param name="voiceUrl">The URL returning InboundXML incoming calls should execute when connected.</param>
        /// <param name="voiceMethod">Specifies the HTTP method used to request the VoiceUrl once incoming call connects.</param>
        /// <param name="voiceFallbackUrl">URL used if any errors occur during execution of InboundXML on a call or at initial request of the VoiceUrl.</param>
        /// <param name="voiceFallbackMethod">Specifies the HTTP method used to request the VoiceFallbackUrl once incoming call connects.</param>
        /// <param name="voiceCallerIdLookup">Look up the caller’s caller-ID name from the CNAM database(additional charges apply).</param>
        /// <param name="smsUrl">The URL returning InboundXML incoming phone numbers should execute when receiving an SMS.</param>
        /// <param name="smsMethod">Specifies the HTTP method used to request the SmsUrl once an incoming SMS is received.</param>
        /// <param name="smsFallbackUrl">URL used if any errors occur during execution of InboundXML from an SMS or at initial request of the SmsUrl.</param>
        /// <param name="smsFallbackMethod">Specifies the HTTP method used to request the SmsFallbackUrl.</param>
        /// <param name="heartbeatUrl">URL that can be used to monitor the phone number.</param>
        /// <param name="heartbeatMethod">The HTTP method Zang will use when requesting the HeartbeatURL.</param>
        /// <param name="statusCallback">URL that can be requested to receive notification when and how incoming call has ended.</param>
        /// <param name="statusCallbackMethod">The HTTP method Zang will use when requesting the HangupCallback URL.</param>
        /// <param name="hangupCallback">This is a StatusCallback clone that will be phased out in future versions.</param>
        /// <param name="hangupCallbackMethod">This is a StatusCallbackMethod clone that will be phased out in future versions.</param>
        /// <returns>Returns updated incoming number</returns>
        public IncomingPhoneNumber UpdateIncomingNumber(string accountSid, string incomingPhoneNumberSid,
            string friendlyName = null, string voiceUrl = null,
            HttpMethod voiceMethod = HttpMethod.POST, string voiceFallbackUrl = null,
            HttpMethod voiceFallbackMethod = HttpMethod.POST, bool voiceCallerIdLookup = false, string smsUrl = null,
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null,
            HttpMethod smsFallbackMethod = HttpMethod.POST, string heartbeatUrl = null,
            HttpMethod heartbeatMethod = HttpMethod.POST,
            string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST,
            string hangupCallback = null, HttpMethod hangupCallbackMethod = HttpMethod.POST)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/IncomingPhoneNumbers/{incomingPhoneNumberSid}.json");

            // Add UpdateIncomingNumber query and body parameters
            this.SetParamsForUpdatingIncomingNumber(request, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl,
                voiceFallbackMethod, voiceCallerIdLookup, smsUrl, smsMethod,
                smsFallbackUrl, smsFallbackMethod, heartbeatUrl, heartbeatMethod, statusCallback, statusCallbackMethod,
                hangupCallback, hangupCallbackMethod);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IncomingPhoneNumber>(response);
        }

        /// <summary>
        /// Updates an incoming phone number data. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="incomingPhoneNumberSid">34 characters long unique incoming phone number identifier</param>
        /// <param name="friendlyName">User generated name for the incoming number.</param>
        /// <param name="voiceUrl">The URL returning InboundXML incoming calls should execute when connected.</param>
        /// <param name="voiceMethod">Specifies the HTTP method used to request the VoiceUrl once incoming call connects.</param>
        /// <param name="voiceFallbackUrl">URL used if any errors occur during execution of InboundXML on a call or at initial request of the VoiceUrl.</param>
        /// <param name="voiceFallbackMethod">Specifies the HTTP method used to request the VoiceFallbackUrl once incoming call connects.</param>
        /// <param name="voiceCallerIdLookup">Look up the caller’s caller-ID name from the CNAM database(additional charges apply).</param>
        /// <param name="smsUrl">The URL returning InboundXML incoming phone numbers should execute when receiving an SMS.</param>
        /// <param name="smsMethod">Specifies the HTTP method used to request the SmsUrl once an incoming SMS is received.</param>
        /// <param name="smsFallbackUrl">URL used if any errors occur during execution of InboundXML from an SMS or at initial request of the SmsUrl.</param>
        /// <param name="smsFallbackMethod">Specifies the HTTP method used to request the SmsFallbackUrl.</param>
        /// <param name="heartbeatUrl">URL that can be used to monitor the phone number.</param>
        /// <param name="heartbeatMethod">The HTTP method Zang will use when requesting the HeartbeatURL.</param>
        /// <param name="statusCallback">URL that can be requested to receive notification when and how incoming call has ended.</param>
        /// <param name="statusCallbackMethod">The HTTP method Zang will use when requesting the HangupCallback URL.</param>
        /// <param name="hangupCallback">This is a StatusCallback clone that will be phased out in future versions.</param>
        /// <param name="hangupCallbackMethod">This is a StatusCallbackMethod clone that will be phased out in future versions.</param>
        /// <returns>Returns updated incoming number</returns>
        public IncomingPhoneNumber UpdateIncomingNumber(string incomingPhoneNumberSid, string friendlyName = null,
            string voiceUrl = null,
            HttpMethod voiceMethod = HttpMethod.POST, string voiceFallbackUrl = null,
            HttpMethod voiceFallbackMethod = HttpMethod.POST, bool voiceCallerIdLookup = false, string smsUrl = null,
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null,
            HttpMethod smsFallbackMethod = HttpMethod.POST, string heartbeatUrl = null,
            HttpMethod heartbeatMethod = HttpMethod.POST,
            string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST,
            string hangupCallback = null, HttpMethod hangupCallbackMethod = HttpMethod.POST)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.PurchaseIncomingNumber(accountSid, incomingPhoneNumberSid, friendlyName, voiceUrl, voiceMethod,
                voiceFallbackUrl, voiceFallbackMethod, voiceCallerIdLookup, smsUrl, smsMethod,
                smsFallbackUrl, smsFallbackMethod, heartbeatUrl, heartbeatMethod, statusCallback, statusCallbackMethod,
                hangupCallback, hangupCallbackMethod);
        }

        /// <summary>
        /// Deletes an incoming phone number
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="incomingPhoneNumberSid">34 characters long unique incoming phone number identifier.</param>
        /// <returns>Returns deleted incoming phone number</returns>
        public IncomingPhoneNumber DeleteIncomingNumber(string accountSid, string incomingPhoneNumberSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/IncomingPhoneNumbers/{incomingPhoneNumberSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<IncomingPhoneNumber>(response);
        }

        /// <summary>
        /// Deletes an incoming phone number. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="incomingPhoneNumberSid">34 characters long unique incoming phone number identifier.</param>
        /// <returns>Returns deleted incoming phone number</returns>
        public IncomingPhoneNumber DeleteIncomingNumber(string incomingPhoneNumberSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteIncomingNumber(accountSid, incomingPhoneNumberSid);
        }

        /// <summary>
        /// Sets the parameters for list incoming phone numbers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="contains">The contains.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListIncomingNumbers(IRestRequest request, string contains, string friendlyName,
            int? page, int? pageSize)
        {
            if (contains.HasValue()) request.AddQueryParameter("Contains", contains);
            if (friendlyName.HasValue()) request.AddQueryParameter("FriendlyName", friendlyName);
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }

        /// <summary>
        /// Sets the parameters for purchase incoming number.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="voiceCallerIdLookup">if set to <c>true</c> [voice caller identifier lookup].</param>
        /// <param name="smsUrl">The SMS URL.</param>
        /// <param name="smsMethod">The SMS method.</param>
        /// <param name="smsFallbackUrl">The SMS fallback URL.</param>
        /// <param name="smsFallbackMethod">The SMS fallback method.</param>
        /// <param name="heartbeatUrl">The heartbeat URL.</param>
        /// <param name="heartbeatMethod">The heartbeat method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="hangupCallback">The hangup callback.</param>
        /// <param name="hangupCallbackMethod">The hangup callback method.</param>
        /// <param name="voiceApplicationSid">The voice application sid.</param>
        /// <param name="smsApplicationSid">The SMS application sid.</param>
        private void SetParamsForPurchaseIncomingNumber(IRestRequest request, string phoneNumber, string areaCode,
            string friendlyName, string voiceUrl, HttpMethod voiceMethod, string voiceFallbackUrl,
            HttpMethod voiceFallbackMethod,
            bool voiceCallerIdLookup, string smsUrl,
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null,
            HttpMethod smsFallbackMethod = HttpMethod.POST, string heartbeatUrl = null,
            HttpMethod heartbeatMethod = HttpMethod.POST,
            string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST,
            string hangupCallback = null, HttpMethod hangupCallbackMethod = HttpMethod.POST,
            string voiceApplicationSid = null, string smsApplicationSid = null)
        {
            if (phoneNumber.HasValue()) request.AddParameter("PhoneNumber", phoneNumber);
            if (areaCode.HasValue()) request.AddParameter("AreaCode", areaCode);
            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
            if (voiceUrl.HasValue()) request.AddParameter("VoiceUrl", voiceUrl);
            request.AddParameter("VoiceMethod", voiceMethod);
            if (voiceFallbackUrl.HasValue()) request.AddParameter("VoiceFallbackUrl", voiceFallbackUrl);
            request.AddParameter("VoiceFallbackMethod", voiceFallbackMethod);
            request.AddParameter("VoiceCallerIdLookup", voiceCallerIdLookup);
            if (smsUrl.HasValue()) request.AddParameter("SmsUrl", smsUrl);
            request.AddParameter("SmsMethod", smsMethod);
            if (smsFallbackUrl.HasValue()) request.AddParameter("SmsFallbackUrl", smsFallbackUrl);
            request.AddParameter("SmsFallbackMethod", smsFallbackMethod);
            if (heartbeatUrl.HasValue()) request.AddParameter("HeartbeatUrl", heartbeatUrl);
            request.AddParameter("HeartbeatMethod", heartbeatMethod);
            if (statusCallback.HasValue()) request.AddParameter("StatusCallback", statusCallback);
            request.AddParameter("StatusCallbackMethod", statusCallbackMethod);
            if (hangupCallback.HasValue()) request.AddParameter("HangupCallback", hangupCallback);
            request.AddParameter("HangupCallbackMethod", hangupCallbackMethod);
            if (voiceApplicationSid.HasValue()) request.AddParameter("VoiceApplicationSid", voiceApplicationSid);
            if (smsApplicationSid.HasValue()) request.AddParameter("SmsApplicationSid", smsApplicationSid);
        }

        /// <summary>
        /// Sets the parameters for updating incoming number.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="voiceCallerIdLookup">if set to <c>true</c> [voice caller identifier lookup].</param>
        /// <param name="smsUrl">The SMS URL.</param>
        /// <param name="smsMethod">The SMS method.</param>
        /// <param name="smsFallbackUrl">The SMS fallback URL.</param>
        /// <param name="smsFallbackMethod">The SMS fallback method.</param>
        /// <param name="heartbeatUrl">The heartbeat URL.</param>
        /// <param name="heartbeatMethod">The heartbeat method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="hangupCallback">The hangup callback.</param>
        /// <param name="hangupCallbackMethod">The hangup callback method.</param>
        private void SetParamsForUpdatingIncomingNumber(IRestRequest request, string friendlyName, string voiceUrl,
            HttpMethod voiceMethod, string voiceFallbackUrl, HttpMethod voiceFallbackMethod, bool voiceCallerIdLookup,
            string smsUrl, HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null,
            HttpMethod smsFallbackMethod = HttpMethod.POST, string heartbeatUrl = null,
            HttpMethod heartbeatMethod = HttpMethod.POST,
            string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST,
            string hangupCallback = null, HttpMethod hangupCallbackMethod = HttpMethod.POST)
        {
            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
            if (voiceUrl.HasValue()) request.AddParameter("VoiceUrl", voiceUrl);
            request.AddParameter("VoiceMethod", voiceMethod);
            if (voiceFallbackUrl.HasValue()) request.AddParameter("VoiceFallbackUrl", voiceFallbackUrl);
            request.AddParameter("VoiceFallbackMethod", voiceFallbackMethod);
            request.AddParameter("VoiceCallerIdLookup", voiceCallerIdLookup);
            if (smsUrl.HasValue()) request.AddParameter("SmsUrl", smsUrl);
            request.AddParameter("SmsMethod", smsMethod);
            if (smsFallbackUrl.HasValue()) request.AddParameter("SmsFallbackUrl", smsFallbackUrl);
            request.AddParameter("SmsFallbackMethod", smsFallbackMethod);
            if (heartbeatUrl.HasValue()) request.AddParameter("HeartbeatUrl", heartbeatUrl);
            request.AddParameter("HeartbeatMethod", heartbeatMethod);
            if (statusCallback.HasValue()) request.AddParameter("StatusCallback", statusCallback);
            request.AddParameter("StatusCallbackMethod", statusCallbackMethod);
            if (hangupCallback.HasValue()) request.AddParameter("HangupCallback", hangupCallback);
            request.AddParameter("HangupCallbackMethod", hangupCallbackMethod);
        }
    }
}