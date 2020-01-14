using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Tests.ConnectorsTests
{
    [TestClass]
    public class UsagesConnectorTests
    {
        private const int Port = Configuration.Port;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.usage.json";
        private const string ResponseListJsonFileName = "Responses.usageList.json";
        private const string TestGroupName = "UsagesTest";

        [TestMethod]
        public void UsagesConnectorViewUsageTest()
        {
            const string methodName = "viewUsage";

            using (new MockServer(Port, "Accounts/{accountSid}/Usages/{usageSid}.json", (req, rsp, prm) =>
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

                // View usage using usages connector
                var usage = service.UsagesConnector.ViewUsage("TestUsageSid");
            }
        }

        [TestMethod]
        public void UsagesConnectorListUsagesTest()
        {
            const string methodName = "listUsages";

            using (new MockServer(Port, "Accounts/{accountSid}/Usages.json", (req, rsp, prm) =>
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

                // List usages using usages connector
                var usageList = service.UsagesConnector.ListUsages(Convert.ToInt32(jsonRequest.QueryParameter("Day")),
                    Convert.ToInt32(jsonRequest.QueryParameter("Month")), Convert.ToInt32(jsonRequest.QueryParameter("Year")),
                    ProductStringConverter.GetProduct(Convert.ToInt32(jsonRequest.QueryParameter("Product"))),
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")),
                    Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(jsonRequest.QueryParameter("Page"), usageList.Page.ToString());
                Assert.AreEqual(jsonRequest.QueryParameter("PageSize"), usageList.Pagesize.ToString());

                var usage = usageList.Elements.First();

                Assert.AreEqual(ProductStringConverter.GetProduct(Convert.ToInt32(jsonRequest.QueryParameter("Product"))), usage.Product);
                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Year")), usage.Year);
            }
        }
    }
}
