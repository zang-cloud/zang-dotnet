using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with notifications
    /// </summary>
    public class NotificationsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing notification
        /// </summary>
        public void ViewNotification()
        {
            try
            {
                // View notification using notifications connector
                var notification = service.NotificationsConnector.ViewNotification("TestNotificationSid");
                Console.WriteLine(notification.MoreInfo);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing notifications
        /// </summary>
        public void ListNotifications()
        {
            try
            {
                // List notifications using notifications connector
                var notifications = service.NotificationsConnector.ListNotifications(Log.WARNING, 0, 15);
                Console.WriteLine(notifications.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
