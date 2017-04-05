using RestSharp;
using RestSharp.Extensions;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Available phone numbers connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class AvailablePhoneNumbersConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailablePhoneNumbersConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public AvailablePhoneNumbersConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Lists the available numbers.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="country">The country.</param>
        /// <param name="type">The type.</param>
        /// <param name="contains">The contains.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="inRegion">The in region.</param>
        /// <param name="inPostalCode">The in postal code.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns available phone number list</returns>
        public AvailablePhoneNumberList ListAvailableNumbers(string accountSid, string country, PhoneNumberType type,            
            string contains = null, string areaCode = null, string inRegion = null, string inPostalCode = null, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            var typeString = EnumHelper.GetEnumValue(type);

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/AvailablePhoneNumbers/{country}/{typeString}.json");

            // Add ListNotifications query and body parameters
            this.SetParamsForListNotifications(request, contains, areaCode, inRegion, inPostalCode, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<AvailablePhoneNumberList>(response);
        }

        /// <summary>
        /// Lists the available numbers. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="type">The type.</param>
        /// <param name="contains">The contains.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="inRegion">The in region.</param>
        /// <param name="inPostalCode">The in postal code.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns available phone nu,ber list</returns>
        public AvailablePhoneNumberList ListAvailableNumbers(string country, PhoneNumberType type,
            string contains = null, string areaCode = null, string inRegion = null, string inPostalCode = null, int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListAvailableNumbers(accountSid, country, type, contains, areaCode, inRegion, inPostalCode, page, pageSize);
        }

        /// <summary>
        /// Sets the parameters for list notifications.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="contains">The contains.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="inRegion">The in region.</param>
        /// <param name="inPostalCode">The in postal code.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListNotifications(IRestRequest request, string contains, string areaCode, string inRegion, string inPostalCode, int? page, int? pageSize)
        {
            if (contains.HasValue()) request.AddQueryParameter("Contains", contains);
            if (areaCode.HasValue()) request.AddQueryParameter("AreaCode", areaCode);
            if (inRegion.HasValue()) request.AddQueryParameter("InRegion", inRegion);
            if (inPostalCode.HasValue()) request.AddQueryParameter("InPostalCode", inPostalCode);
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}
