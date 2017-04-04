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
    /// Tests for SmsConncector
    /// </summary>
    [TestClass]
    public class SmsConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.sms.json";
        private const string ResponseListJsonFileName = "Responses.smsList.json";
        private const string TestGroupName = "SmsTest";

        [TestMethod]
        public void SmsConnectorSendSms()
        {
            const string methodName = "sendSms";

            using (new MockServer(Port, "/Accounts/{accountSid}/SMS/Messages.json", (req, rsp, prm) =>
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

                // Send sms using sms connector
                var message = service.SmsConnector.SendSms(jsonRequest.BodyParameter("To"),
                    jsonRequest.BodyParameter("Body"), jsonRequest.BodyParameter("From"),
                    jsonRequest.BodyParameter("StatusCallback"), HttpMethod.GET);

                Assert.AreEqual(jsonRequest.BodyParameter("To"), message.To);
                Assert.AreEqual(jsonRequest.BodyParameter("Body"), message.Body);
                Assert.AreEqual(jsonRequest.BodyParameter("From"), message.From);
                Assert.AreEqual(SmsStatus.SENT, message.Status);
                Assert.AreEqual(SmsDirection.OUTBOUND_API, message.Direction);
                Assert.AreEqual(Convert.ToDecimal(0.0616), message.Price);
            }
        }

        [TestMethod]
        public void SmsConnectorViewSmsMessages()
        {
            const string methodName = "viewSms";

            using (new MockServer(Port, "/Accounts/{accountSid}/SMS/Messages/{smsMessageSid}.json", (req, rsp, prm) =>
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

                // View sms using sms connector
                var message = service.SmsConnector.ViewSmsMessage("TestSmsSid");

                Assert.AreEqual(SmsStatus.SENT, message.Status);
                Assert.AreEqual(SmsDirection.OUTBOUND_API, message.Direction);
                Assert.AreEqual(Convert.ToDecimal(0.0616), message.Price);
            }
        }

        [TestMethod]
        public void SmsConnectorListSmsMessages()
        {
            const string methodName = "listSms";

            using (new MockServer(Port, "Accounts/{accountSid}/SMS/Messages.json", (req, rsp, prm) =>
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

                // List messages using sms connector
                var smsList = service.SmsConnector.ListSmsMessages(to: jsonRequest.QueryParameter("To"), page: int.Parse(jsonRequest.QueryParameter("Page")), pageSize: int.Parse(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(2, smsList.Total);
                Assert.AreEqual(1, smsList.Numpages);
                Assert.AreEqual(jsonRequest.QueryParameter("Page"), smsList.Page.ToString());
                Assert.AreEqual(jsonRequest.QueryParameter("PageSize"), smsList.Pagesize.ToString());

                var message = smsList.Elements.Last();

                Assert.AreEqual(jsonRequest.QueryParameter("To"), message.To);
                Assert.AreEqual(SmsStatus.SENT, message.Status);
                Assert.AreEqual(SmsDirection.OUTBOUND_API, message.Direction);
                Assert.AreEqual(Convert.ToDecimal(0.0616), message.Price);
            }
        }
    }
}