using System;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using ZangAPI.Configuration;
using ZangAPI.ConnectionManager;
using ZangAPI.Exceptions;
using ZangAPI.Model;
using ZangAPI.Model.Lists;
using ZangAPI.Model.Enums;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Calls connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class CallsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public CallsConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        //todo MakeCall
        public Call MakeCall(string to, string from, string url, string accountSid = null, string fallbackUrl = null,
            string statusCallback = null, string heartbeatUrl = null, string forwardedFrom = null, string playDtmf = null, string recordCallback = null,
            string transcribeCallback = null, string ifMachineUrl = null, string sipAuthUsername = null, string sipAuthPassword = null,
            HttpMethod method = HttpMethod.POST, HttpMethod fallbackMethod = HttpMethod.POST,
            HttpMethod statusCallbackMethod = HttpMethod.POST, HttpMethod heartbeatMethod = HttpMethod.POST,
            int timeout = 60, bool hideCallerId = false, bool record = false,
            HttpMethod recordCallbackMethod = HttpMethod.POST, bool transcribe = false, bool straightToVoicemail = false,
            IfMachine ifMachine = IfMachine.CONTINUE, HttpMethod ifMachineMethod = HttpMethod.POST)
        {
            // Mark obligatory parameters
            Require.Argument("To", to);
            Require.Argument("From", from);
            Require.Argument("Url", url);

            var client = HttpProvider.GetHttpClient();

            if (!accountSid.HasValue())
            {
                accountSid = HttpProvider.GetConfiguration().AccountSid;
            }

            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Calls.json");

            this.SetParamsForMakeCall(request, to, from, url, fallbackUrl, statusCallback, heartbeatUrl, forwardedFrom, playDtmf, recordCallback,
                transcribeCallback, ifMachineUrl, sipAuthUsername, sipAuthPassword, method, fallbackMethod, statusCallbackMethod, heartbeatMethod,
                timeout, hideCallerId, record, recordCallbackMethod, transcribe, straightToVoicemail, ifMachine, ifMachineMethod);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        //todo ViewCall
        //String accountSid, String callSid

        //String accountSid, String to, String from, CallStatus status, Date startTimeGte, Date startTimeLt, Integer page, Integer pageSize
        public CallList ListCalls(string accountSid = null, CallStatus? status = null, string to = null, string from = null, DateTime startTimeGte = default(DateTime), DateTime startTimeLt = default(DateTime), int? page = null, int? pageSize = null)
        {
            var client = HttpProvider.GetHttpClient();

            if (!accountSid.HasValue())
            {
                accountSid = HttpProvider.GetConfiguration().AccountSid;
            }

            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Calls.json");

            this.SetParamsForListCalls(request, to, from, status, startTimeGte, startTimeLt, page, pageSize);

            var response = client.Execute(request);

            return this.ReturnOrThrowException<CallList>(response);
        }     

        //todo InterruptLiveCall
        //String accountSid, String callSid, String url, HttpMethod method, EndCallStatus status)
        //addparams

        //todo SendDigitsToLiveCall
        //String accountSid, String callSid, String playDtmf, AudioDirection playDtmfDirection

        //todo RecordLiveCall
        //String accountSid, String callSid, Boolean record, RecordingAudioDirection direction, Integer timeLimit, String callbackUrl, RecordingFileFormat fileFormat, Boolean trimSilence, Boolean transcribe, TranscribeQuality transcribeQuality, String transcribeCallback

        //todo PlayAudioToLiveCall
        //String accountSid, String callSid, String audioUrl, RecordingAudioDirection direction, Boolean loop

        //todo ApplyVoiceEffect
        //String accountSid, String callSid, AudioDirection direction, Integer pitch, Integer pitchSemiTones, Integer pitchOctaves, Integer rate, Integer tempo

        /// <summary>
        /// Sets the parameters for make call.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="url">The URL.</param>
        /// <param name="fallbackUrl">The fallback URL.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="heartbeatUrl">The heartbeat URL.</param>
        /// <param name="forwardedFrom">The forwarded from.</param>
        /// <param name="playDtmf">The play DTMF.</param>
        /// <param name="recordCallback">The record callback.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <param name="ifMachineUrl">If machine URL.</param>
        /// <param name="sipAuthUsername">The sip authentication username.</param>
        /// <param name="sipAuthPassword">The sip authentication password.</param>
        /// <param name="method">The method.</param>
        /// <param name="fallbackMethod">The fallback method.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="heartbeatMethod">The heartbeat method.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="hideCallerId">if set to <c>true</c> [hide caller identifier].</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="recordCallbackMethod">The record callback method.</param>
        /// <param name="transcribe">if set to <c>true</c> [transcribe].</param>
        /// <param name="straightToVoicemail">if set to <c>true</c> [straight to voicemail].</param>
        /// <param name="ifMachine">If machine.</param>
        /// <param name="ifMachineMethod">If machine method.</param>
        private void SetParamsForMakeCall(IRestRequest request, string to, string from, string url, string fallbackUrl, string statusCallback, string heartbeatUrl, string forwardedFrom, string playDtmf, string recordCallback,
            string transcribeCallback, string ifMachineUrl, string sipAuthUsername, string sipAuthPassword, HttpMethod method, HttpMethod fallbackMethod,
            HttpMethod statusCallbackMethod, HttpMethod heartbeatMethod, int timeout, bool hideCallerId, bool record, HttpMethod recordCallbackMethod, bool transcribe,
            bool straightToVoicemail, IfMachine ifMachine, HttpMethod ifMachineMethod)
        {
            request.AddParameter("To", to);
            request.AddParameter("From", from);
            request.AddParameter("Url", url);

            request.AddParameter("Method", method);
            if (fallbackUrl.HasValue()) request.AddParameter("FallbackUrl", fallbackUrl);
            request.AddParameter("FallbackMethod", fallbackMethod.ToString().ToUpper());
            if (statusCallback.HasValue()) request.AddParameter("StatusCallback", statusCallback);
            request.AddParameter("StatusCallbackMethod", statusCallbackMethod.ToString().ToUpper());
            if (heartbeatUrl.HasValue()) request.AddParameter("HeartbeatUrl", heartbeatUrl);
            request.AddParameter("HeartbeatMethod", heartbeatMethod.ToString().ToUpper());
            if (forwardedFrom.HasValue()) request.AddParameter("ForwardedFrom", forwardedFrom);
            if (playDtmf.HasValue()) request.AddParameter("PlayDtmf", playDtmf);
            request.AddParameter("Timeout", timeout);
            request.AddParameter("HideCallerId", hideCallerId);
            request.AddParameter("Record", record);
            if (recordCallback.HasValue()) request.AddParameter("RecordCallback", recordCallback);
            request.AddParameter("RecordCallbackMethod", recordCallbackMethod.ToString().ToUpper());
            request.AddParameter("Transcribe", transcribe);
            if (transcribeCallback.HasValue()) request.AddParameter("TranscribeCallback", transcribeCallback);
            request.AddParameter("StraightToVoicemail", straightToVoicemail);
            request.AddParameter("IfMachine", ifMachine.ToString().ToUpper());
            if (ifMachineUrl.HasValue()) request.AddParameter("IfMachineUrl", ifMachineUrl);
            request.AddParameter("IfMachineMethod", ifMachineMethod.ToString().ToUpper());
            if (sipAuthUsername.HasValue()) request.AddParameter("SipAuthUsername", sipAuthUsername);
            if (sipAuthPassword.HasValue()) request.AddParameter("SipAuthPassword", sipAuthPassword);
        }

        /// <summary>
        /// Sets the parameters for list calls.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="status">The status.</param>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="startTimeGte">The start time gte.</param>
        /// <param name="startTimeLt">The start time lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListCalls(RestRequest request, string to, string from, CallStatus? status, DateTime startTimeGte, DateTime startTimeLt, int? page, int? pageSize)
        {
            if (to.HasValue()) request.AddQueryParameter("To", to);
            if (from.HasValue()) request.AddQueryParameter("From", from);
            if (status != null) request.AddQueryParameter("Status", status.ToString());
            if (startTimeGte != default(DateTime))
                request.AddQueryParameter("StartTime", startTimeGte.ToString("yyyy-MM-dd"));
            if (startTimeLt != default(DateTime))
                request.AddQueryParameter("StartTime", startTimeLt.ToString("yyyy-MM-dd"));
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}