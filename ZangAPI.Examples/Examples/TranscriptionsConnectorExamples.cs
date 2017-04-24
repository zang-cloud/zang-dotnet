using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;
using ZangAPI.Model.Enums;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Zang service to work with transcriptions
    /// </summary>
    public class TranscriptionsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing transcription
        /// </summary>
        public void ViewTranscription()
        {
            try
            {
                // View transcription using transcriptions connector
                var transcription = service.TranscriptionsConnector.ViewTranscription("TestTranscriptionSid");
                Console.WriteLine(transcription.Price);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing transcriptions
        /// </summary>
        public void ListTranscriptions()
        {
            try
            {
                // List transcriptions using transcriptions connector
                var transcriptions = service.TranscriptionsConnector.ListTranscriptions(TranscriptionStatus.IN_PROGRESS, page:3, pageSize:40);
                Console.WriteLine(transcriptions.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of transcribing recording
        /// </summary>
        public void TranscribeRecording()
        {
            try
            {
                // Transcribe recording using transcriptions connector
                var transcription = service.TranscriptionsConnector.TranscribeRecording("TestRecordingSid",
                    "transcribeCallback", HttpMethod.GET, 1, 40);
                Console.WriteLine(transcription.Price);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of transcribing audio url
        /// </summary>
        public void TranscribeAudioUrl()
        {
            try
            {
                // Transcribe audio url using transcriptions connector
                var transcription = service.TranscriptionsConnector.TranscribeAudioUrl("audioURl",
                    "transcribeCallback", HttpMethod.GET, 1, 40);
                Console.WriteLine(transcription.Price);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
