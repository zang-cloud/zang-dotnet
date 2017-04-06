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
    public class TranscriptionsConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.transcription.json";
        private const string ResponseListJsonFileName = "Responses.transcriptionList.json";
        private const string TestGroupName = "TranscriptionsTest";

        [TestMethod]
        public void TranscriptionsConnectorViewTranscriptionTest()
        {
            const string methodName = "viewTranscription";

            using (new MockServer(Port, "/Accounts/{accountSid}/Transcriptions/{transcriptionSid}", (req, rsp, prm) =>
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
                var smsJson = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(smsJson);
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

                // View transcription using transcriptions connector
                var transcription = service.TranscriptionsConnector.ViewTranscription("TestTranscriptionSid");

                Assert.AreEqual("http://dev.calyx.hr/~vprenner/zang.mp3", transcription.AudioUrl);
                Assert.AreEqual(TranscriptionStatus.COMPLETED, transcription.Status);
                Assert.AreEqual(50, transcription.Duration);
            }
        }

        [TestMethod]
        public void TranscriptionsConnectorListTranscriptionsTest()
        {
            const string methodName = "listTranscriptions";

            using (new MockServer(Port, "Accounts/{accountSid}/Transcriptions.json", (req, rsp, prm) =>
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
                var smsJson = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(smsJson);
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

                // List transcriptions using transcriptions connector
                var transcriptionsList = service.TranscriptionsConnector.ListTranscriptions(
                    EnumHelper.ParseEnum<TranscriptionStatus>(jsonRequest.QueryParameter("Status")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("DateTranscribed>")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("DateTranscribed<")),
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")), Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("PageSize")), transcriptionsList.Pagesize);
                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Page")), transcriptionsList.Page);
                Assert.AreEqual(2, transcriptionsList.Total);

                var transcription = transcriptionsList.Elements.First();

                Assert.AreEqual("http://dev.calyx.hr/~vprenner/zang.mp3", transcription.AudioUrl);
                Assert.AreEqual(TranscriptionStatus.COMPLETED, transcription.Status);
                Assert.AreEqual(50, transcription.Duration);
            }
        }

        [TestMethod]
        public void TranscriptionsConnectorTranscribeRecordingTest()
        {
            const string methodName = "transcribeRecording";

            using (new MockServer(Port, "/Accounts/{accountSid}/Transcriptions.json", (req, rsp, prm) =>
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
                var smsJson = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(smsJson);
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

                // Transcribe recording using transcriptions connector
                var transcription = service.TranscriptionsConnector.TranscribeRecording("TestRecordingSid",
                    jsonRequest.BodyParameter("TranscribeCallback"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("CallbackMethod")),
                    Convert.ToInt32(jsonRequest.BodyParameter("SliceStart")),
                    Convert.ToInt32(jsonRequest.BodyParameter("SliceDuration")),
                    EnumHelper.ParseEnum<TranscribeQuality>(jsonRequest.BodyParameter("Quality")));

                Assert.AreEqual("http://dev.calyx.hr/~vprenner/zang.mp3", transcription.AudioUrl);
                Assert.AreEqual(TranscriptionStatus.COMPLETED, transcription.Status);
                Assert.AreEqual(50, transcription.Duration);
            }
        }

        [TestMethod]
        public void TranscriptionsConnectorTranscribeAudioUrlTest()
        {
            const string methodName = "transcribeAudioUrl";

            using (new MockServer(Port, "/Accounts/{accountSid}/Transcriptions.json", (req, rsp, prm) =>
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
                var smsJson = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(smsJson);
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

                // Transcribe audio url using transcriptions connector
                var transcription = service.TranscriptionsConnector.TranscribeAudioUrl(
                    jsonRequest.BodyParameter("AudioUrl"),
                    jsonRequest.BodyParameter("TranscribeCallback"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("CallbackMethod")),
                    Convert.ToInt32(jsonRequest.BodyParameter("SliceStart")),
                    Convert.ToInt32(jsonRequest.BodyParameter("SliceDuration")),
                    EnumHelper.ParseEnum<TranscribeQuality>(jsonRequest.BodyParameter("Quality")));

                Assert.AreEqual("http://dev.calyx.hr/~vprenner/zang.mp3", transcription.AudioUrl);
                Assert.AreEqual(TranscriptionStatus.COMPLETED, transcription.Status);
                Assert.AreEqual(50, transcription.Duration);
            }
        }
    }
}