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
    /// Calls connector - used for all forms of communication with the Calls endpoint of the Avaya CPaaS REST API
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
        /// View all information about a call
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
        /// View all information about a call. Uses {accountSid} from configuration in HttpProvider
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
        /// List all calls associated with your account or filter results
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="to">Filter by a specific number calls were made to.</param>
        /// <param name="from">Filter by a specific number calls were made from.</param>
        /// <param name="status">Filter by calls with the specified status.Allowed values are "ringing", "in-progress", "queued", "busy", "completed", "no-answer", and "failed".</param>
        /// <param name="startTimeGte">Filter by start time greater or equal than.</param>
        /// <param name="startTimeLt">Filter by start time less than.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns call list</returns>
        public CallsList ListCalls(string accountSid, string to = null, string from = null, CallStatus? status = null,
            DateTime startTimeGte = default(DateTime), DateTime startTimeLt = default(DateTime), int? page = null,
            int? pageSize = null)
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
        /// List all calls associated with your account or filter results. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="to">Filter by a specific number calls were made to.</param>
        /// <param name="from">Filter by a specific number calls were made from.</param>
        /// <param name="status">Filter by calls with the specified status.Allowed values are "ringing", "in-progress", "queued", "busy", "completed", "no-answer", and "failed".</param>
        /// <param name="startTimeGte">Filter by start time greater or equal than.</param>
        /// <param name="startTimeLt">Filter by start time less than.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns call list</returns>
        public CallsList ListCalls(string to = null, string from = null, CallStatus? status = null,
            DateTime startTimeGte = default(DateTime), DateTime startTimeLt = default(DateTime), int? page = null,
            int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListCalls(accountSid, to, from, status, startTimeGte, startTimeLt, page, pageSize);
        }

        /// <summary>
        /// Make a call
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="to">The phone number or SIP endpoint to call. Phone number should be in international format and one recipient per request. For e.g, to dial a number in the US, the To should be, +17325551212. SIP endpoints must be prefixed with sip: e.g sip:12345@sip.zang.io.</param>
        /// <param name="from">The number to display as calling (i.e. Caller ID). The value does not have to be a real phone number or even in a valid format. For example, 8143 could be passed to the From parameter and would be displayed as the caller ID. Spoofed calls carry an additional charge.</param>
        /// <param name="url">The URL requested once the call connects. This URL must be valid and should return InboundXML containing instructions on how to process your call. A badly formatted URL will NOT fallback to the FallbackUrl but return an error without placing the call. URL length is limited to 200 characters.</param>
        /// <param name="method">The HTTP method used to request the URL once the call connects. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="fallbackUrl">URL used if the required URL is unavailable or if any errors occur during execution of the InboundXML returned by the required URL. Url length is limited to 200 characters.</param>
        /// <param name="fallbackMethod">The HTTP method used to request the FallbackUrl once the call connects. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="statusCallback">A URL that will be requested when the call connects and ends, sending information about the call. URL length is limited to 200 characters.</param>
        /// <param name="statusCallbackMethod">The HTTP method used to request the StatusCallback URL. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="heartbeatUrl">A URL that will be requested every 60 seconds during the call, sending information about the call. The HeartbeatUrl will NOT be requested unless at least 60 seconds of call time have elapsed. URL length is limited to 200 characters.</param>
        /// <param name="heartbeatMethod">The HTTP method used to request the HeartbeatUrl. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="forwardedFrom">Specifies the Forwarding From number to pass to the carrier.</param>
        /// <param name="playDtmf">Dial digits or play tones using DTMF as soon as the call connects. Useful for navigating IVRs. Allowed values for digits are 0-9, #, *, W, or w (W and w are for .5 second pauses), for example 142##* (spaces are valid). Tones follow the @1000 syntax, for example to play the tone 4 for two seconds, 4@2000 (milliseconds) would be used.</param>
        /// <param name="timeout">Number of seconds call stays on line while waiting for an answer. The max time limit is 999.</param>
        /// <param name="hideCallerId">Specifies if the Caller ID will be blocked. Allowed positive values are "true" and "True" - any other value will default to "false".</param>
        /// <param name="record">Specifies if this call should be recorded. Allowed positive values are "true", "True" and "1" - any other value will default to "false". Please note that no more than 5 recordings may be associated with a single call.</param>
        /// <param name="recordCallback">The URL some parameters regarding the recording will be passed to once it is completed. The longer the recording time, the longer the process delay in returning the recording information. If no RecordCallback is given, the recording will still be saved to the system and available either in your Recording Logs or via a REST List Recordings request. Url length is limited to 200 characters.</param>
        /// <param name="recordCallbackMethod">The HTTP method used to request the RecordCallback. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="transcribe">Specifies whether this call should be transcribed. Allowed positive values are "true", "True", and "1".</param>
        /// <param name="transcribeCallback">The URL some parameters regarding the transcription will be passed to once it is completed. The longer the recording time, the longer the process delay in returning the transcription information. If no TranscribeCallback is given, the recording will still be saved to the system and available either in your Transcriptions Logs or via a REST List Transcriptions (ADD URL LINK) request. Url length is limited to 200 characters.</param>
        /// <param name="straightToVoicemail">Specifies whether this call should be sent straight to the user's voicemail. Allowed positive values are "true" and "True" - any other value will default to "false".</param>
        /// <param name="ifMachine">Specifies how Avaya CPaaS should handle this call if it goes to voicemail. Allowed values are "continue" to proceed as normal, "redirect" to redirect the call to the ifMachineUrl, or "hangup" to hang up the call. Hangup occurs when the tone is played. IfMachine accuracy is around 90% and may not work in all countries.</param>
        /// <param name="ifMachineUrl">The URL Avaya CPaaS will redirect to for instructions if a voicemail machine is detected while the IfMachine parameter is set to "redirect". Url length is limited to 200 characters.</param>
        /// <param name="ifMachineMethod">The HTTP method used to request the IfMachineUrl. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="sipAuthUsername">Your authenticated SIP username, used only for SIP calls.</param>
        /// <param name="sipAuthPassword">Your authenticated SIP password, used only for SIP calls.</param>
        /// <returns>Returns created call</returns>
        public Call MakeCall(string accountSid, string to, string from, string url, HttpMethod method = HttpMethod.POST,
            string fallbackUrl = null, HttpMethod fallbackMethod = HttpMethod.POST, string statusCallback = null,
            HttpMethod statusCallbackMethod = HttpMethod.POST, string heartbeatUrl = null,
            HttpMethod heartbeatMethod = HttpMethod.POST,
            string forwardedFrom = null, string playDtmf = null, int timeout = 60, bool hideCallerId = false,
            bool record = false,
            string recordCallback = null, HttpMethod recordCallbackMethod = HttpMethod.POST, bool transcribe = false,
            string transcribeCallback = null, bool straightToVoicemail = false, IfMachine ifMachine = IfMachine.CONTINUE,
            string ifMachineUrl = null, HttpMethod ifMachineMethod = HttpMethod.POST, string sipAuthUsername = null,
            string sipAuthPassword = null)
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
            this.SetParamsForMakeCall(request, to, from, url, method, fallbackUrl, fallbackMethod, statusCallback,
                statusCallbackMethod, heartbeatUrl, heartbeatMethod,
                forwardedFrom, playDtmf, timeout, hideCallerId, record, recordCallback, recordCallbackMethod, transcribe,
                transcribeCallback,
                straightToVoicemail, ifMachine, ifMachineUrl, ifMachineMethod, sipAuthUsername, sipAuthPassword);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Make a call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="to">The phone number or SIP endpoint to call. Phone number should be in international format and one recipient per request. For e.g, to dial a number in the US, the To should be, +17325551212. SIP endpoints must be prefixed with sip: e.g sip:12345@sip.zang.io.</param>
        /// <param name="from">The number to display as calling (i.e. Caller ID). The value does not have to be a real phone number or even in a valid format. For example, 8143 could be passed to the From parameter and would be displayed as the caller ID. Spoofed calls carry an additional charge.</param>
        /// <param name="url">The URL requested once the call connects. This URL must be valid and should return InboundXML containing instructions on how to process your call. A badly formatted URL will NOT fallback to the FallbackUrl but return an error without placing the call. URL length is limited to 200 characters.</param>
        /// <param name="method">The HTTP method used to request the URL once the call connects. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="fallbackUrl">URL used if the required URL is unavailable or if any errors occur during execution of the InboundXML returned by the required URL. Url length is limited to 200 characters.</param>
        /// <param name="fallbackMethod">The HTTP method used to request the FallbackUrl once the call connects. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="statusCallback">A URL that will be requested when the call connects and ends, sending information about the call. URL length is limited to 200 characters.</param>
        /// <param name="statusCallbackMethod">The HTTP method used to request the StatusCallback URL. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="heartbeatUrl">A URL that will be requested every 60 seconds during the call, sending information about the call. The HeartbeatUrl will NOT be requested unless at least 60 seconds of call time have elapsed. URL length is limited to 200 characters.</param>
        /// <param name="heartbeatMethod">The HTTP method used to request the HeartbeatUrl. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="forwardedFrom">Specifies the Forwarding From number to pass to the carrier.</param>
        /// <param name="playDtmf">Dial digits or play tones using DTMF as soon as the call connects. Useful for navigating IVRs. Allowed values for digits are 0-9, #, *, W, or w (W and w are for .5 second pauses), for example 142##* (spaces are valid). Tones follow the @1000 syntax, for example to play the tone 4 for two seconds, 4@2000 (milliseconds) would be used.</param>
        /// <param name="timeout">Number of seconds call stays on line while waiting for an answer. The max time limit is 999.</param>
        /// <param name="hideCallerId">Specifies if the Caller ID will be blocked. Allowed positive values are "true" and "True" - any other value will default to "false".</param>
        /// <param name="record">Specifies if this call should be recorded. Allowed positive values are "true", "True" and "1" - any other value will default to "false". Please note that no more than 5 recordings may be associated with a single call.</param>
        /// <param name="recordCallback">The URL some parameters regarding the recording will be passed to once it is completed. The longer the recording time, the longer the process delay in returning the recording information. If no RecordCallback is given, the recording will still be saved to the system and available either in your Recording Logs or via a REST List Recordings request. Url length is limited to 200 characters.</param>
        /// <param name="recordCallbackMethod">The HTTP method used to request the RecordCallback. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="transcribe">Specifies whether this call should be transcribed. Allowed positive values are "true", "True", and "1".</param>
        /// <param name="transcribeCallback">The URL some parameters regarding the transcription will be passed to once it is completed. The longer the recording time, the longer the process delay in returning the transcription information. If no TranscribeCallback is given, the recording will still be saved to the system and available either in your Transcriptions Logs or via a REST List Transcriptions (ADD URL LINK) request. Url length is limited to 200 characters.</param>
        /// <param name="straightToVoicemail">Specifies whether this call should be sent straight to the user's voicemail. Allowed positive values are "true" and "True" - any other value will default to "false".</param>
        /// <param name="ifMachine">Specifies how Avaya CPaaS should handle this call if it goes to voicemail. Allowed values are "continue" to proceed as normal, "redirect" to redirect the call to the ifMachineUrl, or "hangup" to hang up the call. Hangup occurs when the tone is played. IfMachine accuracy is around 90% and may not work in all countries.</param>
        /// <param name="ifMachineUrl">The URL Avaya CPaaS will redirect to for instructions if a voicemail machine is detected while the IfMachine parameter is set to "redirect". Url length is limited to 200 characters.</param>
        /// <param name="ifMachineMethod">The HTTP method used to request the IfMachineUrl. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="sipAuthUsername">Your authenticated SIP username, used only for SIP calls.</param>
        /// <param name="sipAuthPassword">Your authenticated SIP password, used only for SIP calls.</param>
        /// <returns>Returns created call</returns>
        public Call MakeCall(string to, string from, string url, HttpMethod method = HttpMethod.POST,
            string fallbackUrl = null, HttpMethod fallbackMethod = HttpMethod.POST, string statusCallback = null,
            HttpMethod statusCallbackMethod = HttpMethod.POST, string heartbeatUrl = null,
            HttpMethod heartbeatMethod = HttpMethod.POST,
            string forwardedFrom = null, string playDtmf = null, int timeout = 60, bool hideCallerId = false,
            bool record = false, string recordCallback = null, HttpMethod recordCallbackMethod = HttpMethod.POST,
            bool transcribe = false,
            string transcribeCallback = null, bool straightToVoicemail = false, IfMachine ifMachine = IfMachine.CONTINUE,
            string ifMachineUrl = null, HttpMethod ifMachineMethod = HttpMethod.POST, string sipAuthUsername = null,
            string sipAuthPassword = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.MakeCall(accountSid, to, from, url, method, fallbackUrl, fallbackMethod, statusCallback,
                statusCallbackMethod, heartbeatUrl, heartbeatMethod,
                forwardedFrom, playDtmf, timeout, hideCallerId, record, recordCallback, recordCallbackMethod, transcribe,
                transcribeCallback,
                straightToVoicemail, ifMachine, ifMachineUrl, ifMachineMethod, sipAuthUsername, sipAuthPassword);
        }

        /// <summary>
        /// Send new instructions to the call
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">Call SID.</param>
        /// <param name="url">The URL that in-progress calls will request for new instructions.</param>
        /// <param name="method">The HTTP method used to request the redirect URL. Valid parameters are GET and POST.</param>
        /// <param name="status">The status used to end the call. Allowed values are "canceled" for ending queued or ringing calls, and "completed" to end in-progress calls in addition to queued and ringing calls.</param>
        /// <returns>Returns call</returns>
        public Call InterruptLiveCall(string accountSid, string callSid, string url = null,
            HttpMethod method = HttpMethod.POST, CallStatus? status = null)
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
        /// Send new instructions to the call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">Call SID.</param>
        /// <param name="url">The URL that in-progress calls will request for new instructions.</param>
        /// <param name="method">The HTTP method used to request the redirect URL. Valid parameters are GET and POST.</param>
        /// <param name="status">The status used to end the call. Allowed values are "canceled" for ending queued or ringing calls, and "completed" to end in-progress calls in addition to queued and ringing calls.</param>
        /// <returns>Returns call</returns>
        public Call InterruptLiveCall(string callSid, string url = null, HttpMethod method = HttpMethod.POST,
            CallStatus? status = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.InterruptLiveCall(accountSid, callSid, url, method, status);
        }

        /// <summary>
        /// Use DTMF tones to mimic button presses
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">Call SID.</param>
        /// <param name="playDtmf">Allowed values are the digits 0-9, #, *, W, or w. "w" and "W"stand for 1/2 second pauses. You can combine these values together, for example, "12ww34". Tones are also supported and follow the @1000 syntax, for example to play the tone 4 for two seconds, 4@2000 (milliseconds) would be used.</param>
        /// <param name="playDtmfDirection">Specifies which leg of the call DTMF tones will be played on. Allowed values are “in” to send tones to the incoming caller or “out” to send tones to the out going caller.</param>
        /// <returns>Returns call</returns>
        public Call SendDigitsToLiveCall(string accountSid, string callSid, string playDtmf = null,
            AudioDirection? playDtmfDirection = null)
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
        /// Use DTMF tones to mimic button presses. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">Call SID.</param>
        /// <param name="playDtmf">Allowed values are the digits 0-9, #, *, W, or w. "w" and "W"stand for 1/2 second pauses. You can combine these values together, for example, "12ww34". Tones are also supported and follow the @1000 syntax, for example to play the tone 4 for two seconds, 4@2000 (milliseconds) would be used.</param>
        /// <param name="playDtmfDirection">Specifies which leg of the call DTMF tones will be played on. Allowed values are “in” to send tones to the incoming caller or “out” to send tones to the out going caller.</param>
        /// <returns>Returns call</returns>
        public Call SendDigitsToLiveCall(string callSid, string playDtmf = null,
            AudioDirection? playDtmfDirection = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.SendDigitsToLiveCall(accountSid, callSid, playDtmf, playDtmfDirection);
        }

        /// <summary>
        /// Options include time limit, file format, trimming silence and transcribing
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">Call SID.</param>
        /// <param name="record">Specifies if a call recording should start or end. Allowed values are "true" to start recording and "false" to end recording. Any number of simultaneous, separate recordings can be initiated.</param>
        /// <param name="direction">Specifies which audio stream to record. Allowed values are "in" to record the incoming caller's audio, "out" to record the outgoing caller's audio, and "both" to record both.</param>
        /// <param name="timeLimit">The maximum duration of the recording. Allowed value is an integer greater than 0.</param>
        /// <param name="callbackUrl">A URL that will be requested when the recording ends, sending information about the recording.The longer the recording, the longer the delay in processing the recording and requesting the CallbackUrl.Url length is limited to 200 characters.</param>
        /// <param name="fileFormat">Specifies the file format of the recording. Allowed values are "mp3" or "wav" - any other value will default to "mp3".</param>
        /// <param name="trimSilence">Trims all silence from the beginning of the recording. Allowed values are "true" or "false" - any other value will default to "false".</param>
        /// <param name="transcribe">Specifies if this recording should be transcribed. Allowed values are "true" and "false" - all other values will default to "false".</param>
        /// <param name="transcribeQuality">Specifies the quality of the transcription. Allowed values are "auto" for automated transcriptions and "hybrid" for human-reviewed transcriptions - all other values will default to "auto".</param>
        /// <param name="transcribeCallback">A URL that will be requested when the call ends, sending information about the transcription. The longer the recording, the longer the delay in processing the transcription and requesting the TranscribeCallback. Url length is limited to 200 characters.</param>
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
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Calls/{callSid}/Recordings.json");

            // Add RecordLiveCall query and body parameters
            this.SetParamsForRecordLiveCall(request, record, direction, timeLimit, callbackUrl, fileFormat, trimSilence,
                transcribe,
                transcribeQuality, transcribeCallback);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Options include time limit, file format, trimming silence and transcribing. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">Call SID.</param>
        /// <param name="record">Specifies if a call recording should start or end. Allowed values are "true" to start recording and "false" to end recording. Any number of simultaneous, separate recordings can be initiated.</param>
        /// <param name="direction">Specifies which audio stream to record. Allowed values are "in" to record the incoming caller's audio, "out" to record the outgoing caller's audio, and "both" to record both.</param>
        /// <param name="timeLimit">The maximum duration of the recording. Allowed value is an integer greater than 0.</param>
        /// <param name="callbackUrl">A URL that will be requested when the recording ends, sending information about the recording.The longer the recording, the longer the delay in processing the recording and requesting the CallbackUrl.Url length is limited to 200 characters.</param>
        /// <param name="fileFormat">Specifies the file format of the recording. Allowed values are "mp3" or "wav" - any other value will default to "mp3".</param>
        /// <param name="trimSilence">Trims all silence from the beginning of the recording. Allowed values are "true" or "false" - any other value will default to "false".</param>
        /// <param name="transcribe">Specifies if this recording should be transcribed. Allowed values are "true" and "false" - all other values will default to "false".</param>
        /// <param name="transcribeQuality">Specifies the quality of the transcription. Allowed values are "auto" for automated transcriptions and "hybrid" for human-reviewed transcriptions - all other values will default to "auto".</param>
        /// <param name="transcribeCallback">A URL that will be requested when the call ends, sending information about the transcription. The longer the recording, the longer the delay in processing the transcription and requesting the TranscribeCallback. Url length is limited to 200 characters.</param>
        /// <returns>Returns Call</returns>
        public Call RecordLiveCall(string callSid, bool record,
            RecordingAudioDirection direction = RecordingAudioDirection.BOTH,
            int? timeLimit = null, string callbackUrl = null, RecordingFileFormat fileFormat = RecordingFileFormat.MP3,
            bool trimSilence = false, bool transcribe = false,
            TranscribeQuality transcribeQuality = TranscribeQuality.AUTO, string transcribeCallback = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.RecordLiveCall(accountSid, callSid, record, direction, timeLimit, callbackUrl, fileFormat,
                trimSilence,
                transcribe, transcribeQuality, transcribeCallback);
        }

        /// <summary>
        /// Options include restricting to one caller and looping
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">Call SID.</param>
        /// <param name="audioUrl">A URL returning the sound file to play. Progressive downloads and SHOUTCAST streaming are also supported.</param>
        /// <param name="direction">Specifies which caller will hear the played audio. Allowed values are "in" to play audio to the incoming caller, "out" to play to the outgoing caller, and "both" to play the audio to both callers.</param>
        /// <param name="loop">Specifies whether the audio will loop. Allowed values are "true" and "false".</param>
        /// <returns>Returns call</returns>
        public Call PlayAudioToLiveCall(string accountSid, string callSid, string audioUrl,
            RecordingAudioDirection direction = RecordingAudioDirection.BOTH, bool loop = false)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Mark obligatory parameters
            Require.Argument("AudioUrl", audioUrl);

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Calls/{callSid}/Play.json");

            // Add PlayAudioToLiveCall query and body parameters
            this.SetParamsForPlayAudioToLiveCall(request, audioUrl, direction, loop);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Options include restricting to one caller and looping. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">Call SID.</param>
        /// <param name="audioUrl">A URL returning the sound file to play. Progressive downloads and SHOUTCAST streaming are also supported.</param>
        /// <param name="direction">Specifies which caller will hear the played audio. Allowed values are "in" to play audio to the incoming caller, "out" to play to the outgoing caller, and "both" to play the audio to both callers.</param>
        /// <param name="loop">Specifies whether the audio will loop. Allowed values are "true" and "false".</param>
        /// <returns>Returns call</returns>
        public Call PlayAudioToLiveCall(string callSid, string audioUrl,
            RecordingAudioDirection direction = RecordingAudioDirection.BOTH, bool loop = false)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.PlayAudioToLiveCall(accountSid, callSid, audioUrl, direction, loop);
        }

        /// <summary>
        /// Applies voice effect on the call
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">Call SID.</param>
        /// <param name="direction">Specifies which caller should have their voice modified. Allowed values are "in" for the incoming caller and "out" for the outgoing caller. This value can be changed as often as you like to control live call flow.</param>
        /// <param name="pitch">Sets the pitch. The lower the value, the lower the tone. Allowed values are integers greater than 0.</param>
        /// <param name="pitchSemiTones">Changes the pitch of audio in semitone intervals. Allowed values are integers between -14 and 14.</param>
        /// <param name="pitchOctaves">Changes the pitch of the audio in octave intervals. Allowed values are integers between -1 and 1.</param>
        /// <param name="rate">Sets the rate. The lower the value, the lower the rate. Allowed values are integers greater than 0.</param>
        /// <param name="tempo">Sets the tempo. The lower the value, the slower the tempo. Allowed values are integers greater than 0.</param>
        /// <returns>Returns call</returns>
        public Call ApplyVoiceEffect(string accountSid, string callSid, AudioDirection direction = AudioDirection.OUT,
            int pitch = 1, int pitchSemiTones = 1, int pitchOctaves = 1, int rate = 1, int tempo = 1)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Calls/{callSid}/Effects.json");

            // Add ApplyVoiceEffect query and body parameters
            this.SetParamsForApplyVoiceEffect(request, direction, pitch, pitchSemiTones, pitchOctaves, rate, tempo);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Call>(response);
        }

        /// <summary>
        /// Applies voice effect on the call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">Call SID.</param>
        /// <param name="direction">Specifies which caller should have their voice modified. Allowed values are "in" for the incoming caller and "out" for the outgoing caller. This value can be changed as often as you like to control live call flow.</param>
        /// <param name="pitch">Sets the pitch. The lower the value, the lower the tone. Allowed values are integers greater than 0.</param>
        /// <param name="pitchSemiTones">Changes the pitch of audio in semitone intervals. Allowed values are integers between -14 and 14.</param>
        /// <param name="pitchOctaves">Changes the pitch of the audio in octave intervals. Allowed values are integers between -1 and 1.</param>
        /// <param name="rate">Sets the rate. The lower the value, the lower the rate. Allowed values are integers greater than 0.</param>
        /// <param name="tempo">Sets the tempo. The lower the value, the slower the tempo. Allowed values are integers greater than 0.</param>
        /// <returns>Returns call</returns>
        public Call ApplyVoiceEffect(string callSid, AudioDirection direction = AudioDirection.OUT,
            int pitch = 1, int pitchSemiTones = 1, int pitchOctaves = 1, int rate = 1, int tempo = 1)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ApplyVoiceEffect(accountSid, callSid, direction, pitch, pitchSemiTones, pitchOctaves, rate,
                tempo);
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
        private void SetParamsForMakeCall(IRestRequest request, string to, string from, string url, HttpMethod method,
            string fallbackUrl, HttpMethod fallbackMethod,
            string statusCallback, HttpMethod statusCallbackMethod, string heartbeatUrl, HttpMethod heartbeatMethod,
            string forwardedFrom, string playDtmf,
            int timeout, bool hideCallerId, bool record, string recordCallback, HttpMethod recordCallbackMethod,
            bool transcribe, string transcribeCallback,
            bool straightToVoicemail, IfMachine ifMachine, string ifMachineUrl, HttpMethod ifMachineMethod,
            string sipAuthUsername, string sipAuthPassword)
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
        private void SetParamsForListCalls(IRestRequest request, string to, string from, CallStatus? status,
            DateTime startTimeGte, DateTime startTimeLt, int? page, int? pageSize)
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
        private void SetParamsForInterruptLiveCall(IRestRequest request, string url, HttpMethod method,
            CallStatus? status)
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
        private void SetParamsForSendDigitsToLiveCall(IRestRequest request, string playDtmf,
            AudioDirection? playDtmfDirection)
        {
            if (playDtmf.HasValue()) request.AddParameter("PlayDtmf", playDtmf);
            if (playDtmfDirection != null)
                request.AddParameter("PlayDtmfDirection", EnumHelper.GetEnumValue(playDtmfDirection));
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
        private void SetParamsForRecordLiveCall(IRestRequest request, bool record, RecordingAudioDirection direction,
            int? timeLimit,
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
        private void SetParamsForPlayAudioToLiveCall(IRestRequest request, string audioUrl,
            RecordingAudioDirection direction, bool loop)
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
        private void SetParamsForApplyVoiceEffect(IRestRequest request, AudioDirection direction = AudioDirection.OUT,
            int pitch = 1, int pitchSemiTones = 1,
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