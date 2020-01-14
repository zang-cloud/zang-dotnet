using System.Collections.Generic;
using RestSharp;
using AvayaCPaaS.ConnectionManager;
using AvayaCPaaS.Helpers;
using AvayaCPaaS.Model;
using AvayaCPaaS.Model.Enums;
using AvayaCPaaS.Model.Lists;

namespace AvayaCPaaS.Connectors
{
    /// <summary>
    /// Fraud control connector - used for all forms of communication with the Fraud Control endpoint of the Avaya CPaaS REST API
    /// </summary>
    /// <seealso cref="AvayaCPaaS.Connectors.AConnector" />
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
        /// Restricts outbound calls and sms messages to some destination
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="countryCode">Country code.</param>
        /// <param name="mobileEnabled">Mobile status for the destination. If false, all mobile call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="landlineEnabled">Landline status for the destination. If false, all landline call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="smsEnabled">SMS status for the destination. If false, all SMS activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule BlockDestination(string accountSid, string countryCode,
            bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Fraud/Block/{countryCode}.json");

            // Add BlockDestination query and body parameters
            this.SetParamsForBlockOrAuthorizeDestination(request, mobileEnabled, landlineEnabled, smsEnabled);

            // Send request
            var response = client.Execute(request);

            var fraudControlRuleElement = this.ReturnOrThrowException<FraudControlRuleElement>(response);

            // If blocked part of a fraud control rule element is null return null
            if (fraudControlRuleElement.Blocked == null)
            {
                return null;
            }

            // Get blocked part of fraud control rule element and set type
            var fraudControlRuleBlocked = fraudControlRuleElement.Blocked;
            fraudControlRuleBlocked.Type = FraudControlType.BLOCKED;

            return fraudControlRuleBlocked;
        }

        /// <summary>
        /// Restricts outbound calls and sms messages to some destination. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="countryCode">Country code.</param>
        /// <param name="mobileEnabled">Mobile status for the destination. If false, all mobile call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="landlineEnabled">Landline status for the destination. If false, all landline call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="smsEnabled">SMS status for the destination. If false, all SMS activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule BlockDestination(string countryCode, bool mobileEnabled = true,
            bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.BlockDestination(accountSid, countryCode, mobileEnabled, landlineEnabled, smsEnabled);
        }

        /// <summary>
        /// Authorizes previously blocked destination for outbound calls and sms messages
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="countryCode">Country code.</param>
        /// <param name="mobileEnabled">Mobile status for the destination. If false, all mobile call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="landlineEnabled">Landline status for the destination. If false, all landline call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="smsEnabled">SMS status for the destination. If false, all SMS activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule AuthorizeDestination(string accountSid, string countryCode,
            bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Fraud/Authorize/{countryCode}.json");

            // Add AuthorizeDestination query and body parameters
            this.SetParamsForBlockOrAuthorizeDestination(request, mobileEnabled, landlineEnabled, smsEnabled);

            // Send request
            var response = client.Execute(request);

            var fraudControlRuleElement = this.ReturnOrThrowException<FraudControlRuleElement>(response);

            // If authorized part of a fraud control rule element is null return null
            if (fraudControlRuleElement.Authorized == null)
            {
                return null;
            }

            // Get authorized part of fraud control rule element and set type
            var fraudControlRuleAuthorized = fraudControlRuleElement.Authorized;
            fraudControlRuleAuthorized.Type = FraudControlType.AUTHORIZED;

            return fraudControlRuleAuthorized;
        }

        /// <summary>
        /// Authorizes previously blocked destination for outbound calls and sms messages. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="countryCode">Country code.</param>
        /// <param name="mobileEnabled">Mobile status for the destination. If false, all mobile call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="landlineEnabled">Landline status for the destination. If false, all landline call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="smsEnabled">SMS status for the destination. If false, all SMS activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule AuthorizeDestination(string countryCode, bool mobileEnabled = true,
            bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.AuthorizeDestination(accountSid, countryCode, mobileEnabled, landlineEnabled, smsEnabled);
        }

        /// <summary>
        /// Extends a destinations authorization expiration by 30 days
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="countryCode">Country code.</param>
        /// <returns>Returns fraud contorl rule</returns>
        public FraudControlRule ExtendDestinationAuthorization(string accountSid, string countryCode)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Fraud/Extend/{countryCode}.json");

            // Send request
            var response = client.Execute(request);

            var fraudControlRuleElement = this.ReturnOrThrowException<FraudControlRuleElement>(response);

            // If authorized part of a fraud control rule element is null return null
            if (fraudControlRuleElement.Authorized == null)
            {
                return null;
            }

            // Get authorized part of fraud control rule element and set type
            var fraudControlRuleAuthorized = fraudControlRuleElement.Authorized;
            fraudControlRuleAuthorized.Type = FraudControlType.AUTHORIZED;

            return fraudControlRuleAuthorized;
        }

        /// <summary>
        /// Extends a destinations authorization expiration by 30 days. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="countryCode">Country code.</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule ExtendDestinationAuthorization(string countryCode)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ExtendDestinationAuthorization(accountSid, countryCode);
        }

        /// <summary>
        /// Permanently authorizes destination that may have been blocked by our automated fraud detection system
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="countryCode">Country code.</param>
        /// <param name="mobileEnabled">Mobile status for the destination. If false, all mobile call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="landlineEnabled">Landline status for the destination. If false, all landline call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="smsEnabled">SMS status for the destination. If false, all SMS activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule WhitelistDestination(string accountSid, string countryCode,
            bool mobileEnabled = true, bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Fraud/Whitelist/{countryCode}.json");

            // Add WhitelistDestination query and body parameters
            this.SetParamsForWhitelistDestination(request, mobileEnabled, landlineEnabled, smsEnabled);

            // Send request
            var response = client.Execute(request);

            var fraudControlRuleElement = this.ReturnOrThrowException<FraudControlRuleElement>(response);

            // If whitelisted part of a fraud control rule element is null return null
            if (fraudControlRuleElement.Whitelisted == null)
            {
                return null;
            }

            // Get whitelisted part of fraud control rule element and set type
            var fraudControlRuleWhitelisted = fraudControlRuleElement.Whitelisted;
            fraudControlRuleWhitelisted.Type = FraudControlType.WHITELISTED;

            return fraudControlRuleWhitelisted;
        }

        /// <summary>
        /// Permanently authorizes destination that may have been blocked by our automated fraud detection system.
        ///  Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="countryCode">Country code.</param>
        /// <param name="mobileEnabled">Mobile status for the destination. If false, all mobile call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="landlineEnabled">Landline status for the destination. If false, all landline call activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <param name="smsEnabled">SMS status for the destination. If false, all SMS activity will be rejected or disabled. Allowed values are "true" and "false".</param>
        /// <returns>Returns fraud control rule</returns>
        public FraudControlRule WhitelistDestination(string countryCode, bool mobileEnabled = true,
            bool landlineEnabled = true, bool smsEnabled = true)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.WhitelistDestination(accountSid, countryCode, mobileEnabled, landlineEnabled, smsEnabled);
        }

        /// <summary>
        /// Shows information on all fraud control resources associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns fraud control rules list</returns>
        public FraudControlRulesList ListFraudControlResources(string accountSid, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Fraud.json");

            // Add ListFraudControlResources query and body parameters
            this.SetParamsForListFraudControlResources(request, page, pageSize);

            // Send request
            var response = client.Execute(request);

            var fraudControlRules = this.ReturnOrThrowException<FraudControlRulesList>(response);

            // If fraud control rules are null return null
            if (fraudControlRules == null)
            {
                return null;
            }

            fraudControlRules.Authorized = new List<FraudControlRule>();
            fraudControlRules.Blocked = new List<FraudControlRule>();
            fraudControlRules.Whitelisted = new List<FraudControlRule>();

            foreach (var element in fraudControlRules.Elements)
            {
                // Determine which type of rule is the rule and add it to appropriate list
                var authorizedRule = element.Authorized;
                var blockedRule = element.Blocked;
                var whitelistedRule = element.Whitelisted;

                if (authorizedRule != null)
                {
                    authorizedRule.Type = FraudControlType.AUTHORIZED;
                    fraudControlRules.Authorized.Add(authorizedRule);
                }
                else if (blockedRule != null)
                {
                    blockedRule.Type = FraudControlType.BLOCKED;
                    fraudControlRules.Blocked.Add(blockedRule);
                }
                else if (whitelistedRule != null)
                {
                    whitelistedRule.Type = FraudControlType.WHITELISTED;
                    fraudControlRules.Whitelisted.Add(whitelistedRule);
                }
            }

            return fraudControlRules;
        }

        /// <summary>
        /// Shows information on all fraud control resources associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns fraud control rules list</returns>
        public FraudControlRulesList ListFraudControlResources(int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListFraudControlResources(accountSid, page, pageSize);
        }

        /// <summary>
        /// Sets the parameters for block or authorize destination.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        private void SetParamsForBlockOrAuthorizeDestination(IRestRequest request, bool mobileEnabled,
            bool landlineEnabled,
            bool smsEnabled)
        {
            request.AddParameter("MobileEnabled", mobileEnabled.ToString());
            request.AddParameter("LandlineEnabled", landlineEnabled.ToString());
            request.AddParameter("SmsEnabled", smsEnabled.ToString());
        }

        /// <summary>
        /// Sets the parameters for whitelist destination.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="mobileEnabled">if set to <c>true</c> [mobile enabled].</param>
        /// <param name="landlineEnabled">if set to <c>true</c> [landline enabled].</param>
        /// <param name="smsEnabled">if set to <c>true</c> [SMS enabled].</param>
        private void SetParamsForWhitelistDestination(IRestRequest request, bool mobileEnabled, bool landlineEnabled,
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