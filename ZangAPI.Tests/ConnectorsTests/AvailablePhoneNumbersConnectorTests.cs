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
    public class AvailablePhoneNumbersConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseListJsonFileName = "Responses.availablePhoneNumberList.json";
        private const string TestGroupName = "AvailablePhoneNumbersTest";

        [TestMethod]
        public void AvailablePhoneNumbersConnectorListAvailablePhoneNumbersTest()
        {
            const string methodName = "listAvailablePhoneNumbers";

            using (new MockServer(Port, "Accounts/{accountSid}/AvailablePhoneNumbers/{country}/{typeString}.json", (req, rsp, prm) =>
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

                // List available phone numbers using available phone numbers connector
                var availablePhoneNumbersList = service.AvailablePhoneNumbersConnector.ListAvailableNumbers("TestAccountSid",
                    "HR", PhoneNumberType.TOLLFREE, 
                    jsonRequest.QueryParameter("Contains"), jsonRequest.QueryParameter("AreaCode"), jsonRequest.QueryParameter("InRegion"),
                    jsonRequest.QueryParameter("InPostalCode"), Convert.ToInt32(jsonRequest.QueryParameter("Page")), Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                var availablePhoneNumber = availablePhoneNumbersList.Elements.First();

                Assert.AreEqual("HR", availablePhoneNumber.IsoCountry);
            }
        }
    }
}