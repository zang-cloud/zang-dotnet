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
    public class SipDomainsConnectorTests
    {
        private const int Port = 3337;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileNameDomain = "Responses.domain.json";
        private const string ResponseListJsonFileNameDomains = "Responses.domainList.json";
        private const string ResponseJsonFileNameCredentialsList = "Responses.credentialList.json";
        private const string ResponseListJsonFileNameDomainsCredentialsList = "Responses.credentialsListList.json";
        private const string ResponseJsonFileNameIPAccesControlList = "Responses.ipacl.json";
        private const string ResponseListJsonFileNameIPAccesControlList = "Responses.ipaclList.json";
        private const string TestGroupName = "SipDomainsTest";

        [TestMethod]
        public void SipDomainsConnectorViewDomainTest()
        {
            const string methodName = "viewDomain";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameDomain}"));
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

                // View domain using domains connector
                var domain = service.SipDomainsConnector.ViewDomain("TestDomainSid");

                Assert.AreEqual("mytestdomain.com", domain.DomainName);
                Assert.AreEqual("http://telapi.com/ivr/welcome/call", domain.VoiceUrl);
                Assert.AreEqual("POST", domain.VoiceFallbackMethod.ToString());
            }
        }

        [TestMethod]
        public void SipDomainsConnectorListDomainsTest()
        {
            const string methodName = "listDomains";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseListJsonFileNameDomains}"));
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

                // List domains using domainsconnector
                var domainsList = service.SipDomainsConnector.ListDomains();

                Assert.AreEqual(2, domainsList.Total);

                var domain = domainsList.Elements.First();

                Assert.AreEqual("mytestdomain.com", domain.DomainName);
                Assert.AreEqual("http://telapi.com/ivr/welcome/call", domain.VoiceUrl);
                Assert.AreEqual("POST", domain.VoiceFallbackMethod.ToString());
            }
        }

        [TestMethod]
        public void SipDomainsConnectorCreateDomainTest()
        {
            const string methodName = "createDomain";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameDomain}"));
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

                // Create domain using domains connector
                var domain = service.SipDomainsConnector.CreateDomain("TestAccountSid",
                    jsonRequest.BodyParameter("DomainName"), jsonRequest.BodyParameter("FriendlyName"),
                    jsonRequest.BodyParameter("VoiceUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceMethod")),
                    jsonRequest.BodyParameter("VoiceFallbackUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceFallbackMethod")),
                    jsonRequest.BodyParameter("HeartbeatUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HeartbeatMethod")),
                    jsonRequest.BodyParameter("VoiceStatusCallback"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceStatusCallbackMethod")));

                Assert.AreEqual(jsonRequest.BodyParameter("DomainName"), domain.DomainName);
                Assert.AreEqual(jsonRequest.BodyParameter("FriendlyName"), domain.FriendlyName);
                Assert.AreEqual(EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceMethod")), domain.VoiceMethod);
            }
        }

        [TestMethod]
        public void SipDomainsConnectorUpdateDomainTest()
        {
            const string methodName = "updateDomain";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameDomain}"));
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

                // Update domain using domains connector
                var domain = service.SipDomainsConnector.UpdateDomain("TestAccountSid",
                    "TestDomainSid", jsonRequest.BodyParameter("FriendlyName"),
                    jsonRequest.BodyParameter("VoiceUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceMethod")),
                    jsonRequest.BodyParameter("VoiceFallbackUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceFallbackMethod")),
                    jsonRequest.BodyParameter("HeartbeatUrl"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("HeartbeatMethod")),
                    jsonRequest.BodyParameter("VoiceStatusCallback"),
                    EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceStatusCallbackMethod")));

                Assert.AreEqual(jsonRequest.BodyParameter("FriendlyName"), domain.FriendlyName);
                Assert.AreEqual(EnumHelper.ParseEnum<HttpMethod>(jsonRequest.BodyParameter("VoiceMethod")), domain.VoiceMethod);
            }
        }

        [TestMethod]
        public void SipDomainsConnectorDeleteDomainTest()
        {
            const string methodName = "deleteDomain";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("DELETE")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameDomain}"));
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

                // Delete domain using domains connector
                var domain = service.SipDomainsConnector.DeleteDomain("TestDomainSid");

                Assert.AreEqual("mytestdomain.com", domain.DomainName);
                Assert.AreEqual("http://telapi.com/ivr/welcome/call", domain.VoiceUrl);
                Assert.AreEqual("POST", domain.VoiceFallbackMethod.ToString());
            }
        }

        [TestMethod]
        public void SipDomainsConnectorMapCredentialsListTest()
        {
            const string methodName = "mapCredentialsList";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings.json", (req, rsp, prm) =>
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

                // Map credential list using domains connector
                var credentialList = service.SipDomainsConnector.MapCredentialsList("TestDomainSid", "TestCredentialsListSid");

                Assert.AreEqual(jsonRequest.BodyParameter("CredentialListSid"), "TestCredentialsListSid");
            }
        }

        [TestMethod]
        public void SipDomainsConnectorListMappedCredentialsListsTest()
        {
            const string methodName = "listMappedCredentialsList";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings.json", (req, rsp, prm) =>
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

                // List mapped credentials lists using domains connector
                var credentialsListsList = service.SipDomainsConnector.ListMappedCredentialsLists("TestDomainSid");

                Assert.AreEqual(1, credentialsListsList.Total);

                var credentialList = credentialsListsList.Elements.First();

                Assert.AreEqual("MyCredentialsList2", credentialList.FriendlyName);
            }
        }

        [TestMethod]
        public void SipDomainsConnectorDeleteMappedCredentialsListTest()
        {
            const string methodName = "deleteMappedCredentialsList";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}/CredentialListMappings/{clSid}.json", (req, rsp, prm) =>
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

                // Delete mapped credential list using domains connector
                var credentialList = service.SipDomainsConnector.DeleteMappedCredentialsList("TestDomainSid", "TestCredentialsListSid");

                Assert.AreEqual("TestCredentialsListSid", credentialList.Sid);
            }
        }

        [TestMethod]
        public void SipDomainsConnectorMapIPAccessControlListTest()
        {
            const string methodName = "mapIpAcl";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameIPAccesControlList}"));
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

                // Map IP access control list using domains connector
                var ipAcl = service.SipDomainsConnector.MapIpAccessControlList("TestDomainSid", "TestIpAccessControlListSid");

                Assert.AreEqual("TestIpAccessControlListSid", ipAcl.Sid);
            }
        }

        [TestMethod]
        public void SipDomainsConnectorListMappedIPAccessControlListsTest()
        {
            const string methodName = "listMappedIpAcls";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseListJsonFileNameIPAccesControlList}"));
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

                // List mapped IP access control lists using domains connector
                var ipAccessControlListsList = service.SipDomainsConnector.ListMappedIpAccessControlLists("TestDomainSid");

                Assert.AreEqual(1, ipAccessControlListsList.Total);

                var ipAcl = ipAccessControlListsList.Elements.First();

                Assert.AreEqual("MyAclList", ipAcl.FriendlyName);
            }
        }

        [TestMethod]
        public void SipDomainsConnectorDeleteIPAccessControlListTest()
        {
            const string methodName = "deleteIpAcl";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/Domains/{domainSid}/IpAccessControlListMappings/{alSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("DELETE")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameIPAccesControlList}"));
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

                // Delete mapped IP access control list using domains connector
                var ipAcl = service.SipDomainsConnector.DeleteMappedIpAccessControlList("TestDomainSid", "TestIpAccessControlListSid");

                Assert.AreEqual("TestIpAccessControlListSid", ipAcl.Sid);
            }
        }
    }
}