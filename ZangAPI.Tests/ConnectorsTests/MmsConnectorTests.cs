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
    /// <summary>
    /// Tests for MmsConncector
    /// </summary>
    [TestClass]
    public class MmsConnectorTests
    {
        private const int Port = 3337;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.mms.json";
        private const string TestGroupName = "MmsTest";

        [TestMethod]
        public void MmsConnectorSendMms()
        {
            const string methodName = "sendMms";

            using (new MockServer(Port, "/Accounts/{accountSid}/MMS/Messages.json", (req, rsp, prm) =>
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

                // Send mms using mms connector
                var message = service.MmsConnector.SendMms(jsonRequest.BodyParameter("To"),
                    jsonRequest.BodyParameter("MediaUrl"), from:jsonRequest.BodyParameter("From"),
                    body:jsonRequest.BodyParameter("Body"));

                Assert.AreEqual(jsonRequest.BodyParameter("To"), message.To);
                Assert.AreEqual(jsonRequest.BodyParameter("MediaUrl"), message.MediaUrl);
                Assert.AreEqual(jsonRequest.BodyParameter("Body"), message.Body);
                Assert.AreEqual(jsonRequest.BodyParameter("From"), message.From);
                Assert.AreEqual(MmsStatus.QUEUED, message.Status);
                Assert.AreEqual(MmsDirection.OUTBOUND, message.Direction);
            }
        }

    }
}