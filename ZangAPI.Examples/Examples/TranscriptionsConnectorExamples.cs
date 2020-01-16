using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with transcriptions
    /// </summary>
    public class TranscriptionsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

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
            catch (CPaaSException e)
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
            catch (CPaaSException e)
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
            catch (CPaaSException e)
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
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
