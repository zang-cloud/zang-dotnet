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
    /// Transcriptions connector - Used for all forms of communication with the Transcriptions endpoint of the Avaya CPaaS REST API
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
        /// Shows info on some transcription
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="transcriptionSid">Transcription SID.</param>
        /// <returns>Returns transcription</returns>
        public Transcription ViewTranscription(string accountSid, string transcriptionSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Transcriptions/{transcriptionSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Transcription>(response);
        }

        /// <summary>
        /// Shows info on some transcription. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="transcriptionSid">Transcription SID.</param>
        /// <returns>Returns transcription</returns>
        public Transcription ViewTranscription(string transcriptionSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewTranscription(accountSid, transcriptionSid);
        }

        /// <summary>
        /// Shows info on all transcriptions associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="status">Filter by transcriptions with a given status.Allowed values are "completed", "in-progress", and "failed".</param>
        /// <param name="dateTranscribedGte">Filter by date transcribed greater or equal than.</param>
        /// <param name="dateTranscribedLt">Filter by date transcribed less than.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns transcription list</returns>
        public TranscriptionsList ListTranscriptions(string accountSid, TranscriptionStatus? status = null,
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

            return this.ReturnOrThrowException<TranscriptionsList>(response);
        }

        /// <summary>
        /// Shows info on all transcriptions associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="status">Filter by transcriptions with a given status.Allowed values are "completed", "in-progress", and "failed".</param>
        /// <param name="dateTranscribedGte">Filter by date transcribed greater or equal than.</param>
        /// <param name="dateTranscribedLt">Filter by date transcribed less than.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns transcription list</returns>
        public TranscriptionsList ListTranscriptions(TranscriptionStatus? status = null,
            DateTime dateTranscribedGte = default(DateTime), DateTime dateTranscribedLt = default(DateTime),
            int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListTranscriptions(accountSid, status, dateTranscribedGte, dateTranscribedLt, page, pageSize);
        }

        /// <summary>
        /// Transcribes some recording
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="recordingSid">Recording SID.</param>
        /// <param name="transcribeCallback">The URL some parameters regarding the transcription will be passed to once it is completed. The longer the recording time, the longer the process delay in returning the transcription information. If no TranscribeCallback is given, the recording will still be saved to the system and available either in your Transcriptions Logs or via a REST List Transcriptions (ADD URL LINK) request. URL length is limited to 200 characters.</param>
        /// <param name="callbackMethod">The HTTP method used to request the TranscribeCallback. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="sliceStart">Start point for slice transcription(in seconds).</param>
        /// <param name="sliceDuration">Duration of slice transcription (in seconds).</param>
        /// <param name="quality">Specifies the transcription quality.Transcription price differs for each quality tier.See pricing page for details.Allowed values are "auto", "hybrid" and "keywords", where "auto" is a machine-generated transcription, "hybrid" is reviewed by a human for accuracy and "keywords" returns topics and keywords for given audio file.</param>
        /// <returns>Returns created transcription</returns>
        public Transcription TranscribeRecording(string accountSid, string recordingSid,
            string transcribeCallback = null, HttpMethod callbackMethod = HttpMethod.POST, int? sliceStart = null,
            int? sliceDuration = null, TranscribeQuality quality = TranscribeQuality.AUTO)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Transcriptions.json");

            // Add TranscribeRecording query and body parameters
            this.SetParamsForTranscribeRecordingOrAudioUrl(request, transcribeCallback, callbackMethod, sliceStart,
                sliceDuration, quality);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Transcription>(response);
        }

        /// <summary>
        /// Transcribes some recording. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="recordingSid">Recording SID.</param>
        /// <param name="transcribeCallback">The URL some parameters regarding the transcription will be passed to once it is completed. The longer the recording time, the longer the process delay in returning the transcription information. If no TranscribeCallback is given, the recording will still be saved to the system and available either in your Transcriptions Logs or via a REST List Transcriptions (ADD URL LINK) request. URL length is limited to 200 characters.</param>
        /// <param name="callbackMethod">The HTTP method used to request the TranscribeCallback. Valid parameters are GET and POST - any other value will default to POST.</param>
        /// <param name="sliceStart">Start point for slice transcription(in seconds).</param>
        /// <param name="sliceDuration">Duration of slice transcription (in seconds).</param>
        /// <param name="quality">Specifies the transcription quality.Transcription price differs for each quality tier.See pricing page for details.Allowed values are "auto", "hybrid" and "keywords", where "auto" is a machine-generated transcription, "hybrid" is reviewed by a human for accuracy and "keywords" returns topics and keywords for given audio file.</param>
        /// <returns>Returns created transcription</returns>
        public Transcription TranscribeRecording(string recordingSid, string transcribeCallback = null,
            HttpMethod callbackMethod = HttpMethod.POST, int? sliceStart = null,
            int? sliceDuration = null, TranscribeQuality quality = TranscribeQuality.AUTO)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.TranscribeRecording(accountSid, recordingSid, transcribeCallback, callbackMethod, sliceStart,
                sliceDuration, quality);
        }

        /// <summary>
        /// Transcribes an audio file on some URL
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="audioUrl">URL where the audio to be transcribed is located.</param>
        /// <param name="transcribeCallback">URL that will be requested when the transcription has finished processing.</param>
        /// <param name="callbackMethod">Specifies the HTTP method to use when requesting the TranscribeCallback URL. Allowed values are "POST" and "GET".</param>
        /// <param name="sliceStart">Start point for slice transcription (in seconds).</param>
        /// <param name="sliceDuration">Duration of slice transcription (in seconds).</param>
        /// <param name="quality">Specifies the transcription quality. Transcription price differs for each quality tier. See pricing page for details. Allowed values are "auto", "hybrid" and "keywords", where "auto" is a machine-generated transcription, "hybrid" is reviewed by a human for accuracy and "keywords" returns topics and keywords for given audio file.</param>
        /// <returns>Returns created transcription</returns>
        public Transcription TranscribeAudioUrl(string accountSid, string audioUrl = null,
            string transcribeCallback = null, HttpMethod callbackMethod = HttpMethod.POST, int? sliceStart = null,
            int? sliceDuration = null, TranscribeQuality quality = TranscribeQuality.AUTO)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Transcriptions.json");

            if (audioUrl.HasValue()) request.AddParameter("AudioUrl", audioUrl);

            // Add TranscribeAudioUrl query and body parameters
            this.SetParamsForTranscribeRecordingOrAudioUrl(request, transcribeCallback, callbackMethod, sliceStart,
                sliceDuration, quality);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Transcription>(response);
        }

        /// <summary>
        /// Transcribes an audio file on some URL
        /// </summary>
        /// <param name="audioUrl">URL where the audio to be transcribed is located.</param>
        /// <param name="transcribeCallback">URL that will be requested when the transcription has finished processing.</param>
        /// <param name="callbackMethod">Specifies the HTTP method to use when requesting the TranscribeCallback URL. Allowed values are "POST" and "GET".</param>
        /// <param name="sliceStart">Start point for slice transcription (in seconds).</param>
        /// <param name="sliceDuration">Duration of slice transcription (in seconds).</param>
        /// <param name="quality">Specifies the transcription quality. Transcription price differs for each quality tier. See pricing page for details. Allowed values are "auto", "hybrid" and "keywords", where "auto" is a machine-generated transcription, "hybrid" is reviewed by a human for accuracy and "keywords" returns topics and keywords for given audio file.</param>
        /// <returns>Returns created transcription</returns>
        public Transcription TranscribeAudioUrl(string audioUrl, string transcribeCallback = null,
            HttpMethod callbackMethod = HttpMethod.POST, int? sliceStart = null,
            int? sliceDuration = null, TranscribeQuality quality = TranscribeQuality.AUTO)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.TranscribeAudioUrl(accountSid, audioUrl, transcribeCallback, callbackMethod, sliceStart,
                sliceDuration, quality);
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
        private void SetParamsForTranscribeRecordingOrAudioUrl(IRestRequest request, string transcribeCallback,
            HttpMethod callbackMethod, int? sliceStart,
            int? sliceDuration, TranscribeQuality quality)
        {
            if (transcribeCallback.HasValue()) request.AddParameter("TranscribeCallback", transcribeCallback);
            request.AddParameter("CallbackMethod", callbackMethod);
            if (sliceStart != null) request.AddParameter("SliceStart", sliceStart.ToString());
            if (sliceDuration != null) request.AddParameter("SliceDuration", sliceDuration);
            request.AddParameter("Quality", EnumHelper.GetEnumValue(quality));
        }
    }
}