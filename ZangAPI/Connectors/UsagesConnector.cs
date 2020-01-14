using RestSharp;
using AvayaCPaaS.ConnectionManager;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model;
using AvayaCPaaS.Model.Enums;
using AvayaCPaaS.Model.Lists;

namespace AvayaCPaaS.Connectors
{
    /// <summary>
    /// Usage connector - used for all forms of communication with the Usages endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Connectors.AConnector" />
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
        /// View the usage of an item returned by List Usages
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="usageSid">Usage SID.</param>
        /// <returns>Returns usage</returns>
        public Usage ViewUsage(string accountSid, string usageSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Usages/{usageSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Usage>(response);
        }

        /// <summary>
        /// View the usage of an item returned by List Usages. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="usageSid">Usage SID.</param>
        /// <returns>Returns usage</returns>
        public Usage ViewUsage(string usageSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewUsage(accountSid, usageSid);
        }

        /// <summary>
        /// Complete list of all usages of your account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="day">Filters usage by day of month. If no month is specified then defaults to current month. Allowed values are integers between 1 and 31 depending on the month. Leading 0s will be ignored.</param>
        /// <param name="month">Filters usage by month. Allowed values are integers between 1 and 12. Leading 0s will be ignored.</param>
        /// <param name="year">Filters usage by year. Allowed values are valid years in integer form such as "2014".</param>
        /// <param name="product">Filters usage by a specific “product” of Avaya Cloud. Each product is uniquely identified by an integer. For example: Product=1, would return all outbound call usage. The integer assigned to each product is listed below.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns usage list</returns>
        public UsagesList ListUsages(string accountSid, int? day = null, int? month = null, int? year = null,
            Product? product = null, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Usages.json");

            // Add ListUsages query and body parameters
            this.SetParamsForListUsages(request, day, month, year, product, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<UsagesList>(response);
        }

        /// <summary>
        /// Complete list of all usages of your account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="day">Filters usage by day of month. If no month is specified then defaults to current month. Allowed values are integers between 1 and 31 depending on the month. Leading 0s will be ignored.</param>
        /// <param name="month">Filters usage by month. Allowed values are integers between 1 and 12. Leading 0s will be ignored.</param>
        /// <param name="year">Filters usage by year. Allowed values are valid years in integer form such as "2014".</param>
        /// <param name="product">Filters usage by a specific “product” of Avaya Cloud. Each product is uniquely identified by an integer. For example: Product=1, would return all outbound call usage. The integer assigned to each product is listed below.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns usage list</returns>
        public UsagesList ListUsages(int? day = null, int? month = null, int? year = null,
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
        private void SetParamsForListUsages(IRestRequest request, int? day, int? month, int? year, Product? product,
            int? page, int? pageSize)
        {
            if (day != null) request.AddQueryParameter("Day", day.ToString());
            if (month != null) request.AddQueryParameter("Month", month.ToString());
            if (year != null) request.AddQueryParameter("Year", year.ToString());
            if (product != null) request.AddQueryParameter("Product", ProductStringConverter.GetProduct(product.Value));
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}