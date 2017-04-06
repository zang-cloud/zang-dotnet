using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Recordings connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
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
        /// Lists the recordings.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="callSid">The call sid.</param>
        /// <param name="dateCreatedGte">The date created gte.</param>
        /// <param name="dateCreatedLt">The date created lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns recording list</returns>
        public RecordingList ListRecordings(string accountSid, string callSid = null, DateTime dateCreatedGte = default(DateTime), 
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

            return this.ReturnOrThrowException<RecordingList>(response);
        }

        /// <summary>
        /// Lists the recordings. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="callSid">The call sid.</param>
        /// <param name="dateCreatedGte">The date created gte.</param>
        /// <param name="dateCreatedLt">The date created lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns recording list</returns>
        public RecordingList ListRecordings(string callSid = null, DateTime dateCreatedGte = default(DateTime),
            DateTime dateCreatedLt = default(DateTime), int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListRecordings(accountSid, callSid, dateCreatedGte, dateCreatedLt, page, pageSize);
        }

        /// <summary>
        /// Deletes the recording.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="recordingSid">The recording sid.</param>
        /// <returns>Returns deleted recording</returns>
        public Recording DeleteRecording(string accountSid, string recordingSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE, $"Accounts/{accountSid}/Recordings/{recordingSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Recording>(response);
        }

        /// <summary>
        /// Deletes the recording. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="recordingSid">The recording sid.</param>
        /// <returns>Returns deleted recording</returns>
        public Recording DeleteRecording(string recordingSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteRecording(accountSid, recordingSid);
        }

        public Recording RecordCall(string accountSid, string callSid, bool record, RecordingAudioDirection direction = RecordingAudioDirection.BOTH, int? timeLimit = null,
            string callbackUrl = null, RecordingFileFormat fileFormat = RecordingFileFormat.MP3, bool trimSilence = false, bool transcribe = false, TranscribeQuality transcribeQuality = TranscribeQuality.AUTO,
            string transcribeCallback = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Calls/{callSid}/Recordings.json");

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
        /// Records the call. Uses {accountSid} from configuration in HttpProvider
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
        /// <returns>Returns recording</returns>
        public Recording RecordCall(string callSid, bool record, RecordingAudioDirection direction = RecordingAudioDirection.BOTH, int? timeLimit = null,
            string callbackUrl = null, RecordingFileFormat fileFormat = RecordingFileFormat.MP3, bool trimSilence = false, bool transcribe = false, TranscribeQuality transcribeQuality = TranscribeQuality.AUTO,
            string transcribeCallback = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.RecordCall(accountSid, callSid, record, direction, timeLimit, callbackUrl, fileFormat, trimSilence,
                transcribe, transcribeQuality, transcribeCallback);
        }

        /// <summary>
        /// Views the recording.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="recordingSid">The recording sid.</param>
        /// <returns>Returns recording</returns>
        public Recording ViewRecording(string accountSid, string recordingSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Recordings/{recordingSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Recording>(response);
        }

        /// <summary>
        /// Views the recording. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="recordingSid">The recording sid.</param>
        /// <returns>Returns recording</returns>
        public Recording ViewRecording(string recordingSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewRecording(accountSid, recordingSid);
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
        private void SetParamsForRecordCall(IRestRequest request, bool record, RecordingAudioDirection direction, int? timeLimit,
            string callbackUrl, RecordingFileFormat fileFormat, bool trimSilence, bool transcribe, TranscribeQuality transcribeQuality,
            string transcribeCallback)
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
