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
    public class IncomingPhoneNumbersConnectorTests
    {
        private const int Port = Configuration.Port;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.incomingPhoneNumber.json";
        private const string ResponseListJsonFileName = "Responses.incomingPhoneNumberList.json";
        private const string TestGroupName = "IncomingPhoneNumbersTest";

        [TestMethod]
        public void IncomingPhoneNumbersConnectorViewIncomingPhoneNumberTest()
        {
            const string methodName = "viewIncomingPhoneNumber";

            using (
                new MockServer(Port, "/Accounts/{accountSid}/IncomingPhoneNumbers/{incomingNumberSid}.json",
                    (req, rsp, prm) =>
                    {
                        // Check http method
                        if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                        // Check parameter equality
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

                // View incoming phone number using incoming phone numbers connector
                var phoneNumber = service.IncomingPhoneNumbersConnector.ViewIncomingNumber("TestIncomingPhoneNumberSid");

                Assert.AreEqual("(989) 494-5633", phoneNumber.FriendlyName);
                Assert.AreEqual(IncomingPhoneNumberType.LOCAL, phoneNumber.Type);
            }
        }

        [TestMethod]
        public void IncomingPhoneNumbersConnectorListIncomingPhoneNumbersTest()
        {
            const string methodName = "listIncomingPhoneNumbers";

            using (new MockServer(Port, "Accounts/{accountSid}/IncomingPhoneNumbers.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
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

                // List View incoming phone numbers using incoming phone numbers connector
                var phoneNumbersList =
                    service.IncomingPhoneNumbersConnector.ListIncomingNumbers(jsonRequest.QueryParameter("Contains"),
                        jsonRequest.QueryParameter("FriendlyName"), Convert.ToInt32(jsonRequest.QueryParameter("Page")),
                        Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("PageSize")), phoneNumbersList.Pagesize);
                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Page")), phoneNumbersList.Page);

                Assert.AreEqual(1, phoneNumbersList.Total);

                var phoneNumber = phoneNumbersList.Elements.First();

                Assert.AreEqual("(989) 494-5633", phoneNumber.FriendlyName);
                Assert.AreEqual(IncomingPhoneNumberType.LOCAL, phoneNumber.Type);
            }
        }

        [TestMethod]
        public void IncomingPhoneNumbersConnectorPurchaseIncomingNumberTest()
        {
            const string methodName = "purchaseIncomingPhoneNumber";

            using (new MockServer(Port, "/Accounts/{accountSid}/IncomingPhoneNumbers.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
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

                // Purchase incoming phone number using incoming phone numbers connector
                var phoneNumber = service.IncomingPhoneNumbersConnector.PurchaseIncomingNumber("TestAccountSid", 
                    jsonRequest.BodyParameter("PhoneNumber"), jsonRequest.BodyParameter("AreaCode"), jsonRequest.BodyParameter("FriendlyName"), 
                    jsonRequest.BodyParameter("VoiceUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceMethod")),
                    jsonRequest.BodyParameter("VoiceFallbackUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceFallbackMethod")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("VoiceCallerIdLookup")),
                    jsonRequest.BodyParameter("SmsUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("SmsMethod")),
                    jsonRequest.BodyParameter("SmsFallbackUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("SmsFallbackMethod")),
                    jsonRequest.BodyParameter("HeartbeatUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HeartbeatMethod")),
                    jsonRequest.BodyParameter("StatusCallback"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("StatusCallbackMethod")),
                    jsonRequest.BodyParameter("HangupCallback"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HangupCallbackMethod")),
                    jsonRequest.BodyParameter("VoiceApplicationSid"), jsonRequest.BodyParameter("SmsApplicationSid"));

                Assert.AreEqual("(989) 494-5633", phoneNumber.FriendlyName);
                Assert.AreEqual(jsonRequest.BodyParameter("PhoneNumber"), phoneNumber.PhoneNumber);
                Assert.AreEqual(jsonRequest.BodyParameter("SmsUrl"), phoneNumber.SmsUrl);
            }
        }

        [TestMethod]
        public void IncomingPhoneNumbersConnectorUpdateIncomingNumberTest()
        {
            const string methodName = "updateIncomingPhoneNumber";

            using (new MockServer(Port, "/Accounts/{accountSid}/IncomingPhoneNumbers/{incomingPhoneNumberSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
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

                // Update incoming phone number application using applications connector
                var phoneNumber = service.IncomingPhoneNumbersConnector.UpdateIncomingNumber("TestAccountSid", "TestIncomingPhoneNumberSid",
                    jsonRequest.BodyParameter("FriendlyName"),
                    jsonRequest.BodyParameter("VoiceUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceMethod")),
                    jsonRequest.BodyParameter("VoiceFallbackUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceFallbackMethod")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("VoiceCallerIdLookup")),
                    jsonRequest.BodyParameter("SmsUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("SmsMethod")),
                    jsonRequest.BodyParameter("SmsFallbackUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("SmsFallbackMethod")),
                    jsonRequest.BodyParameter("HeartbeatUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HeartbeatMethod")),
                    jsonRequest.BodyParameter("StatusCallback"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("StatusCallbackMethod")),
                    jsonRequest.BodyParameter("HangupCallback"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HangupCallbackMethod")));

                Assert.AreEqual("(989) 494-5633", phoneNumber.FriendlyName);
                Assert.AreEqual(Convert.ToBoolean(jsonRequest.BodyParameter("VoiceCallerIdLookup")), phoneNumber.VoiceCallerIdLookup);
            }
        }

        [TestMethod]
        public void IncomingPhoneNumbersConnectorDeleteIncomingNumberTest()
        {
            const string methodName = "deleteIncomingPhoneNumber";

            using (new MockServer(Port, "/Accounts/{accountSid}/IncomingPhoneNumbers/{incomingPhoneNumberSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("DELETE")) throw new ArgumentException();

                // Check parameter equality
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

                // Delete incoming phone number application using applications connector
                var phoneNumber = service.IncomingPhoneNumbersConnector.DeleteIncomingNumber("TestIncomingPhoneNumberSid");

                Assert.AreEqual("(989) 494-5633", phoneNumber.FriendlyName);
                Assert.AreEqual(IncomingPhoneNumberType.LOCAL, phoneNumber.Type);
            }
        }
    }
}
