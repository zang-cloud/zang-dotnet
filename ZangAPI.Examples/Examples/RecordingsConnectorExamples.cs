using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with recordings
    /// </summary>
    public class RecordingsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

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
            catch (CPaaSException e)
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
            catch (CPaaSException e)
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
            catch (CPaaSException e)
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
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
