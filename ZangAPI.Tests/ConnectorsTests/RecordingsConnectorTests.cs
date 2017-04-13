using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using ZangAPI.Configuration;
using ZangAPI.Helpers;
using ZangAPI.Model.Enums;

namespace ZangAPI.Tests.ConnectorsTests
{
    [TestClass]
    public class RecordingsConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.recording.json";
        private const string ResponseListJsonFileName = "Responses.recordingList.json";
        private const string TestGroupName = "RecordingsTest";

        [TestMethod]
        public void RecordingsConnectorViewRecordingTest()
        {
            const string methodName = "viewRecording";

            using (new MockServer(Port, "/Accounts/{accountSid}/Recordings/{recordingSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileName}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new ZangConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new ZangService(configuration);

                // View recording using recordings connector
                var recording = service.RecordingsConnector.ViewRecording("TestRecordingSid");

                Assert.AreEqual("http://recordings.telapi.com/RBfcc94a3e2b5d4e4c89f7017d6387ffb8/RE4288908463cd614b41084509ad8893a7.mp3", recording.RecordingUrl);
                Assert.AreEqual(15, recording.Duration);
            }
        }

        [TestMethod]
        public void RecordingsConnectorListRecordingsTest()
        {
            const string methodName = "listRecordings";

            using (new MockServer(Port, "Accounts/{accountSid}/Recordings.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseListJsonFileName}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new ZangConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new ZangService(configuration);

                // List recordings using recordings connector
                var recordingsList = service.RecordingsConnector.ListRecordings("TestCallSid",
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("DateCreated>")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("DateCreated<")),
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")),
                    Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("PageSize")), recordingsList.Pagesize);
                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Page")), recordingsList.Page);
                Assert.AreEqual(1, recordingsList.Total);

                var recording = recordingsList.Elements.First();

                Assert.AreEqual("http://recordings.telapi.com/RBfcc94a3e2b5d4e4c89f7017d6387ffb8/RE4288908463cd614b41084509ad8893a7.mp3", recording.RecordingUrl);
                Assert.AreEqual(15, recording.Duration);
            }
        }

        [TestMethod]
        public void RecordingsConnectorRecordCallTest()
        {
            const string methodName = "recordCall";

            using (new MockServer(Port, "/Accounts/{accountSid}/Calls/{callSid}/Recordings.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileName}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new ZangConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new ZangService(configuration);

                // Record call using recordings connector
                var recording = service.RecordingsConnector.RecordCall("TestCallSid",
                    Convert.ToBoolean(jsonRequest.BodyParameter("Record")), EnumHelper.ParseEnum<RecordingAudioDirection>(jsonRequest.BodyParameter("Direction")), 
                    Convert.ToInt32(jsonRequest.BodyParameter("TimeLimit")), jsonRequest.BodyParameter("CallbackUrl"), 
                    EnumHelper.ParseEnum<RecordingFileFormat>(jsonRequest.BodyParameter("FileFormat")), Convert.ToBoolean(jsonRequest.BodyParameter("TrimSilence")), 
                    Convert.ToBoolean(jsonRequest.BodyParameter("Transcribe")), EnumHelper.ParseEnum<TranscribeQuality>(jsonRequest.BodyParameter("TranscribeQuality")),
                    jsonRequest.BodyParameter("TranscribeCallback"));

                Assert.AreEqual("http://recordings.telapi.com/RBfcc94a3e2b5d4e4c89f7017d6387ffb8/RE4288908463cd614b41084509ad8893a7.mp3", recording.RecordingUrl);
                Assert.AreEqual(15, recording.Duration);
            }
        }

        [TestMethod]
        public void RecordingsConnectorDeleteRecordingTest()
        {
            const string methodName = "deleteRecording";

            using (new MockServer(Port, "/Accounts/{accountSid}/Recordings/{recordingSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("DELETE")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileName}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new ZangConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new ZangService(configuration);

                // Delete recording using recordings connector
                var recording = service.RecordingsConnector.DeleteRecording("TestRecordingSid");

                Assert.AreEqual("http://recordings.telapi.com/RBfcc94a3e2b5d4e4c89f7017d6387ffb8/RE4288908463cd614b41084509ad8893a7.mp3", recording.RecordingUrl);
            }
        }
    }
}
