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
    public class NotificationsConnectorTests
    {
        private const int Port = 21513;
        private const string AccountSid = "TestAccountSid";
        private const string AuthToken = "TestAuthToken";
        private const string ResponseJsonFileName = "Responses.notification.json";
        private const string ResponseListJsonFileName = "Responses.notificationList.json";
        private const string TestGroupName = "NotificationsTest";

        [TestMethod]
        public void NotificationsConnectorViewNotificationTest()
        {
            const string methodName = "viewNotification";

            using (new MockServer(Port, "/Accounts/{accountSid}/Notifications/{notificationSid}.json", (req, rsp, prm) =>
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

                // View notification using notifications connector
                var notification = service.NotificationsConnector.ViewNotification("TestNotificationSid");

                Assert.AreEqual(EnumHelper.ParseEnum<Log>("0"), notification.Log);
                Assert.AreEqual(21227, notification.ErrorCode);
            }
        }

        [TestMethod]
        public void NotificationsConnectorListNotificationsTest()
        {
            const string methodName = "listNotifications";

            using (new MockServer(Port, "Accounts/{accountSid}/Notifications.json", (req, rsp, prm) =>
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

                // List notifications using notifications connector
                var notificationList = service.NotificationsConnector.ListNotifications(EnumHelper.ParseEnum<Log>(jsonRequest.QueryParameter("Log")),
                    Convert.ToInt32(jsonRequest.QueryParameter("Page")), Convert.ToInt32(jsonRequest.QueryParameter("PageSize")));

                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("PageSize")), notificationList.Pagesize);
                Assert.AreEqual(Convert.ToInt32(jsonRequest.QueryParameter("Page")), notificationList.Page);
                Assert.AreEqual(23, notificationList.Total);

                var notification = notificationList.Elements.First();

                Assert.AreEqual(EnumHelper.ParseEnum<Log>(jsonRequest.QueryParameter("Log")), notification.Log);
                Assert.AreEqual(21227, notification.ErrorCode);
            }
        }
    }
}
