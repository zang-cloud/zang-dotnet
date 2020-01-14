using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Tests.ConnectorsTests
{
    [TestClass]
    public class CarrierServicesConnectorTests
    {
        private const int Port = Configuration.Port;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileNameCarrier = "Responses.carrierLookup.json";
        private const string ResponseListJsonFileNameCarrier = "Responses.carrierLookupList.json";
        private const string ResponseJsonFileNameCnam = "Responses.cnamLookup.json";
        private const string ResponseListJsonFileNameCnam = "Responses.cnamLookupList.json";
        private const string ResponseJsonFileNameBna = "Responses.bnaLookup.json";
        private const string ResponseListJsonFileNameBna = "Responses.bnaLookupList.json";
        private const string TestGroupName = "CarrierServicesTest";

        [TestMethod]
        public void CarrierServicesConnectorCarrierLookupTest()
        {
            const string methodName = "carrierLookup";

            using (new MockServer(Port, "Accounts/{accountSid}/Lookups/Carrier.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileNameCarrier}"));
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

                // Retrieve carrier lookup using carrier services connector
                var carrierLookup = service.CarrierServicesConnector.CarrierLookup(jsonRequest.BodyParameter("PhoneNumber"));

                Assert.AreEqual(jsonRequest.BodyParameter("PhoneNumber"), carrierLookup.PhoneNumber);
                Assert.AreEqual(22607, carrierLookup.CarrierId);
            }
        }

        [TestMethod]
        public void CarrierServicesConnectorListCarrierLookups()
        {
            const string methodName = "listCarrierLookups";

            using (new MockServer(Port, "Accounts/{accountSid}/Lookups/Carrier.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseListJsonFileNameCarrier}"));
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

                // List carrier lookups using carrier services connector
                var carrierLookupsList = service.CarrierServicesConnector.CarrierLookupList(
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")), Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(3, carrierLookupsList.Total);
                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Page")), carrierLookupsList.Page);

                var carrierLookup = carrierLookupsList.Elements.First();

                Assert.AreEqual("+14086474636", carrierLookup.PhoneNumber);
                Assert.AreEqual(22607, carrierLookup.CarrierId);
            }
        }

        [TestMethod]
        public void CarrierServicesConnectorCnamLookupTest()
        {
            const string methodName = "cnamLookup";

            using (new MockServer(Port, "Accounts/{accountSid}/Lookups/Cnam.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileNameCnam}"));
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

                // Retrieve cnam lookup using carrier services connector
                var cnamLookup = service.CarrierServicesConnector.CnamLookup(jsonRequest.BodyParameter("PhoneNumber"));

                Assert.AreEqual(jsonRequest.BodyParameter("PhoneNumber"), cnamLookup.PhoneNumber);
                Assert.AreEqual("/v2/Accounts/ACe1889084056167d57a944486a50ceb46/CNAM/CL6588908407bcc1991e244475aaadec39", cnamLookup.Uri);
            }
        }

        [TestMethod]
        public void CarrierServicesConnectorListCnamLookups()
        {
            const string methodName = "listCnamLookups";

            using (new MockServer(Port, "Accounts/{accountSid}/Lookups/Cnam.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseListJsonFileNameCnam}"));
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

                // List cnam lookups using carrier services connector
                var cnamLookupsList = service.CarrierServicesConnector.CnamLookupList(
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")), Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(4, cnamLookupsList.Total);

                var cnamLookup = cnamLookupsList.Elements.First();

                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Page")), cnamLookupsList.Page);
                Assert.AreEqual("+19093900002", cnamLookup.PhoneNumber);
                Assert.AreEqual("/v2/Accounts/ACe1889084056167d57a944486a50ceb46/CNAM/CL6588908407bcc1991e244475aaadec39", cnamLookup.Uri);
            }
        }

        [TestMethod]
        public void CarrierServicesConnectorBnaLookupTest()
        {
            const string methodName = "bnaLookup";

            using (new MockServer(Port, "Accounts/{accountSid}/Lookups/Bna.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseJsonFileNameBna}"));
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

                // Retrieve bna lookup using carrier services connector
                var bnaLookup = service.CarrierServicesConnector.BnaLookup(jsonRequest.BodyParameter("PhoneNumber"));

                Assert.AreEqual(jsonRequest.BodyParameter("PhoneNumber"), bnaLookup.PhoneNumber);
                Assert.AreEqual("Saratoga", bnaLookup.City);
            }
        }

        [TestMethod]
        public void CarrierServicesConnectorListBnaLookups()
        {
            const string methodName = "listBnaLookups";

            using (new MockServer(Port, "Accounts/{accountSid}/Lookups/Bna.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(TestGroupName, methodName, "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader =
                    new StreamReader(assembly.GetManifestResourceStream($"AvayaCPaaS.Tests.{ResponseListJsonFileNameBna}"));
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

                // List bna lookups using carrier services connector
                var bnaLookupsList = service.CarrierServicesConnector.BnaLookupList(
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")),
                    Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(1, bnaLookupsList.Total);
                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Page")), bnaLookupsList.Page);

                var bnaLookup = bnaLookupsList.Elements.First();

                Assert.AreEqual("+14086474636", bnaLookup.PhoneNumber);
                Assert.AreEqual("Saratoga", bnaLookup.City);
            }
        }
    }
}
