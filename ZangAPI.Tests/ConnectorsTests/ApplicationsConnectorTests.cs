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
    public class ApplicationsConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.application.json";
        private const string ResponseListJsonFileName = "Responses.applicationList.json";
        private const string TestGroupName = "ApplicationsTest";

        [TestMethod]
        public void ApplicationsConnectorViewApplicationTest()
        {
            const string methodName = "viewApplication";

            using (new MockServer(Port, "/Accounts/{accountSid}/Applications/{applicationSid}.json", (req, rsp, prm) =>
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

                // View application using applications connector
                var application = service.ApplicationsConnector.ViewApplication("TestApplicationSid");

                Assert.AreEqual("TestApplicationSid", application.Sid);
                Assert.AreEqual("TestApplication", application.FriendlyName);
            }
        }

        [TestMethod]
        public void ApplicationsConnectorListApplicationsTest()
        {
            const string methodName = "listApplications";

            using (new MockServer(Port, "Accounts/{accountSid}/Applications.json", (req, rsp, prm) =>
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

                // List applications using applications connector
                var applicationList = service.ApplicationsConnector.ListApplications("TestAccountSid", "TestApplication",
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")), Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(1, applicationList.Total);

                var application = applicationList.Elements.First();

                Assert.AreEqual("TestApplicationSid", application.Sid);
                Assert.AreEqual("TestApplication", application.FriendlyName);
            }
        }

        [TestMethod]
        public void ApplicationsConnectorCreateApplicationTest()
        {
            const string methodName = "createApplication";

            using (new MockServer(Port, "/Accounts/{accountSid}/Applications.json", (req, rsp, prm) =>
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

                // Create application using applications connector
                var application = service.ApplicationsConnector.CreateApplication("TestAccountSid", "TestApplication",
                    jsonRequest.BodyParameter("VoiceUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceMethod")),
                    jsonRequest.BodyParameter("VoiceFallbackUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceFallbackMethod")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("VoiceCallerIdLookup")),
                    jsonRequest.BodyParameter("SmsUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("SmsMethod")),
                    jsonRequest.BodyParameter("SmsFallbackUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("SmsFallbackMethod")),
                    jsonRequest.BodyParameter("HeartbeatUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HeartbeatMethod")),
                    jsonRequest.BodyParameter("StatusCallback"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("StatusCallbackMethod")),
                    jsonRequest.BodyParameter("HangupCallback"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HangupCallbackMethod")));

                Assert.AreEqual("TestApplicationSid", application.Sid);
                Assert.AreEqual("TestApplication", application.FriendlyName);
            }
        }

        [TestMethod]
        public void ApplicationsConnectorUpdateApplicationTest()
        {
            const string methodName = "updateApplication";

            using (new MockServer(Port, "/Accounts/{accountSid}/Applications/{applicationSid}.json", (req, rsp, prm) =>
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

                // Update application using applications connector
                var application = service.ApplicationsConnector.UpdateApplication("TestAccountSid", "TestApplicationSid", 
                    jsonRequest.BodyParameter("FriendlyName"), jsonRequest.BodyParameter("VoiceUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceMethod")),
                    jsonRequest.BodyParameter("VoiceFallbackUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceFallbackMethod")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("VoiceCallerIdLookup")), 
                    jsonRequest.BodyParameter("SmsUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("SmsMethod")),
                    jsonRequest.BodyParameter("SmsFallbackUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("SmsFallbackMethod")),
                    jsonRequest.BodyParameter("HeartbeatUrl"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HeartbeatMethod")),
                    jsonRequest.BodyParameter("StatusCallback"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("StatusCallbackMethod")),
                    jsonRequest.BodyParameter("HangupCallback"), EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HangupCallbackMethod")));

                Assert.AreEqual("TestApplicationSid", application.Sid);
                Assert.AreEqual("TestApplication", application.FriendlyName);
            }
        }

        [TestMethod]
        public void ApplicationsConnectorDeleteApplicationTest()
        {
            const string methodName = "deleteApplication";

            using (new MockServer(Port, "/Accounts/{accountSid}/Applications/{applicationSid}.json", (req, rsp, prm) =>
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

                // Delete application using applications connector
                var application = service.ApplicationsConnector.DeleteApplication("TestApplicationSid");

                Assert.AreEqual("TestApplicationSid", application.Sid);
                Assert.AreEqual("TestApplication", application.FriendlyName);
            }
        }    
    }
}
