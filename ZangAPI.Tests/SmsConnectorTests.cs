using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using ZangAPI.Configuration;
using ZangAPI.Model.Enums;

namespace ZangAPI.Tests
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
        private const string ResponseJsonFileName = "sms.json";
        private const string ResponseListJsonFileName = "smsList.json";

        [TestMethod]
        public void SmsConnectorSendSms()
        {
            var jsonFileName = "sendSms.json";

            using (new MockServer(Port, "/Accounts/{accountSid}/SMS/Messages.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("POST")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(jsonFileName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(jsonFileName, "queryParams", req);

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
                var jsonRequest = ParametersHelper.GetJsonRequestFromFile(jsonFileName);

                // Create service
                var service = new ZangService(configuration);

                // Send sms using sms connector
                var message = service.SmsConnector.SendSms(jsonRequest.BodyParameter("To"),
                    jsonRequest.BodyParameter("Body"), jsonRequest.BodyParameter("From"),
                    jsonRequest.BodyParameter("StatusCallback"), HttpMethod.GET);

                Assert.AreEqual(jsonRequest.BodyParameter("To"), message.To);
                Assert.AreEqual(jsonRequest.BodyParameter("Body"), message.Body);
                Assert.AreEqual(jsonRequest.BodyParameter("From"), message.From);
                Assert.AreEqual(SmsDirection.OUTBOUND_API, message.Direction);
                Assert.AreEqual(Convert.ToDecimal(0.0616), message.Price);
            }
        }

        [TestMethod]
        public void SmsConnectorViewSmsMessages()
        {
            var jsonFileName = "viewSms.json";

            using (new MockServer(Port, "/Accounts/{accountSid}/SMS/Messages/{smsMessageSid}.json", (req, rsp, prm) =>
            {
                // Check http method
                if (!req.HttpMethod.Equals("GET")) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(jsonFileName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(jsonFileName, "queryParams", req);

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
                var jsonRequest = ParametersHelper.GetJsonRequestFromFile(jsonFileName);

                // Create service
                var service = new ZangService(configuration);

                // Send sms using sms connector
                var message = service.SmsConnector.ViewSmsMessage("TestSmsSid");

                //todo asserts
            }
        }

        [TestMethod]
        public void SmsConnectorListSmsMessages()
        {
            var jsonFileName = "listSmsMessages.json";

            using (new MockServer(Port, "Accounts/{accountSid}/SMS/Messages.json", (req, rsp, prm) =>
            {
                //todo wtf
                //// Check http method
                //if (!req.HttpMethod.Equals(HttpMethod.GET)) throw new ArgumentException();

                // Check parameter equality
                ParametersHelper.CheckParametersEquality(jsonFileName, "bodyParams", req);
                ParametersHelper.CheckParametersEquality(jsonFileName, "queryParams", req);

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
                var jsonRequest = ParametersHelper.GetJsonRequestFromFile(jsonFileName);

                // Create service
                var service = new ZangService(configuration);

                // Send sms using sms connector
                var message = service.SmsConnector.ListSmsMessages(to: jsonRequest.QueryParameter("To"), page: int.Parse(jsonRequest.QueryParameter("Page")), pageSize: int.Parse(jsonRequest.QueryParameter("PageSize")));

                //todo asserts
            }
        }
    }
}