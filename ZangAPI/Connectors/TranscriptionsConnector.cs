using System;
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
    /// Transcriptions connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class TranscriptionsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TranscriptionsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public TranscriptionsConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Views the transcription.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="transcriptionSid">The transcription sid.</param>
        /// <returns>Returns transcription</returns>
        public Transcription ViewTranscription(string accountSid, string transcriptionSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Transcriptions/{transcriptionSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Transcription>(response);
        }

        /// <summary>
        /// Views the transcription. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="transcriptionSid">The transcription sid.</param>
        /// <returns>Returns transcription</returns>
        public Transcription ViewTranscription(string transcriptionSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewTranscription(accountSid, transcriptionSid);
        }

        /// <summary>
        /// Lists the transcriptions.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="status">The status.</param>
        /// <param name="dateTranscribedGte">The date transcribed gte.</param>
        /// <param name="dateTranscribedLt">The date transcribed lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns transcription list</returns>
        public TranscriptionList ListTranscriptions(string accountSid, TranscriptionStatus? status = null, 
            DateTime dateTranscribedGte = default(DateTime), DateTime dateTranscribedLt = default(DateTime), 
            int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Transcriptions.json");

            // Add ListTranscriptions query and body parameters
            this.SetParamsForListTranscriptions(request, status, dateTranscribedGte, dateTranscribedLt, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<TranscriptionList>(response);
        }

        /// <summary>
        /// Lists the transcriptions. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="dateTranscribedGte">The date transcribed gte.</param>
        /// <param name="dateTranscribedLt">The date transcribed lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns transcription list</returns>
        public TranscriptionList ListTranscriptions(TranscriptionStatus? status = null,
            DateTime dateTranscribedGte = default(DateTime), DateTime dateTranscribedLt = default(DateTime),
            int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListTranscriptions(accountSid, status, dateTranscribedGte, dateTranscribedLt, page, pageSize);
        }

        /// <summary>
        /// Transcribes the recording.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="recordingSid">The recording sid.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <param name="callbackMethod">The callback method.</param>
        /// <param name="sliceStart">The slice start.</param>
        /// <param name="sliceDuration">Duration of the slice.</param>
        /// <param name="quality">The quality.</param>
        /// <returns>Returns transcription</returns>
        public Transcription TranscribeRecording(string accountSid, string recordingSid, string transcribeCallback = null, HttpMethod callbackMethod = HttpMethod.POST, int? sliceStart = null,
            int? sliceDuration = null, TranscribeQuality quality = TranscribeQuality.AUTO)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Transcriptions.json");

            // Add TranscribeRecording query and body parameters
            this.SetParamsForTranscribeRecordingOrAudioUrl(request, transcribeCallback, callbackMethod, sliceStart, sliceDuration, quality);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Transcription>(response);
        }

        /// <summary>
        /// Transcribes the recording. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="recordingSid">The recording sid.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <param name="callbackMethod">The callback method.</param>
        /// <param name="sliceStart">The slice start.</param>
        /// <param name="sliceDuration">Duration of the slice.</param>
        /// <param name="quality">The quality.</param>
        /// <returns>Returns transcription</returns>
        public Transcription TranscribeRecording(string recordingSid, string transcribeCallback = null, HttpMethod callbackMethod = HttpMethod.POST, int? sliceStart = null,
            int? sliceDuration = null, TranscribeQuality quality = TranscribeQuality.AUTO)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.TranscribeRecording(accountSid, recordingSid, transcribeCallback, callbackMethod, sliceStart, sliceDuration, quality);
        }

        /// <summary>
        /// Transcribes the audio URL.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="audioUrl">The audio URL.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <param name="callbackMethod">The callback method.</param>
        /// <param name="sliceStart">The slice start.</param>
        /// <param name="sliceDuration">Duration of the slice.</param>
        /// <param name="quality">The quality.</param>
        /// <returns>Returns transcription</returns>
        public Transcription TranscribeAudioUrl(string accountSid, string audioUrl = null, string transcribeCallback = null, HttpMethod callbackMethod = HttpMethod.POST, int? sliceStart = null,
            int? sliceDuration = null, TranscribeQuality quality = TranscribeQuality.AUTO)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Applications.json");

            // Add TranscribeAudioUrl query and body parameters
            this.SetParamsForTranscribeRecordingOrAudioUrl(request, transcribeCallback, callbackMethod, sliceStart, sliceDuration, quality);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Transcription>(response);
        }

        /// <summary>
        /// Transcribes the audio URL. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="recordingSid">The recording sid.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <param name="callbackMethod">The callback method.</param>
        /// <param name="sliceStart">The slice start.</param>
        /// <param name="sliceDuration">Duration of the slice.</param>
        /// <param name="quality">The quality.</param>
        /// <returns>Returns transcription</returns>
        public Transcription TranscribeAudioUrl(string recordingSid, string transcribeCallback = null, HttpMethod callbackMethod = HttpMethod.POST, int? sliceStart = null,
            int? sliceDuration = null, TranscribeQuality quality = TranscribeQuality.AUTO)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.TranscribeAudioUrl(accountSid, recordingSid, transcribeCallback, callbackMethod, sliceStart, sliceDuration, quality);
        }

        /// <summary>
        /// Sets the parameters for list transcriptions.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="status">The status.</param>
        /// <param name="dateTranscribedGte">The date transcribed gte.</param>
        /// <param name="dateTranscribedLt">The date transcribed lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListTranscriptions(IRestRequest request, TranscriptionStatus? status,
            DateTime dateTranscribedGte, DateTime dateTranscribedLt, int? page, int? pageSize)
        {
            if (status != null)
                request.AddQueryParameter("Status", EnumHelper.GetEnumValue(status));
            if (dateTranscribedGte != default(DateTime))
                request.AddQueryParameter("DateTranscribed>", dateTranscribedGte.ToString("yyyy-MM-dd"));
            if (dateTranscribedLt != default(DateTime))
                request.AddQueryParameter("DateTranscribed<", dateTranscribedLt.ToString("yyyy-MM-dd"));
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }

        /// <summary>
        /// Sets the parameters for transcribe recording.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="transcribeCallback">The transcribe callback.</param>
        /// <param name="callbackMethod">The callback method.</param>
        /// <param name="sliceStart">The slice start.</param>
        /// <param name="sliceDuration">Duration of the slice.</param>
        /// <param name="quality">The quality.</param>
        private void SetParamsForTranscribeRecordingOrAudioUrl(IRestRequest request, string transcribeCallback, HttpMethod callbackMethod, int? sliceStart,
            int? sliceDuration, TranscribeQuality quality)
        {
            if (transcribeCallback.HasValue()) request.AddParameter("TranscribeCallback", transcribeCallback);
            request.AddParameter("CallbackMethod", callbackMethod);
            if (sliceStart != null) request.AddParameter("SliceStart", sliceStart.ToString());
            if (sliceDuration != null) request.AddParameter("SliceDuration", sliceDuration);
            request.AddParameter("VoiceFallbackMethod", EnumHelper.GetEnumValue(quality));
        }
    }
}
