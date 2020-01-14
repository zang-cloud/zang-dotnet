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
    /// <summary>
    /// Tests for calls connector
    /// </summary>
    [TestClass]
    public class CallsConnectorTests
    {
        private const int Port = Configuration.Port;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.call.json";
        private const string ResponseListJsonFileName = "Responses.callList.json";
        private const string TestGroupName = "CallsTest";

        [TestMethod]
        public void CallsConnectorMakeCall()
        {
            const string methodName = "makeCall";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                //// Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileName}"));
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

                // Make call using calls connector
                var call = service.CallsConnector.MakeCall(jsonRequest.BodyParameter("To"),
                    jsonRequest.BodyParameter("From"), jsonRequest.BodyParameter("Url"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("Method")),
                    jsonRequest.BodyParameter("FallbackUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("FallbackMethod")),
                    jsonRequest.BodyParameter("StatusCallback"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("StatusCallbackMethod")),
                    jsonRequest.BodyParameter("HeartbeatUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HeartbeatMethod")),
                    jsonRequest.BodyParameter("ForwardedFrom"), jsonRequest.BodyParameter("PlayDtmf"),
                    Convert.ToInt32(jsonRequest.BodyParameter("Timeout")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("HideCallerId")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("Record")), jsonRequest.BodyParameter("RecordCallback"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("RecordCallbackMethod")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("Transcribe")),
                    jsonRequest.BodyParameter("TranscribeCallback"),
                    Convert.ToBoolean(jsonRequest.BodyParameter("StraightToVoicemail")),
                    EnumHelper.ParseEnum<IfMachine>(jsonRequest.BodyParameter("IfMachine")),
                    jsonRequest.BodyParameter("IfMachineUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("IfMachineMethod")),
                    jsonRequest.BodyParameter("SipAuthUsername"), jsonRequest.BodyParameter("SipAuthPassword")
                    );

                Assert.AreEqual(jsonRequest.BodyParameter("To"), call.To);
                Assert.AreEqual(AnsweredBy.TBD, call.AnsweredBy);
                Assert.AreEqual(Convert.ToDecimal(0.1872), call.Price);
                Assert.AreEqual(CallStatus.COMPLETED, call.Status);
            }
        }

        [TestMethod]
        public void CallsConnectorViewCall()
        {
            const string methodName = "viewCall";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls/{callSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                //// Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileName}"));
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

                // Make call using calls connector
                var call = service.CallsConnector.ViewCall("TestCallSid");

                Assert.AreEqual(AnsweredBy.TBD, call.AnsweredBy);
                Assert.AreEqual(Convert.ToDecimal(0.1872), call.Price);
                Assert.AreEqual(CallStatus.COMPLETED, call.Status);
            }
        }

        [TestMethod]
        public void CallsConnectorListCalls()
        {
            const string methodName = "listCalls";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                //// Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseListJsonFileName}"));
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

                // List calls using calls connector
                var callList = service.CallsConnector.ListCalls(jsonRequest.QueryParameter("To"),
                    jsonRequest.QueryParameter("From"), EnumHelper.ParseEnum<CallStatus>(jsonRequest.QueryParameter("Status")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("StartTime>")),
                    ParametersHelper.GetDateFromString(jsonRequest.QueryParameter("StartTime<")), Convert.ToInt32(jsonRequest.QueryParameter("Page")),
                    Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(1, callList.Total);
                Assert.AreEqual(1, callList.Numpages);
                Assert.AreEqual(jsonRequest.QueryParameter("Page"), callList.Page.ToString());
                Assert.AreEqual(jsonRequest.QueryParameter("PageSize"), callList.Pagesize.ToString());

                var call = callList.Elements.First();

                Assert.AreEqual(jsonRequest.QueryParameter("To"), call.To);
                Assert.AreEqual(AnsweredBy.TBD, call.AnsweredBy);
                Assert.AreEqual(Convert.ToDecimal(0.1872), call.Price);
                Assert.AreEqual(CallStatus.COMPLETED, call.Status);
            }
        }

        [TestMethod]
        public void CallsConnectorInterruptLiveCall()
        {
            const string methodName = "interruptLiveCall";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls/{callSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                //// Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileName}"));
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

                // Interrupt live call using calls connector
                var call = service.CallsConnector.InterruptLiveCall("TestCallSid", jsonRequest.BodyParameter("Url"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("Method")),
                    EnumHelper.ParseEnum<CallStatus>(jsonRequest.BodyParameter("Status")));

                Assert.AreEqual(AnsweredBy.TBD, call.AnsweredBy);
                Assert.AreEqual(Convert.ToDecimal(0.1872), call.Price);
                Assert.AreEqual(CallStatus.COMPLETED, call.Status);
            }
        }

        [TestMethod]
        public void CallsConnectorSendDigitsToLiveCall()
        {
            const string methodName = "sendDigitsToLiveCall";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls/{callSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                //// Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileName}"));
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

                // Send digits to live call using calls connector
                var call = service.CallsConnector.SendDigitsToLiveCall("TestCallSid",
                    jsonRequest.BodyParameter("PlayDtmf"),
                    EnumHelper.ParseEnum<AudioDirection>(jsonRequest.BodyParameter("PlayDtmfDirection")));

                Assert.AreEqual(AnsweredBy.TBD, call.AnsweredBy);
                Assert.AreEqual(Convert.ToDecimal(0.1872), call.Price);
                Assert.AreEqual(CallStatus.COMPLETED, call.Status);
            }
        }

        [TestMethod]
        public void CallsConnectorRecordLiveCall()
        {
            const string methodName = "recordLiveCall";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls/{callSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                //// Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileName}"));
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

                // Record live call using calls connector
                var call = service.CallsConnector.RecordLiveCall("TestCallSid",
                    Convert.ToBoolean(jsonRequest.BodyParameter("Record")),
                    EnumHelper.ParseEnum<RecordingAudioDirection>(jsonRequest.BodyParameter("Direction")),
                        Convert.ToInt32(jsonRequest.BodyParameter("TimeLimit")),
                        jsonRequest.BodyParameter("CallbackUrl"),
                        EnumHelper.ParseEnum<RecordingFileFormat>(jsonRequest.BodyParameter("FileFormat")),
                        Convert.ToBoolean(jsonRequest.BodyParameter("TrimSilence")),
                        Convert.ToBoolean(jsonRequest.BodyParameter("Transcribe")),
                        EnumHelper.ParseEnum<TranscribeQuality>(jsonRequest.BodyParameter("TranscribeQuality")),
                        jsonRequest.BodyParameter("TranscribeCallback"));

                Assert.AreEqual(AnsweredBy.TBD, call.AnsweredBy);
                Assert.AreEqual(Convert.ToDecimal(0.1872), call.Price);
                Assert.AreEqual(CallStatus.COMPLETED, call.Status);
            }
        }

        [TestMethod]
        public void CallsConnectorPlayAudioToLiveCall()
        {
            const string methodName = "playAudioToLiveCall";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls/{callSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                //// Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileName}"));
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

                // Record live call using calls connector
                var call = service.CallsConnector.PlayAudioToLiveCall("TestCallSid",
                    jsonRequest.BodyParameter("AudioUrl"), EnumHelper.ParseEnum<RecordingAudioDirection>(jsonRequest.BodyParameter("Direction")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("Loop")));

                Assert.AreEqual(AnsweredBy.TBD, call.AnsweredBy);
                Assert.AreEqual(Convert.ToDecimal(0.1872), call.Price);
                Assert.AreEqual(CallStatus.COMPLETED, call.Status);
            }
        }

        [TestMethod]
        public void CallsConnectorApplyVoiceEffect()
        {
            const string methodName = "applyVoiceEffect";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls/{callSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                //// Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileName}"));
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

                // Record live call using calls connector
                var call = service.CallsConnector.ApplyVoiceEffect("TestCallSid",
                    EnumHelper.ParseEnum<AudioDirection>(jsonRequest.BodyParameter("AudioDirection")),
                    Convert.ToInt32(jsonRequest.BodyParameter("Pitch")),
                    Convert.ToInt32(jsonRequest.BodyParameter("PitchSemiTones")),
                    Convert.ToInt32(jsonRequest.BodyParameter("PitchOctaves")),
                    Convert.ToInt32(jsonRequest.BodyParameter("Rate")),
                    Convert.ToInt32(jsonRequest.BodyParameter("Tempo")));

                Assert.AreEqual(AnsweredBy.TBD, call.AnsweredBy);
                Assert.AreEqual(Convert.ToDecimal(0.1872), call.Price);
                Assert.AreEqual(CallStatus.COMPLETED, call.Status);
            }
        }
    }
}