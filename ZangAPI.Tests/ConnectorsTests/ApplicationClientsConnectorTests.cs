using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using ZangAPI.Configuration;
using ZangAPI.Model.Enums;

namespace ZangAPI.Tests.ConnectorsTests
{
    [TestClass]
    public class ApplicationClientsConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.applicationClient.json";
        private const string ResponseListJsonFileName = "Responses.applicationClientList.json";
        private const string TestGroupName = "ApplicationClientsTest";

        [TestMethod]
        public void ApplicationsClientsConnectorViewApplicationClientTest()
        {
            const string methodName = "viewApplicationClient";

            using (new MockServer(Port, "/Accounts/{accountSid}/Applications/{applicationSid}/Clients/{clientSid}.json", (req, rsp, prm) =>
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

                // View application client using application clients connector
                var applicationClient = service.ApplicationClientsConnector.ViewApplicationClient("TestApplicationSid", "TestApplicationClientSid");

                Assert.AreEqual("TestApplicationClientSid", applicationClient.Sid);
                Assert.AreEqual("MyApplicationClient", applicationClient.Nickname);
                Assert.AreEqual("10.0.0.1", applicationClient.RemoteIp);
                Assert.AreEqual(PresenceStatus.INIT, applicationClient.PresenceStatus);

                Assert.AreEqual("/v2/Accounts/TestAccountSid/Applications/TestApplicationSid/Clients/TestApplicationClientSid", applicationClient.Uri);
            }
        }

        [TestMethod]
        public void ApplicationClientsConnectorListApplicationClientsTest()
        {
            const string methodName = "listApplicationClients";

            using (new MockServer(Port, "Accounts/{accountSid}/Applications/{applicationSid}.json", (req, rsp, prm) =>
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

                // List application clients using application clients connector
                var applicationClientsList = service.ApplicationClientsConnector.ListApplicationClients("TestAccountSid", "TestApplicationSid");

                Assert.AreEqual(1, applicationClientsList.Total);

                var applicationClient = applicationClientsList.Elements.First();

                Assert.AreEqual("TestApplicationClientSid", applicationClient.Sid);
                Assert.AreEqual("MyApplicationClient", applicationClient.Nickname);
                Assert.AreEqual("10.0.0.1", applicationClient.RemoteIp);
                Assert.AreEqual(PresenceStatus.INIT, applicationClient.PresenceStatus);
            }
        }

        [TestMethod]
        public void ApplicationClientsConnectorCreateApplicationClientTest()
        {
            const string methodName = "createApplicationClient";

            using (new MockServer(Port, "/Accounts/{accountSid}/Applications/{applicationSid}/Tokens.json", (req, rsp, prm) =>
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

                // Create application client using application clients connector
                var applicationClient = service.ApplicationClientsConnector.CreateApplicationClient("TestApplicationSid", "MyApplicationClient");

                Assert.AreEqual("TestApplicationClientSid", applicationClient.Sid);
                Assert.AreEqual("MyApplicationClient", applicationClient.Nickname);
                Assert.AreEqual("10.0.0.1", applicationClient.RemoteIp);
                Assert.AreEqual(PresenceStatus.INIT, applicationClient.PresenceStatus);
            }
        }
    }
}
