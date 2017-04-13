using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
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

        /// <summary>
        /// Views the call.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">The call sid.</param>
        /// <returns>Returns call</returns>
        public Call ViewCall(string accountSid, string callSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Calls/{callSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Views the call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">The call sid.</param>
        /// <returns>Returns call</returns>
        public Call ViewCall(string callSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewCall(accountSid, callSid);
        }

        /// <summary>
        /// Lists the calls.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="status">The status.</param>
        /// <param name="startTimeGte">The start time gte.</param>
        /// <param name="startTimeLt">The start time lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns call list</returns>
        public CallsList ListCalls(string accountSid, string to = null, string from = null, CallStatus? status = null, DateTime startTimeGte = default(DateTime), DateTime startTimeLt = default(DateTime), int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Calls.json");

            // Add ListCalls query and body parameters
            this.SetParamsForListCalls(request, to, from, status, startTimeGte, startTimeLt, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CallsList>(response);
        }

        /// <summary>
        /// Lists the calls. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="status">The status.</param>
        /// <param name="startTimeGte">The start time gte.</param>
        /// <param name="startTimeLt">The start time lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns call list</returns>
        public CallsList ListCalls(string to = null, string from = null, CallStatus? status = null, DateTime startTimeGte = default(DateTime),
            DateTime startTimeLt = default(DateTime), int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListCalls(accountSid, to, from, status, startTimeGte, startTimeLt, page, pageSize);
        }

        /// <summary>
        /// Makes the call.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="fallbackUrl">The fallback URL.</param>
        /// <param name="fallbackMethod">The fallback method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="heartbeatUrl">The heartbeat URL.</param>
        /// <param name="heartbeatMethod">The heartbeat method.</param>
        /// <param name="forwardedFrom">The forwarded from.</param>
        /// <param name="playDtmf">The play DTMF.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="hideCallerId">if set to <c>true</c> [hide caller identifier].</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="recordCallback">The record callback.</param>
        /// <param name="recordCallbackMethod">The record callback method.</param>
        /// <param name="transcribe">if set to <c>true</c> [transcribe].</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <param name="straightToVoicemail">if set to <c>true</c> [straight to voicemail].</param>
        /// <param name="ifMachine">If machine.</param>
        /// <param name="ifMachineUrl">If machine URL.</param>
        /// <param name="ifMachineMethod">If machine method.</param>
        /// <param name="sipAuthUsername">The sip authentication username.</param>
        /// <param name="sipAuthPassword">The sip authentication password.</param>
        /// <returns>Returns call</returns>
        public Call MakeCall(string accountSid, string to, string from, string url, HttpMethod method = HttpMethod.POST,
            string fallbackUrl = null, HttpMethod fallbackMethod = HttpMethod.POST, string statusCallback = null,
            HttpMethod statusCallbackMethod = HttpMethod.POST, string heartbeatUrl = null, HttpMethod heartbeatMethod = HttpMethod.POST,
            string forwardedFrom = null, string playDtmf = null, int timeout = 60, bool hideCallerId = false, bool record = false,
            string recordCallback = null, HttpMethod recordCallbackMethod = HttpMethod.POST, bool transcribe = false, 
            string transcribeCallback = null, bool straightToVoicemail = false, IfMachine ifMachine = IfMachine.CONTINUE,
            string ifMachineUrl = null, HttpMethod ifMachineMethod = HttpMethod.POST, string sipAuthUsername = null, string sipAuthPassword = null)
        {
            // Mark obligatory parameters
            Require.Argument("To", to);
            Require.Argument("From", from);
            Require.Argument("Url", url);

            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Calls.json");

            // Add MakeCall query and body parameters
            this.SetParamsForMakeCall(request, to, from, url, method, fallbackUrl, fallbackMethod, statusCallback, statusCallbackMethod, heartbeatUrl, heartbeatMethod,
                forwardedFrom, playDtmf, timeout, hideCallerId, record, recordCallback, recordCallbackMethod, transcribe, transcribeCallback,
                straightToVoicemail, ifMachine, ifMachineUrl, ifMachineMethod, sipAuthUsername, sipAuthPassword);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Makes the call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="fallbackUrl">The fallback URL.</param>
        /// <param name="fallbackMethod">The fallback method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="heartbeatUrl">The heartbeat URL.</param>
        /// <param name="heartbeatMethod">The heartbeat method.</param>
        /// <param name="forwardedFrom">The forwarded from.</param>
        /// <param name="playDtmf">The play DTMF.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="hideCallerId">if set to <c>true</c> [hide caller identifier].</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="recordCallback">The record callback.</param>
        /// <param name="recordCallbackMethod">The record callback method.</param>
        /// <param name="transcribe">if set to <c>true</c> [transcribe].</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <param name="straightToVoicemail">if set to <c>true</c> [straight to voicemail].</param>
        /// <param name="ifMachine">If machine.</param>
        /// <param name="ifMachineUrl">If machine URL.</param>
        /// <param name="ifMachineMethod">If machine method.</param>
        /// <param name="sipAuthUsername">The sip authentication username.</param>
        /// <param name="sipAuthPassword">The sip authentication password.</param>
        /// <returns>Returns call</returns>
        public Call MakeCall(string to, string from, string url, HttpMethod method = HttpMethod.POST,
            string fallbackUrl = null, HttpMethod fallbackMethod = HttpMethod.POST, string statusCallback = null,
            HttpMethod statusCallbackMethod = HttpMethod.POST, string heartbeatUrl = null, HttpMethod heartbeatMethod = HttpMethod.POST,
            string forwardedFrom = null, string playDtmf = null, int timeout = 60, bool hideCallerId = false, bool record = false,
            string recordCallback = null, HttpMethod recordCallbackMethod = HttpMethod.POST, bool transcribe = false,
            string transcribeCallback = null, bool straightToVoicemail = false, IfMachine ifMachine = IfMachine.CONTINUE,
            string ifMachineUrl = null, HttpMethod ifMachineMethod = HttpMethod.POST, string sipAuthUsername = null, string sipAuthPassword = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.MakeCall(accountSid, to, from, url, method, fallbackUrl, fallbackMethod, statusCallback, statusCallbackMethod, heartbeatUrl, heartbeatMethod, 
                forwardedFrom, playDtmf, timeout, hideCallerId, record, recordCallback, recordCallbackMethod, transcribe, transcribeCallback, 
                straightToVoicemail, ifMachine, ifMachineUrl, ifMachineMethod, sipAuthUsername, sipAuthPassword);
        }

        /// <summary>
        /// Interrupts the live call.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">The call sid.</param>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="status">The status.</param>
        /// <returns>Returns call</returns>
        public Call InterruptLiveCall(string accountSid, string callSid, string url = null, HttpMethod method = HttpMethod.POST,
            CallStatus? status = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Calls/{callSid}.json");

            // Add InterruptLiveCall query and body parameters
            this.SetParamsForInterruptLiveCall(request, url, method, status);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Interrupts the live call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">The call sid.</param>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="status">The status.</param>
        /// <returns>Returns call</returns>
        public Call InterruptLiveCall(string callSid, string url = null, HttpMethod method = HttpMethod.POST,
            CallStatus? status = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.InterruptLiveCall(callSid, accountSid, url, method, status);
        }

        /// <summary>
        /// Sends the digits to live call.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">The call sid.</param>
        /// <param name="playDtmf">The play DTMF.</param>
        /// <param name="playDtmfDirection">The play DTMF direction.</param>
        /// <returns>Returns call</returns>
        public Call SendDigitsToLiveCall(string accountSid, string callSid, string playDtmf = null, AudioDirection? playDtmfDirection = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Calls/{callSid}.json");

            // Add SendDigitsToLiveCall query and body parameters
            this.SetParamsForSendDigitsToLiveCall(request, playDtmf, playDtmfDirection);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Sends the digits to live call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">The call sid.</param>
        /// <param name="playDtmf">The play DTMF.</param>
        /// <param name="playDtmfDirection">The play DTMF direction.</param>
        /// <returns>Returns call</returns>
        public Call SendDigitsToLiveCall(string callSid, string playDtmf = null, AudioDirection? playDtmfDirection = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.SendDigitsToLiveCall(accountSid, callSid, playDtmf, playDtmfDirection);
        }

        /// <summary>
        /// Records the live call.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">The call sid.</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="direction">The direction.</param>
        /// <param name="timeLimit">The time limit.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="fileFormat">The file format.</param>
        /// <param name="trimSilence">if set to <c>true</c> [trim silence].</param>
        /// <param name="transcribe">if set to <c>true</c> [transcribe].</param>
        /// <param name="transcribeQuality">The transcribe quality.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <returns>Returns call</returns>
        public Call RecordLiveCall(string accountSid, string callSid, bool record,
            RecordingAudioDirection direction = RecordingAudioDirection.BOTH, int? timeLimit = null,
            string callbackUrl = null, RecordingFileFormat fileFormat = RecordingFileFormat.MP3,
            bool trimSilence = false, bool transcribe = false,
            TranscribeQuality transcribeQuality = TranscribeQuality.AUTO, string transcribeCallback = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Mark obligatory parameters
            Require.Argument("Record", record);

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Calls/{callSid}/Recordings.json");

            // Add RecordLiveCall query and body parameters
            this.SetParamsForRecordLiveCall(request, record, direction, timeLimit, callbackUrl, fileFormat, trimSilence, transcribe, 
                transcribeQuality, transcribeCallback);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Records the live call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">The call sid.</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="direction">The direction.</param>
        /// <param name="timeLimit">The time limit.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="fileFormat">The file format.</param>
        /// <param name="trimSilence">if set to <c>true</c> [trim silence].</param>
        /// <param name="transcribe">if set to <c>true</c> [transcribe].</param>
        /// <param name="transcribeQuality">The transcribe quality.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <returns>Returns Call</returns>
        public Call RecordLiveCall(string callSid, bool record, RecordingAudioDirection direction = RecordingAudioDirection.BOTH, int? timeLimit = null,
            string callbackUrl = null, RecordingFileFormat fileFormat = RecordingFileFormat.MP3,
            bool trimSilence = false, bool transcribe = false,
            TranscribeQuality transcribeQuality = TranscribeQuality.AUTO, string transcribeCallback = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.RecordLiveCall(accountSid, callSid, record, direction, timeLimit, callbackUrl, fileFormat, trimSilence, 
                transcribe, transcribeQuality, transcribeCallback);
        }

        /// <summary>
        /// Plays the audio to live call.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">The call sid.</param>
        /// <param name="audioUrl">The audio URL.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="loop">if set to <c>true</c> [loop].</param>
        /// <returns>Returns call</returns>
        public Call PlayAudioToLiveCall(string accountSid, string callSid, string audioUrl, RecordingAudioDirection direction = RecordingAudioDirection.BOTH, bool loop = false)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Mark obligatory parameters
            Require.Argument("AudioUrl", audioUrl);

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Calls/{callSid}/Play.json");

            // Add PlayAudioToLiveCall query and body parameters
            this.SetParamsForPlayAudioToLiveCall(request, audioUrl, direction, loop);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Plays the audio to live call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">The call sid.</param>
        /// <param name="audioUrl">The audio URL.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="loop">if set to <c>true</c> [loop].</param>
        /// <returns>Returns call</returns>
        public Call PlayAudioToLiveCall(string callSid, string audioUrl,
            RecordingAudioDirection direction = RecordingAudioDirection.BOTH, bool loop = false)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.PlayAudioToLiveCall(accountSid, callSid, audioUrl, direction, loop);
        }

        /// <summary>
        /// Applies the voice effect.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">The call sid.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="pitchSemiTones">The pitch semi tones.</param>
        /// <param name="pitchOctaves">The pitch octaves.</param>
        /// <param name="rate">The rate.</param>
        /// <param name="tempo">The tempo.</param>
        /// <returns>Returns call</returns>
        public Call ApplyVoiceEffect(string accountSid, string callSid, AudioDirection direction = AudioDirection.OUT, int pitch = 1, int pitchSemiTones = 1, int pitchOctaves = 1, int rate = 1, int tempo = 1)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Calls/{callSid}/Effects.json");

            // Add ApplyVoiceEffect query and body parameters
            this.SetParamsForApplyVoiceEffect(request, direction, pitch, pitchSemiTones, pitchOctaves, rate, tempo);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Applies the voice effect. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">The call sid.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="pitchSemiTones">The pitch semi tones.</param>
        /// <param name="pitchOctaves">The pitch octaves.</param>
        /// <param name="rate">The rate.</param>
        /// <param name="tempo">The tempo.</param>
        /// <returns>Returns call</returns>
        public Call ApplyVoiceEffect(string callSid, AudioDirection direction = AudioDirection.OUT,
            int pitch = 1, int pitchSemiTones = 1, int pitchOctaves = 1, int rate = 1, int tempo = 1)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ApplyVoiceEffect(accountSid, callSid, direction, pitch, pitchSemiTones, pitchOctaves, rate, tempo);
        }

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
        private void SetParamsForMakeCall(IRestRequest request, string to, string from, string url, HttpMethod method, string fallbackUrl, HttpMethod fallbackMethod,
            string statusCallback, HttpMethod statusCallbackMethod, string heartbeatUrl, HttpMethod heartbeatMethod, string forwardedFrom, string playDtmf,
            int timeout, bool hideCallerId, bool record, string recordCallback, HttpMethod recordCallbackMethod, bool transcribe, string transcribeCallback,
            bool straightToVoicemail, IfMachine ifMachine, string ifMachineUrl, HttpMethod ifMachineMethod, string sipAuthUsername, string sipAuthPassword)
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
            request.AddParameter("IfMachine", EnumHelper.GetEnumValue(ifMachine));
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
            if (status != null)
                request.AddQueryParameter("Status", EnumHelper.GetEnumValue(status));
            if (startTimeGte != default(DateTime))
                request.AddQueryParameter("StartTime>", startTimeGte.ToString("yyyy-MM-dd"));
            if (startTimeLt != default(DateTime))
                request.AddQueryParameter("StartTime<", startTimeLt.ToString("yyyy-MM-dd"));
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }

        /// <summary>
        /// Sets the parameters for interrupt live call.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="status">The status.</param>
        private void SetParamsForInterruptLiveCall(RestRequest request, string url, HttpMethod method, CallStatus? status)
        {
            if (url.HasValue()) request.AddParameter("Url", url);
            request.AddParameter("Method", method.ToString().ToUpper());
            if (status != null) request.AddParameter("Status", EnumHelper.GetEnumValue(status));
        }

        /// <summary>
        /// Sets the parameters for send digits to live call.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="playDtmf">The play DTMF.</param>
        /// <param name="playDtmfDirection">The play DTMF direction.</param>
        private void SetParamsForSendDigitsToLiveCall(RestRequest request, string playDtmf, AudioDirection? playDtmfDirection)
        {
            if (playDtmf.HasValue()) request.AddParameter("PlayDtmf", playDtmf);
            if (playDtmfDirection != null) request.AddParameter("PlayDtmfDirection", EnumHelper.GetEnumValue(playDtmfDirection));
        }

        /// <summary>
        /// Sets the parameters for record live call.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="direction">The direction.</param>
        /// <param name="timeLimit">The time limit.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="fileFormat">The file format.</param>
        /// <param name="trimSilence">if set to <c>true</c> [trim silence].</param>
        /// <param name="transcribe">if set to <c>true</c> [transcribe].</param>
        /// <param name="transcribeQuality">The transcribe quality.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        private void SetParamsForRecordLiveCall(RestRequest request, bool record, RecordingAudioDirection direction, int? timeLimit,
            string callbackUrl, RecordingFileFormat fileFormat, bool trimSilence, bool transcribe,
            TranscribeQuality transcribeQuality, string transcribeCallback)
        {
            request.AddParameter("Record", record);
            request.AddParameter("Direction", EnumHelper.GetEnumValue(direction));
            if (timeLimit != null) request.AddParameter("TimeLimit", timeLimit.ToString());
            if (callbackUrl.HasValue()) request.AddParameter("CallbackUrl", callbackUrl);
            request.AddParameter("FileFormat", EnumHelper.GetEnumValue(fileFormat));
            request.AddParameter("TrimSilence", trimSilence);
            request.AddParameter("Transcribe", transcribe);
            request.AddParameter("TranscribeQuality", EnumHelper.GetEnumValue(transcribeQuality));
            if (transcribeCallback.HasValue()) request.AddParameter("TranscribeCallback", transcribeCallback);
        }

        /// <summary>
        /// Sets the parameters for play audio to live call.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="audioUrl">The audio URL.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="loop">if set to <c>true</c> [loop].</param>
        private void SetParamsForPlayAudioToLiveCall(RestRequest request, string audioUrl, RecordingAudioDirection direction, bool loop)
        {
            request.AddParameter("AudioUrl", audioUrl);
            request.AddParameter("Direction", EnumHelper.GetEnumValue(direction));
            request.AddParameter("Loop", loop);
        }

        /// <summary>
        /// Sets the parameters for apply voice effect.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="pitchSemiTones">The pitch semi tones.</param>
        /// <param name="pitchOctaves">The pitch octaves.</param>
        /// <param name="rate">The rate.</param>
        /// <param name="tempo">The tempo.</param>
        private void SetParamsForApplyVoiceEffect(RestRequest request, AudioDirection direction = AudioDirection.OUT, int pitch = 1, int pitchSemiTones = 1,
            int pitchOctaves = 1, int rate = 1, int tempo = 1)
        {
            request.AddParameter("AudioDirection", EnumHelper.GetEnumValue(direction));
            request.AddParameter("Pitch", pitch);
            request.AddParameter("PitchSemiTones", pitchSemiTones);
            request.AddParameter("PitchOctaves", pitchOctaves);
            request.AddParameter("Rate", rate);
            request.AddParameter("Tempo", tempo);
        }
    }
}