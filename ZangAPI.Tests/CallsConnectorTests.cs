using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using ZangAPI.Configuration;
using ZangAPI.Model.Enums;

namespace ZangAPI.Tests
{
    /// <summary>
    /// Tests for calls connector
    /// </summary>
    [TestClass]
    public class CallsConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "call.json";
        private const string ResponseListJsonFileName = "callList.json";

        [TestMethod]
        public void CallsConnectorMakeCall()
        {
            var jsonFileName = "makeCall.json";

            using (new MockServer(Port, "Accounts/{accountSid}/Calls.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals(HttpMethod.POST)) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(jsonFileName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(jsonFileName, "queryParams", req);

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
                var jsonRequest = ParametersHelper.GetJsonRequestFromFile(jsonFileName);

                // Create service
                var service = new ZangService(configuration);

                //// Send sms using sms connector
                //var message = service.CallsConnector.MakeCall(jsonRequest.BodyParameter("To"), jsonRequest.BodyParameter("From"), jsonRequest.BodyParameter("Url"), jsonRequest.BodyParameter("Method"),
                //    jsonRequest.BodyParameter("FallbackUrl"), jsonRequest.BodyParameter("FallbackMethod"), jsonRequest.BodyParameter("StatusCallback"), jsonRequest.BodyParameter("StatusCallbackMethod"), jsonRequest.BodyParameter("HeartbeatUrl"),
                //    jsonRequest.BodyParameter("HeartbeatMethod"), jsonRequest.BodyParameter("ForwardedFrom"), jsonRequest.BodyParameter("PlayDtmf"), jsonRequest.BodyParameter("Timeout"), jsonRequest.BodyParameter("HideCallerId"),
                //    Convert.ToBoolean(jsonRequest.BodyParameter("Record")), jsonRequest.BodyParameter("RecordCallback"), jsonRequest.BodyParameter("RecordCallbackMethod"), jsonRequest.BodyParameter("Transcribe"), 
                //    jsonRequest.BodyParameter("TranscribeCallback"), jsonRequest.BodyParameter("StraightToVoicemail"),
                //    jsonRequest.BodyParameter("IfMachine"), jsonRequest.BodyParameter("IfMachineUrl"), jsonRequest.BodyParameter("IfMachineMethod"), jsonRequest.BodyParameter("SipAuthUsername"), jsonRequest.BodyParameter("SipAuthPassword"));

                //todo asserts
            }
        }
    }
}
