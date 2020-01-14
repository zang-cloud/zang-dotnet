using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using AvayaCPaaS.ConnectionManager;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model;
using AvayaCPaaS.Model.Enums;
using AvayaCPaaS.Model.Lists;

namespace AvayaCPaaS.Connectors
{
    /// <summary>
    /// Recordings connector - used for all forms of communication with the Recordings endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Connectors.AConnector" />
    public class RecordingsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordingsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public RecordingsConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Shows information on some recording
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="recordingSid">Recording SID.</param>
        /// <returns>Returns recording</returns>
        public Recording ViewRecording(string accountSid, string recordingSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Recordings/{recordingSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Recording>(response);
        }

        /// <summary>
        /// Shows information on some recording. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="recordingSid">Recording SID.</param>
        /// <returns>Returns recording</returns>
        public Recording ViewRecording(string recordingSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewRecording(accountSid, recordingSid);
        }

        /// <summary>
        /// Shows info on all recordings associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">Filters by recordings associated with a given CallSid.</param>
        /// <param name="dateCreatedGte">Filter by date created greater or equal than.</param>
        /// <param name="dateCreatedLt">Filter by date created less than.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns recording list</returns>
        public RecordingsList ListRecordings(string accountSid, string callSid = null,
            DateTime dateCreatedGte = default(DateTime),
            DateTime dateCreatedLt = default(DateTime), int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Recordings.json");

            // Add ListRecordings query and body parameters
            this.SetParamsForListRecordings(request, callSid, dateCreatedGte, dateCreatedLt, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<RecordingsList>(response);
        }

        /// <summary>
        /// Shows info on all recordings associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">Filters by recordings associated with a given CallSid.</param>
        /// <param name="dateCreatedGte">Filter by date created greater or equal than.</param>
        /// <param name="dateCreatedLt">Filter by date created less than.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns recording list</returns>
        public RecordingsList ListRecordings(string callSid = null, DateTime dateCreatedGte = default(DateTime),
            DateTime dateCreatedLt = default(DateTime), int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListRecordings(accountSid, callSid, dateCreatedGte, dateCreatedLt, page, pageSize);
        }

        /// <summary>
        /// Records a call
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">Call SID.</param>
        /// <param name="record">Specifies if a call recording should start or end. Allowed values are "true" to start recording and "false" to end recording. Any number of simultaneous, separate recordings can be initiated.</param>
        /// <param name="direction">Specifies which audio stream to record. Allowed values are "in" to record the incoming caller's audio, "out" to record the outgoing caller's audio, and "both" to record both.</param>
        /// <param name="timeLimit">The maximum duration of the recording.Allowed value is an integer greater than 0.</param>
        /// <param name="callbackUrl">A URL that will be requested when the recording ends, sending information about the recording. The longer the recording, the longer the delay in processing the recording and requesting the CallbackUrl. Url length is limited to 200 characters.</param>
        /// <param name="fileFormat">Specifies the file format of the recording. Allowed values are "mp3" or "wav" - any other value will default to "mp3".</param>
        /// <param name="trimSilence">Trims all silence from the beginning of the recording. Allowed values are "true" or "false" - any other value will default to "false".</param>
        /// <param name="transcribe">Specifies if this recording should be transcribed. Allowed values are "true" and "false" - all other values will default to "false".</param>
        /// <param name="transcribeQuality">Specifies the quality of the transcription. Allowed values are "auto" for automated transcriptions and "hybrid" for human-reviewed transcriptions - all other values will default to "auto".</param>
        /// <param name="transcribeCallback">A URL that will be requested when the call ends, sending information about the transcription. The longer the recording, the longer the delay in processing the transcription and requesting the TranscribeCallback. URL length is limited to 200 characters.</param>
        /// <returns>Returns created recording</returns>
        public Recording RecordCall(string accountSid, string callSid, bool record,
            RecordingAudioDirection direction = RecordingAudioDirection.BOTH, int? timeLimit = null,
            string callbackUrl = null, RecordingFileFormat fileFormat = RecordingFileFormat.MP3,
            bool trimSilence = false, bool transcribe = false,
            TranscribeQuality transcribeQuality = TranscribeQuality.AUTO,
            string transcribeCallback = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Calls/{callSid}/Recordings.json");

            // Mark obligatory parameters
            Require.Argument("Record", record);

            // Add RecordCall query and body parameters
            this.SetParamsForRecordCall(request, record, direction, timeLimit, callbackUrl, fileFormat, trimSilence,
                transcribe, transcribeQuality, transcribeCallback);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Recording>(response);
        }

        /// <summary>
        /// Records a call. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">Call SID.</param>
        /// <param name="record">Specifies if a call recording should start or end. Allowed values are "true" to start recording and "false" to end recording. Any number of simultaneous, separate recordings can be initiated.</param>
        /// <param name="direction">Specifies which audio stream to record. Allowed values are "in" to record the incoming caller's audio, "out" to record the outgoing caller's audio, and "both" to record both.</param>
        /// <param name="timeLimit">The maximum duration of the recording.Allowed value is an integer greater than 0.</param>
        /// <param name="callbackUrl">A URL that will be requested when the recording ends, sending information about the recording. The longer the recording, the longer the delay in processing the recording and requesting the CallbackUrl. Url length is limited to 200 characters.</param>
        /// <param name="fileFormat">Specifies the file format of the recording. Allowed values are "mp3" or "wav" - any other value will default to "mp3".</param>
        /// <param name="trimSilence">Trims all silence from the beginning of the recording. Allowed values are "true" or "false" - any other value will default to "false".</param>
        /// <param name="transcribe">Specifies if this recording should be transcribed. Allowed values are "true" and "false" - all other values will default to "false".</param>
        /// <param name="transcribeQuality">Specifies the quality of the transcription. Allowed values are "auto" for automated transcriptions and "hybrid" for human-reviewed transcriptions - all other values will default to "auto".</param>
        /// <param name="transcribeCallback">A URL that will be requested when the call ends, sending information about the transcription. The longer the recording, the longer the delay in processing the transcription and requesting the TranscribeCallback. URL length is limited to 200 characters.</param>
        /// <returns>Returns created recording</returns>
        public Recording RecordCall(string callSid, bool record,
            RecordingAudioDirection direction = RecordingAudioDirection.BOTH, int? timeLimit = null,
            string callbackUrl = null, RecordingFileFormat fileFormat = RecordingFileFormat.MP3,
            bool trimSilence = false, bool transcribe = false,
            TranscribeQuality transcribeQuality = TranscribeQuality.AUTO,
            string transcribeCallback = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.RecordCall(accountSid, callSid, record, direction, timeLimit, callbackUrl, fileFormat,
                trimSilence,
                transcribe, transcribeQuality, transcribeCallback);
        }

        /// <summary>
        /// Deletes a recording
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="recordingSid">Recording SID.</param>
        /// <returns>Returns deleted recording</returns>
        public Recording DeleteRecording(string accountSid, string recordingSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/Recordings/{recordingSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Recording>(response);
        }

        /// <summary>
        /// Deletes a recording. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="recordingSid">Recording SID.</param>
        /// <returns>Returns deleted recording</returns>
        public Recording DeleteRecording(string recordingSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteRecording(accountSid, recordingSid);
        }

        /// <summary>
        /// Sets the parameters for list recordings.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="callSid">The call sid.</param>
        /// <param name="dateCreatedGte">The date created gte.</param>
        /// <param name="dateCreatedLt">The date created lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListRecordings(IRestRequest request, string callSid, DateTime dateCreatedGte,
            DateTime dateCreatedLt, int? page, int? pageSize)
        {
            if (callSid.HasValue()) request.AddQueryParameter("CallSid", callSid);
            if (dateCreatedGte != default(DateTime))
                request.AddQueryParameter("DateCreated>", dateCreatedGte.ToString("yyyy-MM-dd"));
            if (dateCreatedLt != default(DateTime))
                request.AddQueryParameter("DateCreated<", dateCreatedLt.ToString("yyyy-MM-dd"));
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }

        /// <summary>
        /// Sets the parameters for record call.
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
        private void SetParamsForRecordCall(IRestRequest request, bool record, RecordingAudioDirection direction,
            int? timeLimit, string callbackUrl, RecordingFileFormat fileFormat, bool trimSilence, bool transcribe,
            TranscribeQuality transcribeQuality, string transcribeCallback)
        {
            request.AddParameter("Record", record);
            request.AddParameter("Direction", EnumHelper.GetEnumValue(direction));
            if (timeLimit != null) request.AddParameter("TimeLimit", timeLimit);
            if (callbackUrl.HasValue()) request.AddParameter("CallbackUrl", callbackUrl);
            request.AddParameter("FileFormat", EnumHelper.GetEnumValue(fileFormat));
            request.AddParameter("TrimSilence", trimSilence);
            request.AddParameter("Transcribe", transcribe);
            request.AddParameter("TranscribeQuality", EnumHelper.GetEnumValue(transcribeQuality));
            if (transcribeCallback.HasValue()) request.AddParameter("TranscribeCallback", transcribeCallback);
        }
    }
}