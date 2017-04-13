using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using ZangAPI.Configuration;
using ZangAPI.Helpers;
using ZangAPI.Model.Enums;

namespace ZangAPI.Tests.ConnectorsTests
{
    [TestClass]
    public class FraudControlConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileNameBlocked = "Responses.blocked.json";
        private const string ResponseJsonFileNameAuthorized = "Responses.authorized.json";
        private const string ResponseJsonFileNameWhitelisted = "Responses.whitelisted.json";
        private const string ResponseListJsonFileName = "Responses.fraudControlList.json";
        private const string TestGroupName = "FraudControlTest";

        [TestMethod]
        public void FraudControlConnectorBlockDestinationTest()
        {
            const string methodName = "blockDestination";

            using (new MockServer(Port, "Accounts/{accountSid}/Fraud/Block/{countryCode}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameBlocked}"));
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

                // Block destination using fraud control connector
                var fraudControlRule = service.FraudControlConnector.BlockDestination("HR", 
                    Convert.ToBoolean(jsonRequest.BodyParameter("MobileEnabled")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("LandlineEnabled")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("SmsEnabled")));

                Assert.AreEqual("TestFraudControlRuleSid", fraudControlRule.Sid);
                Assert.AreEqual(Convert.ToBoolean(jsonRequest.BodyParameter("MobileEnabled")), fraudControlRule.MobileEnabled);
                Assert.AreEqual(Convert.ToBoolean(jsonRequest.BodyParameter("SmsEnabled")), fraudControlRule.SmsEnabled);
            }
        }

        [TestMethod]
        public void FraudControlConnectorAuthorizeDestinationTest()
        {
            const string methodName = "authorizeDestination";

            using (new MockServer(Port, "Accounts/{accountSid}/Fraud/Authorize/{countryCode}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameAuthorized}"));
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

                // Authorize destination using fraud control connector
                var fraudControlRule = service.FraudControlConnector.AuthorizeDestination("HR",
                    Convert.ToBoolean(jsonRequest.BodyParameter("MobileEnabled")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("LandlineEnabled")),
                    Convert.ToBoolean(jsonRequest.BodyParameter("SmsEnabled")));

                Assert.AreEqual("TestFraudControlRuleSid", fraudControlRule.Sid);
                Assert.AreEqual(Convert.ToBoolean(jsonRequest.BodyParameter("MobileEnabled")), fraudControlRule.MobileEnabled);
                Assert.AreEqual(Convert.ToBoolean(jsonRequest.BodyParameter("SmsEnabled")), fraudControlRule.SmsEnabled);
            }
        }

        [TestMethod]
        public void FraudControlConnectorExtendDestinationAuthorizationTest()
        {
            const string methodName = "extendDestinationAuthorization";

            using (new MockServer(Port, "Accounts/{accountSid}/Fraud/Extend/{countryCode}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameAuthorized}"));
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

                // Extend destination authorization using fraud control connector
                var fraudControlRule = service.FraudControlConnector.ExtendDestinationAuthorization("HR");

                Assert.AreEqual("TestFraudControlRuleSid", fraudControlRule.Sid);
            }
        }

        [TestMethod]
        public void FraudControlConnectorWhitelistDestinationTest()
        {
            const string methodName = "whitelistDestination";

            using (new MockServer(Port, "Accounts/{accountSid}/Fraud/Whitelist/{countryCode}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameWhitelisted}"));
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

                // Whitelist destination using fraud control connector
                var fraudControlRule = service.FraudControlConnector.WhitelistDestination("HR",
                    Convert.ToBoolean(jsonRequest.QueryParameter("MobileEnabled")),
                    Convert.ToBoolean(jsonRequest.QueryParameter("LandlineEnabled")),
                    Convert.ToBoolean(jsonRequest.QueryParameter("SmsEnabled")));

                Assert.AreEqual("TestFraudControlRuleSid", fraudControlRule.Sid);
                Assert.AreEqual(Convert.ToBoolean(jsonRequest.QueryParameter("MobileEnabled")), fraudControlRule.MobileEnabled);
                Assert.AreEqual(Convert.ToBoolean(jsonRequest.QueryParameter("SmsEnabled")), fraudControlRule.SmsEnabled);
            }
        }

        [TestMethod]
        public void FraudControlConnectorListFraudControlResourcesTest()
        {
            const string methodName = "listFraudControlResources";

            using (new MockServer(Port, "Accounts/{accountSid}/Fraud.json", (req, rsp, prm) =>
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

                // List resources using fraud control connector
                var fraudControlResourcesList = service.FraudControlConnector.ListFraudControlResources(
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")),
                    Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(4, fraudControlResourcesList.Total);

                var fraudControlRule = fraudControlResourcesList.Authorized.First();

                Assert.AreEqual("TestFraudControlRuleSid", fraudControlRule.Sid);
                Assert.AreEqual(false, fraudControlRule.MobileEnabled);
                Assert.AreEqual(true, fraudControlRule.SmsEnabled);
            }
        }
    }
}