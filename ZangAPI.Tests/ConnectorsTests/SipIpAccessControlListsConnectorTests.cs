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
    public class SipIpAccessControlListsConnectorTests
    {
        private const int Port = 3337;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileNameIPAccesControlList = "Responses.ipacl.json";
        private const string ResponseListJsonFileNameIPAccesControlList = "Responses.ipaclList.json";
        private const string ResponseJsonFileNameAccessControlListIP = "Responses.ipaddress.json";
        private const string ResponseListJsonFileNameAccessControlListIP = "Responses.ipaddressList.json";
        private const string TestGroupName = "SipAclTest";

        [TestMethod]
        public void SipIpAccessControlListsConnectorViewIpAclTest()
        {
            const string methodName = "viewIpAcl";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

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

                // View IP access control list using ip acls connector
                var ipAcl = service.SipIpAccessControlListsConnector.ViewIpAccessControlList("TestIpAccessControlListSid");
      
                Assert.AreEqual(0, ipAcl.IpAddressesCount);
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorListIpAclsTest()
        {
            const string methodName = "listIpAcls";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists.json", (req, rsp, prm) =>
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

                // List IP access control lists using ip acls connector
                var ipAclsList = service.SipIpAccessControlListsConnector.ListIpAccessControlLists(
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")), Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Page")), ipAclsList.Page);
                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("PageSize")), ipAclsList.Pagesize);

                var ipAcl = ipAclsList.Elements.First();

                Assert.AreEqual(1, ipAcl.IpAddressesCount);
                Assert.AreEqual("MyAclList", ipAcl.FriendlyName);
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorCreateIpAclTest()
        {
            const string methodName = "createIpAcl";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists.json", (req, rsp, prm) =>
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

                // Create IP access control list using ip acls connector
                var ipAcl = service.SipIpAccessControlListsConnector.CreateIpAccessControlList(jsonRequest.BodyParameter("FriendlyName"));

                Assert.AreEqual(0, ipAcl.IpAddressesCount);
                Assert.AreEqual(jsonRequest.BodyParameter("FriendlyName"), ipAcl.FriendlyName);
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorUpdateIpAclTest()
        {
            const string methodName = "updateIpAcl";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json", (req, rsp, prm) =>
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

                // Update IP access control list using ip acls connector
                var ipAcl = service.SipIpAccessControlListsConnector.UpdateIpAccessControlList("TestIpAccessControlListSid", jsonRequest.BodyParameter("FriendlyName"));

                Assert.AreEqual(0, ipAcl.IpAddressesCount);
                Assert.AreEqual(jsonRequest.BodyParameter("FriendlyName"), ipAcl.FriendlyName);
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorDeleteIpAclTest()
        {
            const string methodName = "deleteIpAcl";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists/{ipAccessControlListSid}.json", (req, rsp, prm) =>
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

                // Delete IP access control list using ip acls connector
                var ipAcl = service.SipIpAccessControlListsConnector.DeleteIpAccessControlList("TestIpAccessControlListSid");
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorViewAclIpTest()
        {
            const string methodName = "viewAclIp";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameAccessControlListIP}"));
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

                // View access control list IP using ip acls connector
                var aclIp = service.SipIpAccessControlListsConnector.ViewAccessControlListIp("TestIpAccessControlListSid", "TestIpAddressSid");

                Assert.AreEqual("192.168.12.12", aclIp.IPAddress);
                Assert.AreEqual("MyIpAddress", aclIp.FriendlyName);
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorListAclIpsTest()
        {
            const string methodName = "listAclIps";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseListJsonFileNameAccessControlListIP}"));
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

                // List access control list IPs using ip acls connector
                var aclIpsList = service.SipIpAccessControlListsConnector.ListAccessControlListIps("TestIpAccessControlListSid");

                var aclIp = aclIpsList.Elements.First();

                Assert.AreEqual("192.168.12.12", aclIp.IPAddress);
                Assert.AreEqual("MyIpAddress2", aclIp.FriendlyName);
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorAddAclIpTest()
        {
            const string methodName = "addAclIp";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameAccessControlListIP}"));
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

                // Add access control list IP using ip acls connector
                var aclIp = service.SipIpAccessControlListsConnector.AddAccessControlListIp("TestIpAccessControlListSid", 
                    jsonRequest.BodyParameter("FriendlyName"), jsonRequest.BodyParameter("IpAddress"));

                Assert.AreEqual(jsonRequest.BodyParameter("IpAddress"), aclIp.IPAddress);
                Assert.AreEqual(jsonRequest.BodyParameter("FriendlyName"), aclIp.FriendlyName);
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorUpdateAclIpTest()
        {
            const string methodName = "updateAclIp";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameAccessControlListIP}"));
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

                // Update access control list IP using ip acls connector
                var aclIp = service.SipIpAccessControlListsConnector.UpdateAccessControlListIp("TestIpAccessControlListSid", "TestIpAddressSid", jsonRequest.BodyParameter("FriendlyName"), jsonRequest.BodyParameter("IpAddress"));

                Assert.AreEqual(jsonRequest.BodyParameter("IpAddress"), aclIp.IPAddress);
                Assert.AreEqual(jsonRequest.BodyParameter("FriendlyName"), aclIp.FriendlyName);
            }
        }

        [TestMethod]
        public void SipIpAccessControlListsConnectorDeleteAclIpTest()
        {
            const string methodName = "deleteAclIp";

            using (new MockServer(Port, "Accounts/{accountSid}/SIP/IpAccessControlLists/{aclSid}/IpAddresses/{ipSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("DELETE")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{ResponseJsonFileNameAccessControlListIP}"));
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

                // Delete access control list IP using ip acls connector
                var aclIp = service.SipIpAccessControlListsConnector.DeleteAccessControlListIp("TestIpAccessControlListSid", "TestIpAddressSid");
            }
        }
    }
}
