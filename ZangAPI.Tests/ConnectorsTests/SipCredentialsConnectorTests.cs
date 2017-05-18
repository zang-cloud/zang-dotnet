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
    public class SipCredentialsConnectorTests
    {
        private const int Port = 3337;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.credential.json";
        private const string ResponseListJsonFileName = "Responses.listofcredentials.json";
        private const string ResponseJsonFileNameCredentialsList = "Responses.credentialList.json";
        private const string ResponseListJsonFileNameDomainsCredentialsList = "Responses.credentialsListList.json";

        private const string TestGroupName = "SipCredentialsTest";

        [TestMethod]
        public void SipCredentialsConnectorViewCredentialTest()
        {
            const string methodName = "viewCredential";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials/{credentialSid}.json", (req, rsp, prm) =>
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

                // View credential using credentials connector
                var credential = service.SipCredentialsConnector.ViewCredential("TestCredentialsListSid", "TestCredentialSid");

                Assert.AreEqual("TestCredentialSid", credential.Sid);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorListCredentialsTest()
        {
            const string methodName = "listCredentials";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials.json", (req, rsp, prm) =>
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

                // List credentials using credentials connector
                var credentialList = service.SipCredentialsConnector.ListCredentials("TestCredentialsListSid");

                Assert.AreEqual(1, credentialList.Total);

                var credential = credentialList.Elements.First();

                Assert.AreEqual("TestCredentialSid", credential.Sid);
                Assert.AreEqual("testuser123", credential.FriendlyName);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorCreateCredentialTest()
        {
            const string methodName = "createCredential";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials.json", (req, rsp, prm) =>
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

                // Create credential using credentials connector
                var credential = service.SipCredentialsConnector.CreateCredential("TestCredentialsListSid", 
                    jsonRequest.BodyParameter("Username"), jsonRequest.BodyParameter("Password"));

                Assert.AreEqual("TestCredentialSid", credential.Sid);
                Assert.AreEqual(jsonRequest.BodyParameter("Username"), credential.Username);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorUpdateCredentialTest()
        {
            const string methodName = "updateCredential";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials/{credentialSid}.json", (req, rsp, prm) =>
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

                // Update credential using credentials connector
                var credential = service.SipCredentialsConnector.UpdateCredential("TestCredentialsListSid", "TestCredentialSid", jsonRequest.BodyParameter("Password"));

                Assert.AreEqual("TestCredentialSid", credential.Sid);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorDeleteCredentialTest()
        {
            const string methodName = "deleteCredential";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists/{clSid}/Credentials/{credentialSid}.json", (req, rsp, prm) =>
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

                // Delete credential using credentials connector
                var credential = service.SipCredentialsConnector.DeleteCredential("TestCredentialsListSid", "TestCredentialSid");

                Assert.AreEqual("TestCredentialSid", credential.Sid);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorViewCredentialsListTest()
        {
            const string methodName = "viewCredentialsList";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists/{clSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameCredentialsList}"));
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

                // View credentials list using credentials connector
                var credentialList = service.SipCredentialsConnector.ViewCredentialsList("TestCredentialsListSid");

                Assert.AreEqual("TestCredentialsListSid", credentialList.Sid);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorListCredentialsListsTest()
        {
            const string methodName = "listCredentialsLists";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseListJsonFileNameDomainsCredentialsList}"));
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

                // List credentials lists using credentials connector
                var credentialsListsList = service.SipCredentialsConnector.ListCredentialsLists();

                Assert.AreEqual(1, credentialsListsList.Total);

                var credentialsList = credentialsListsList.Elements.First();

                Assert.AreEqual("TestCredentialsListSid", credentialsList.Sid);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorCreateCredentialListTest()
        {
            const string methodName = "createCredentialsList";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameCredentialsList}"));
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

                // Create credentials list using credentials connector
                var credentialList = service.SipCredentialsConnector.CreateCredentialsList(jsonRequest.BodyParameter("FriendlyName"));

                Assert.AreEqual("TestCredentialsListSid", credentialList.Sid);
                Assert.AreEqual(jsonRequest.BodyParameter("FriendlyName"), credentialList.FriendlyName);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorUpdateCredentialListTest()
        {
            const string methodName = "updateCredentialsList";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists/{clSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameCredentialsList}"));
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

                // Update credentials list using credentials connector
                var credentialList = service.SipCredentialsConnector.UpdateCredentialsList("TestCredentialsListSid", jsonRequest.BodyParameter("FriendlyName"));

                Assert.AreEqual(jsonRequest.BodyParameter("FriendlyName"), credentialList.FriendlyName);
            }
        }

        [TestMethod]
        public void SipCredentialsConnectorDeleteCredentialListTest()
        {
            const string methodName = "deleteCredentialsList";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/CredentialLists/{clSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("DELETE")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameCredentialsList}"));
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

                // Delete credential list using credentials connector
                var credentialList = service.SipCredentialsConnector.DeleteCredentialsList("TestCredentialsListSid");

                Assert.AreEqual("TestCredentialsListSid", credentialList.Sid);
            }
        }
    }
}
