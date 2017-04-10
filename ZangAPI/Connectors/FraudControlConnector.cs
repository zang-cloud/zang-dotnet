using RestSharp;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Fraud control connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class FraudControlConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FraudControlConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public FraudControlConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Blocks the destination.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule BlockDestination(string accountSid, string countryCode, 
            bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Fraud/Block/{countryCode}.json");

            // Add BlockDestination query and body parameters
            this.SetParamsForBlockOrAuthorizeOrWhitelistDestination(request, mobileEnabled, landlineEnabled, smsEnabled);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<FraudControlRule>(response);
        }

        /// <summary>
        /// Blocks the destination. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule BlockDestination(string countryCode, bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.BlockDestination(accountSid, countryCode, mobileEnabled, landlineEnabled, smsEnabled);
        }

        /// <summary>
        /// Authorizes the destination.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule AuthorizeDestination(string accountSid, string countryCode,
            bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Fraud/Authorize/{countryCode}.json");

            // Add AuthorizeDestination query and body parameters
            this.SetParamsForBlockOrAuthorizeOrWhitelistDestination(request, mobileEnabled, landlineEnabled, smsEnabled);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<FraudControlRule>(response);
        }

        /// <summary>
        /// Authorizes the destination. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule AuthorizeDestination(string countryCode, bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.AuthorizeDestination(accountSid, countryCode, mobileEnabled, landlineEnabled, smsEnabled);
        }

        /// <summary>
        /// Extends the destination authorization.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="countryCode">The country code.</param>
        /// <returns>Returns fraud contorl rule</returns>
        public FraudControlRule ExtendDestinationAuthorization(string accountSid, string countryCode)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Fraud/Extend/{countryCode}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<FraudControlRule>(response);
        }

        /// <summary>
        /// Extends the destination authorization. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule ExtendDestinationAuthorization(string countryCode)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ExtendDestinationAuthorization(accountSid, countryCode);
        }

        //todo kako get?        
        /// <summary>
        /// Whitelists the destination.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule WhitelistDestination(string accountSid, string countryCode,
            bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Fraud/Whitelist/{countryCode}.json");

            // Add WhitelistDestination query and body parameters
            this.SetParamsForBlockOrAuthorizeOrWhitelistDestination(request, mobileEnabled, landlineEnabled, smsEnabled);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<FraudControlRule>(response);
        }

        /// <summary>
        /// Whitelists the destination. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule WhitelistDestination(string countryCode, bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.WhitelistDestination(accountSid, countryCode, mobileEnabled, landlineEnabled, smsEnabled);
        }

        /// <summary>
        /// Lists the fraud control resources.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns fraud control rules list</returns>
        public FraudControlRuleList ListFraudControlResources(string accountSid, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Fraud.json");

            // Add ListFraudControlResources query and body parameters
            this.SetParamsForListFraudControlResources(request, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<FraudControlRuleList>(response);
        }

        /// <summary>
        /// Lists the fraud control resources. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns fraud control rules list</returns>
        public FraudControlRuleList ListFraudControlResources(int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListFraudControlResources(accountSid, page, pageSize);
        }

        //todo ime?
        /// <summary>
        /// Sets the parameters for block destination.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        private void SetParamsForBlockOrAuthorizeOrWhitelistDestination(IRestRequest request, bool mobileEnabled, bool landlineEnabled,
            bool smsEnabled)
        {
            request.AddQueryParameter("MobileEnabled", mobileEnabled.ToString());
            request.AddQueryParameter("LandlineEnabled", landlineEnabled.ToString());
            request.AddQueryParameter("SmsEnabled", smsEnabled.ToString());
        }

        /// <summary>
        /// Sets the parameters for list fraud control resources.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListFraudControlResources(IRestRequest request, int? page, int? pageSize)
        {
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}
