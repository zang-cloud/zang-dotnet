using RestSharp;
using RestSharp.Validation;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Carrier services connector - used for all forms of communication with the Carrier Services endpoint of the Zang REST API
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class CarrierServicesConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarrierServicesConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public CarrierServicesConnector(IHttpProvider httpProvider)
            : base(httpProvider)
        {
        }

        /// <summary>
        /// The Carrier Lookup API allows you to retrieve additional information about a phone number.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="phoneNumber">Phone number to do a lookup for.</param>
        /// <returns>Returns carrier lookup</returns>
        public CarrierLookup CarrierLookup(string accountSid, string phoneNumber)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Lookups/Carrier.json");

            // Add CarrierLookup query and body parameters
            request.AddParameter("PhoneNumber", phoneNumber);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CarrierLookup>(response);
        }

        /// <summary>
        /// The Carrier Lookup API allows you to retrieve additional information about a phone number. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="phoneNumber">Phone number to do a lookup for.</param>
        /// <returns>Returns carrier lookup</returns>
        public CarrierLookup CarrierLookup(string phoneNumber)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CarrierLookup(accountSid, phoneNumber);
        }

        /// <summary>
        /// Shows info on all carrier lookups associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns carrier lookup list</returns>
        public CarrierLookupsList CarrierLookupList(string accountSid, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Lookups/Carrier.json");

            // Add CarrierLookupList query and body parameters
            this.SetParamsForListLookups(request, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CarrierLookupsList>(response);
        }

        /// <summary>
        /// Shows info on all carrier lookups associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns carrier lookup list</returns>
        public CarrierLookupsList CarrierLookupList(int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CarrierLookupList(accountSid, page, pageSize);
        }

        /// <summary>
        /// Shows a CNAM information on some phone number
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="phoneNumber">Phone number to do a lookup for.</param>
        /// <returns>Returns cnam lookup</returns>
        public CnamLookup CnamLookup(string accountSid, string phoneNumber)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Lookups/Cnam.json");

            // Set CnamLookup body parameter
            Require.Argument("PhoneNumber", phoneNumber);
            request.AddParameter("PhoneNumber", phoneNumber);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CnamLookup>(response);
        }

        /// <summary>
        /// Shows a CNAM information on some phone number. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="phoneNumber">Phone number to do a lookup for.</param>
        /// <returns>Returns cnam lookup</returns>
        public CnamLookup CnamLookup(string phoneNumber)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CnamLookup(accountSid, phoneNumber);
        }

        /// <summary>
        /// Shows info on all CNAM lookups associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns cnam lookup list</returns>
        public CnamLookupsList CnamLookupList(string accountSid, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Lookups/Cnam.json");

            // Add CnamLookupList query and body parameters
            this.SetParamsForListLookups(request, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<CnamLookupsList>(response);
        }

        /// <summary> 
        /// Shows info on all CNAM lookups associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns cnam lookup</returns>
        public CnamLookupsList CnamLookupList(int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CnamLookupList(accountSid, page, pageSize);
        }

        /// <summary>
        /// Shows information on billing name address for some phone number
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="phoneNumber">Phone number to do a lookup for.</param>
        /// <returns>Returns bna lookup</returns>
        public BnaLookup BnaLookup(string accountSid, string phoneNumber)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Lookups/Bna.json");

            // Set BnaLookup body parameter
            Require.Argument("PhoneNumber", phoneNumber);
            request.AddParameter("PhoneNumber", phoneNumber);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<BnaLookup>(response);
        }

        /// <summary>
        /// Shows information on billing name address for some phone number. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="phoneNumber">Phone number to do a lookup for.</param>
        /// <returns>Returns bna lookup</returns>
        public BnaLookup BnaLookup(string phoneNumber)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.BnaLookup(accountSid, phoneNumber);
        }

        /// <summary>
        /// Shows info on all BNA lookups associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns bna lookup list</returns>
        public BnaLookupsList BnaLookupList(string accountSid, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Lookups/Bna.json");

            // Add BnaLookupList query and body parameters
            this.SetParamsForListLookups(request, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<BnaLookupsList>(response);
        }

        /// <summary>
        /// Shows info on all BNA lookups associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns bna lookup list</returns>
        public BnaLookupsList BnaLookupList(int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.BnaLookupList(accountSid, page, pageSize);
        }

        /// <summary>
        /// Sets the parameters for list lookups.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListLookups(IRestRequest request, int? page, int? pageSize)
        {
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}