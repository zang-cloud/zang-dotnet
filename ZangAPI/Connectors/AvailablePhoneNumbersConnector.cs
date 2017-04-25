using RestSharp;
using RestSharp.Extensions;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Available phone numbers connector - used for all forms of communication with the Available Phone Numbers endpoint of the Zang REST API
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
        /// Shows information on all phone numbers available for purchasing
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="country">Two letter country code.</param>
        /// <param name="type">Type of the phone number. Can be Local or Tollfree</param>
        /// <param name="contains">Specifies the desired characters contained within the available numbers to list.</param>
        /// <param name="areaCode">Specifies the area code that the returned list of available numbers should be in. Only available for North American numbers</param>
        /// <param name="inRegion">Specifies the desired region of the available numbers to be listed.</param>
        /// <param name="inPostalCode">Specifies the desired postal code of the available numbers to be listed.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns available phone number list</returns>
        public AvailablePhoneNumbersList ListAvailableNumbers(string accountSid, string country,
            AvailablePhoneNumberType type,
            string contains = null, string areaCode = null, string inRegion = null, string inPostalCode = null,
            int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            var typeString = EnumHelper.GetEnumValue(type);

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/AvailablePhoneNumbers/{country}/{typeString}.json");

            // Add ListAvailableNumbers query and body parameters
            this.SetParamsForListAvailablePhoneNumbers(request, contains, areaCode, inRegion, inPostalCode, page,
                pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<AvailablePhoneNumbersList>(response);
        }

        /// <summary>
        /// Shows information on all phone numbers available for purchasing. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="country">Two letter country code.</param>
        /// <param name="type">Type of the phone number. Can be Local or Tollfree</param>
        /// <param name="contains">Specifies the desired characters contained within the available numbers to list.</param>
        /// <param name="areaCode">Specifies the area code that the returned list of available numbers should be in. Only available for North American numbers</param>
        /// <param name="inRegion">Specifies the desired region of the available numbers to be listed.</param>
        /// <param name="inPostalCode">Specifies the desired postal code of the available numbers to be listed.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns available phone nu,ber list</returns>
        public AvailablePhoneNumbersList ListAvailableNumbers(string country, AvailablePhoneNumberType type,
            string contains = null, string areaCode = null, string inRegion = null, string inPostalCode = null,
            int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListAvailableNumbers(accountSid, country, type, contains, areaCode, inRegion, inPostalCode, page,
                pageSize);
        }

        /// <summary>
        /// Sets the parameters for list available phone numbers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="contains">The contains.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="inRegion">The in region.</param>
        /// <param name="inPostalCode">The in postal code.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListAvailablePhoneNumbers(IRestRequest request, string contains, string areaCode,
            string inRegion, string inPostalCode, int? page, int? pageSize)
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