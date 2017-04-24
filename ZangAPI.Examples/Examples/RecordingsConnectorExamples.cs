using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;
using ZangAPI.Model.Enums;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Zang service to work with recordings
    /// </summary>
    public class RecordingsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing recording
        /// </summary>
        public void ViewRecording()
        {
            try
            {
                // View recording using recordings connector
                var recording = service.RecordingsConnector.ViewRecording("TestRecordingSid");
                Console.WriteLine(recording.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing recordings
        /// </summary>
        public void ListRecordings()
        {
            try
            {
                // List recordings using recordings connector
                var recordings = service.RecordingsConnector.ListRecordings("TestCallSid", new DateTime(2017, 1, 15), new DateTime(2017, 2, 15));
                Console.WriteLine(recordings.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of recording call
        /// </summary>
        public void RecordCall()
        {
            try
            {
                // Record call using recordings connector
                var recording = service.RecordingsConnector.RecordCall("TestCallSid", true, fileFormat:RecordingFileFormat.WAV, trimSilence:true);
                Console.WriteLine(recording.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting recording
        /// </summary>
        public void DeleteRecording()
        {
            try
            {
                // Delete recording using recordings connector
                var recording = service.RecordingsConnector.DeleteRecording("TestRecordingSid");
                Console.WriteLine(recording.Sid);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
