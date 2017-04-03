using RestSharp;
using ZangAPI.ConnectionManager;
using ZangAPI.Model;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Usage connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class UsagesConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsagesConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public UsagesConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Views the usage.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="usageSid">The usage sid.</param>
        /// <returns>Returns usage</returns>
        public Usage ViewUsage(string accountSid, string usageSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Usages/{usageSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Usage>(response);
        }

        /// <summary>
        /// Views the usage. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="usageSid">The usage sid.</param>
        /// <returns>Returns usage</returns>
        public Usage ViewUsage(string usageSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewUsage(accountSid, usageSid);
        }

        /// <summary>
        /// Lists the usages.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <param name="product">The product.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns usage list</returns>
        public UsageList ListUsages(string accountSid, int? day = null, int? month = null, int? year = null, Product? product = null, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Usages.json");

            // Add ListUsages query and body parameters
            this.SetParamsForListUsages(request, day, month, year, product, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<UsageList>(response);
        }

        /// <summary>
        /// Lists the usages. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <param name="product">The product.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>REturns usage list</returns>
        public UsageList ListUsages(int? day = null, int? month = null, int? year = null,
            Product? product = null, int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListUsages(accountSid, day, month, year, product, page, pageSize);
        }

        /// <summary>
        /// Sets the parameters for list usages.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <param name="product">The product.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListUsages(RestRequest request, int? day, int? month, int? year, Product? product, int? page, int? pageSize)
        {
            if (day != null) request.AddQueryParameter("Day", day.ToString());
            if (month != null) request.AddQueryParameter("Month", month.ToString());
            if (year != null) request.AddQueryParameter("Year", year.ToString());
            if (product != null) request.AddQueryParameter("Product", product.ToString());
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}
