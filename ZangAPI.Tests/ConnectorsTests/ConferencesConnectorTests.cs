using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Tests.ConnectorsTests
{
    [TestClass]
    public class ConferencesConnectorTests
    {
        private const int Port = Configuration.Port;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileNameConference = "Responses.conference.json";
        private const string ResponseListJsonFileNameConference = "Responses.conferenceList.json";
        private const string ResponseJsonFileNameParticipant = "Responses.participant.json";
        private const string ResponseListJsonFileNameParticipant = "Responses.participantList.json";
        private const string TestGroupName = "ConferencesTest";

        [TestMethod]
        public void ConferencesConnectorViewConferenceTest()
        {
            const string methodName = "viewConference";

            using (new MockServer(Port, "Accounts/{accountSid}/Conferences/{conferenceSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(
                        assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileNameConference}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new APIConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new CPaaSService(configuration);

                // View conference using conferences connector
                var conference = service.ConferencesConnector.ViewConference("TestConferenceSid");

                Assert.AreEqual("TestConferenceSid", conference.Sid);
                Assert.AreEqual(ConferenceStatus.COMPLETED, conference.Status);
                Assert.AreEqual("TestConference", conference.FriendlyName);
            }
        }

        [TestMethod]
        public void ConferencesConnectorListConferencesTest()
        {
            const string methodName = "listConferences";

            using (new MockServer(Port, "Accounts/{accountSid}/Conferences.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(
                        assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseListJsonFileNameConference}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new APIConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new CPaaSService(configuration);

                // List conferences using conferences connector
                var conferenceList = service.ConferencesConnector.ListConferences("TestConference",
                    jsonRequest.QueryParameter("FriendlyName"),
                    EnumHelper.ParseEnum<ConferenceStatus>(jsonRequest.QueryParameter("Status")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("DateCreated>")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("DateCreated<")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("DateUpdated>")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("DateUpdated<")),
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")),
                    Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(2, conferenceList.Total);

                var conference = conferenceList.Elements.First();

                Assert.AreEqual("TestConferenceSid", conference.Sid);
                Assert.AreEqual(ConferenceStatus.COMPLETED, conference.Status);
                Assert.AreEqual("TestConference", conference.FriendlyName);
            }
        }

        [TestMethod]
        public void ConferencesConnectorViewParticipantTest()
        {
            const string methodName = "viewParticipant";

            using (new MockServer(Port, "Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(
                        assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileNameParticipant}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new APIConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new CPaaSService(configuration);

                // View participant using conferences connector
                var participant = service.ConferencesConnector.ViewParticipant("TestConferenceSid", "TestParticipantSid");

                Assert.AreEqual("TestParticipantSid", participant.Sid);
                Assert.AreEqual("TestConferenceSid", participant.ConferenceSid);
                Assert.IsFalse(participant.Muted);
            }
        }

        [TestMethod]
        public void ConferencesConnectorListParticipantsTest()
        {
            const string methodName = "listParticipants";

            using (new MockServer(Port, "Accounts/{accountSid}/Conferences/{conferenceSid}/Participants.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(
                        assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseListJsonFileNameParticipant}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new APIConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new CPaaSService(configuration);

                // List participants using conferences connector
                var participantList = service.ConferencesConnector.ListParticipants("TestConferenceSid",
                    Convert.ToBoolean(jsonRequest.QueryParameter("Muted")), Convert.ToBoolean(jsonRequest.QueryParameter("Deaf")),
                     Convert.ToInt32(jsonRequest.QueryParameter("Page")),
                    Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(1, participantList.Total);

                var participant = participantList.Elements.First();

                Assert.AreEqual("TestParticipantSid", participant.Sid);
                Assert.AreEqual("TestConferenceSid", participant.ConferenceSid);
                Assert.IsFalse(participant.Muted);
            }
        }

        [TestMethod]
        public void ConferencesConnectorMuteOrDeafParticipantTest()
        {
            const string methodName = "muteDeafParticipant";

            using (new MockServer(Port, "Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(
                        assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileNameParticipant}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new APIConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new CPaaSService(configuration);

                // View participant using conferences connector
                var participant = service.ConferencesConnector.MuteOrDeafParticipant("TestConferenceSid", "TestParticipantSid",
                    Convert.ToBoolean(jsonRequest.BodyParameter("Muted")), Convert.ToBoolean(jsonRequest.BodyParameter("Deaf")));

                Assert.AreEqual("TestParticipantSid", participant.Sid);
                Assert.AreEqual("TestConferenceSid", participant.ConferenceSid);
                Assert.IsFalse(participant.Muted);
            }
        }

        [TestMethod]
        public void ConferencesConnectorPlayAudioToParticipantTest()
        {
            const string methodName = "playAudioToParticipant";

            using (new MockServer(Port, "Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(
                        assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileNameParticipant}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new APIConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new CPaaSService(configuration);

                // View participant using conferences connector
                var participant = service.ConferencesConnector.PlayAudioToParticipant("TestConferenceSid", "TestParticipantSid",
                    jsonRequest.BodyParameter("AudioUrl"));

                Assert.AreEqual("TestParticipantSid", participant.Sid);
                Assert.AreEqual("TestConferenceSid", participant.ConferenceSid);
                Assert.IsFalse(participant.Muted);
            }
        }

        [TestMethod]
        public void ConferencesConnectorHangupParticipantTest()
        {
            const string methodName = "hangupParticipant";

            using (new MockServer(Port, "Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("DELETE")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(
                        assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileNameParticipant}"));
                var json = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(json);
                rsp.Content(buffer);
            }))
            {
                // Create configuration
                var configuration = new APIConfiguration(AccountSid, AuthToken);
                configuration.BaseUrl = $"http://localhost:{Port}/";

                // Get json request from json file
                var jsonRequest = ParametersHelper.GetJsonRequestByGroupAndMethod(TestGroupName, methodName);

                // Create service
                var service = new CPaaSService(configuration);

                // View participant using conferences connector
                var participant = service.ConferencesConnector.HangupParticipant("TestConferenceSid", "TestParticipantSid");

                Assert.AreEqual("TestParticipantSid", participant.Sid);
                Assert.AreEqual("TestConferenceSid", participant.ConferenceSid);
                Assert.IsFalse(participant.Muted);
            }
        }
    }
}