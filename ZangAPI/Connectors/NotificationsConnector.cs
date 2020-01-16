using RestSharp;
using AvayaCPaaS.ConnectionManager;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model;
using AvayaCPaaS.Model.Enums;
using AvayaCPaaS.Model.Lists;

namespace AvayaCPaaS.Connectors
{
    /// <summary>
    /// Notifications connector - used for all forms of communication with the Notifications endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Connectors.AConnector" />
    public class NotificationsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public NotificationsConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Shows information on some notification
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="notificationSid">Notification SID</param>
        /// <returns>Returns notification</returns>
        public Notification ViewNotification(string accountSid, string notificationSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Notifications/{notificationSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Notification>(response);
        }

        /// <summary>
        /// Shows information on some notification. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="notificationSid">Notification SID</param>
        /// <returns>Returns notification</returns>
        public Notification ViewNotification(string notificationSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewNotification(accountSid, notificationSid);
        }

        /// <summary>
        /// Shows info on all notifications associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="log">Specifies that only notifications with the given log level value should be listed. Allowed values are 1,2 or 3, where 2=INFO, 1=WARNING, 0=ERROR.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns notification list</returns>
        public NotificationsList ListNotifications(string accountSid, Log? log = null, int? page = null,
            int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Notifications.json");

            // Add ListNotifications query and body parameters
            this.SetParamsForListNotifications(request, log, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<NotificationsList>(response);
        }

        /// <summary>
        /// Shows info on all notifications associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="log">Specifies that only notifications with the given log level value should be listed. Allowed values are 1,2 or 3, where 2=INFO, 1=WARNING, 0=ERROR.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns notification list</returns>
        public NotificationsList ListNotifications(Log? log = null, int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListNotifications(accountSid, log, page, pageSize);
        }

        /// <summary>
        /// Sets the parameters for list notifications.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="log">The log.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListNotifications(IRestRequest request, Log? log, int? page, int? pageSize)
        {
            if (log != null) request.AddQueryParameter("Log", ((int) log.Value).ToString());
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}