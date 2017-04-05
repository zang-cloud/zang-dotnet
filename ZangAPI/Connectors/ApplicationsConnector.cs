using RestSharp;
using RestSharp.Extensions;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Applications connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class ApplicationsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public ApplicationsConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Views the application.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">The application sid.</param>
        /// <returns>Returns application</returns>
        public Application ViewApplication(string accountSid, string applicationSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Applications/{applicationSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Application>(response);
        }

        /// <summary>
        /// Views the application. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">The application sid.</param>
        /// <returns>Returns application</returns>
        public Application ViewApplication(string applicationSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewApplication(accountSid, applicationSid);
        }

        /// <summary>
        /// Creates the application.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="voiceCallerIdLookup">if set to <c>true</c> [voice caller identifier lookup].</param>
        /// <param name="smsUrl">The SMS URL.</param>
        /// <param name="smsMethod">The SMS method.</param>
        /// <param name="smsFallbackUrl">The SMS fallback URL.</param>
        /// <param name="smsFallbackMethod">The SMS fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="hangupCallback">The hangup callback.</param>
        /// <param name="hangupCallbackMethod">The hangup callback method.</param>
        /// <returns>Returns created application</returns>
        public Application CreateApplication(string accountSid, string friendlyName = null, string voiceUrl = null, HttpMethod voiceMethod = HttpMethod.POST, 
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST, bool voiceCallerIdLookup = false, string smsUrl = null, 
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null, HttpMethod smsFallbackMethod = HttpMethod.POST, string hearbeatUrl = null,
            HttpMethod hearbeatMethod = HttpMethod.POST, string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST, string hangupCallback = null,
            HttpMethod hangupCallbackMethod = HttpMethod.POST)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Applications.json");

            // Add CreateApplication query and body parameters
            this.SetParamsForCreateOrUpdateApplication(request, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl, voiceFallbackMethod, voiceCallerIdLookup, smsUrl, smsMethod, smsFallbackUrl, smsFallbackMethod, hearbeatUrl, 
                hearbeatMethod, statusCallback, statusCallbackMethod, hangupCallback, hangupCallbackMethod);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Application>(response);
        }

        /// <summary>
        /// Creates the application. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="voiceCallerIdLookup">if set to <c>true</c> [voice caller identifier lookup].</param>
        /// <param name="smsUrl">The SMS URL.</param>
        /// <param name="smsMethod">The SMS method.</param>
        /// <param name="smsFallbackUrl">The SMS fallback URL.</param>
        /// <param name="smsFallbackMethod">The SMS fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="hangupCallback">The hangup callback.</param>
        /// <param name="hangupCallbackMethod">The hangup callback method.</param>
        /// <returns>Returns created application</returns>
        public Application CreateApplication(string friendlyName = null, string voiceUrl = null, HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST, bool voiceCallerIdLookup = false, string smsUrl = null,
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null, HttpMethod smsFallbackMethod = HttpMethod.POST, string hearbeatUrl = null,
            HttpMethod hearbeatMethod = HttpMethod.POST, string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST, string hangupCallback = null,
            HttpMethod hangupCallbackMethod = HttpMethod.POST)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.CreateApplication(accountSid, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl, voiceFallbackMethod, voiceCallerIdLookup, smsUrl, smsMethod, smsFallbackUrl, smsFallbackMethod, hearbeatUrl,
                hearbeatMethod, statusCallback, statusCallbackMethod, hangupCallback, hangupCallbackMethod);
        }

        /// <summary>
        /// Updates the application.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">The application sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="voiceCallerIdLookup">if set to <c>true</c> [voice caller identifier lookup].</param>
        /// <param name="smsUrl">The SMS URL.</param>
        /// <param name="smsMethod">The SMS method.</param>
        /// <param name="smsFallbackUrl">The SMS fallback URL.</param>
        /// <param name="smsFallbackMethod">The SMS fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="hangupCallback">The hangup callback.</param>
        /// <param name="hangupCallbackMethod">The hangup callback method.</param>
        /// <returns>Returns updated application</returns>
        public Application UpdateApplication(string accountSid, string applicationSid,            
            string friendlyName = null, string voiceUrl = null, HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST, bool voiceCallerIdLookup = false, string smsUrl = null,
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null, HttpMethod smsFallbackMethod = HttpMethod.POST, string hearbeatUrl = null,
            HttpMethod hearbeatMethod = HttpMethod.POST, string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST, string hangupCallback = null,
            HttpMethod hangupCallbackMethod = HttpMethod.POST)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Applications/{applicationSid}.json");

            // Add UpdateApplication query and body parameters
            this.SetParamsForCreateOrUpdateApplication(request, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl, voiceFallbackMethod, voiceCallerIdLookup, smsUrl, smsMethod, smsFallbackUrl, smsFallbackMethod, hearbeatUrl,
                hearbeatMethod, statusCallback, statusCallbackMethod, hangupCallback, hangupCallbackMethod);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Application>(response);
        }

        /// <summary>
        /// Updates the application. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">The application sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="voiceCallerIdLookup">if set to <c>true</c> [voice caller identifier lookup].</param>
        /// <param name="smsUrl">The SMS URL.</param>
        /// <param name="smsMethod">The SMS method.</param>
        /// <param name="smsFallbackUrl">The SMS fallback URL.</param>
        /// <param name="smsFallbackMethod">The SMS fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="hangupCallback">The hangup callback.</param>
        /// <param name="hangupCallbackMethod">The hangup callback method.</param>
        /// <returns>Returns updated application</returns>
        public Application UpdateApplication(string applicationSid, string friendlyName = null, string voiceUrl = null, HttpMethod voiceMethod = HttpMethod.POST,
            string voiceFallbackUrl = null, HttpMethod voiceFallbackMethod = HttpMethod.POST, bool voiceCallerIdLookup = false, string smsUrl = null,
            HttpMethod smsMethod = HttpMethod.POST, string smsFallbackUrl = null, HttpMethod smsFallbackMethod = HttpMethod.POST, string hearbeatUrl = null,
            HttpMethod hearbeatMethod = HttpMethod.POST, string statusCallback = null, HttpMethod statusCallbackMethod = HttpMethod.POST, string hangupCallback = null,
            HttpMethod hangupCallbackMethod = HttpMethod.POST)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.UpdateApplication(accountSid, applicationSid, friendlyName, voiceUrl, voiceMethod, voiceFallbackUrl, voiceFallbackMethod, voiceCallerIdLookup, smsUrl, smsMethod, smsFallbackUrl, smsFallbackMethod, hearbeatUrl,
                hearbeatMethod, statusCallback, statusCallbackMethod, hangupCallback, hangupCallbackMethod);
        }

        /// <summary>
        /// Deletes the application.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="applicationSid">The application sid.</param>
        /// <returns>Returns deleted application</returns>
        public Application DeleteApplication(string accountSid, string applicationSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE, $"Accounts/{accountSid}/Applications/{applicationSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Application>(response);
        }

        /// <summary>
        /// Deletes the application. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="applicationSid">The application sid.</param>
        /// <returns>Returns deleted application</returns>
        public Application DeleteApplication(string applicationSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.DeleteApplication(accountSid, applicationSid);
        }

        /// <summary>
        /// Lists the applications.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns list of applications</returns>
        public ApplicationList ListApplications(string accountSid, string friendlyName = null, int? page = null,
            int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Applications.json");

            // Add ListApplications query and body parameters
            this.SetParamsForListApplications(request, friendlyName, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ApplicationList>(response);
        }

        /// <summary>
        /// Lists the applications. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns application list</returns>
        public ApplicationList ListApplications(string friendlyName = null, int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListApplications(accountSid, friendlyName, page, pageSize);
        }

        /// <summary>
        /// Sets the parameters for create or update application.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="voiceUrl">The voice URL.</param>
        /// <param name="voiceMethod">The voice method.</param>
        /// <param name="voiceFallbackUrl">The voice fallback URL.</param>
        /// <param name="voiceFallbackMethod">The voice fallback method.</param>
        /// <param name="voiceCallerIdLookup">if set to <c>true</c> [voice caller identifier lookup].</param>
        /// <param name="smsUrl">The SMS URL.</param>
        /// <param name="smsMethod">The SMS method.</param>
        /// <param name="smsFallbackUrl">The SMS fallback URL.</param>
        /// <param name="smsFallbackMethod">The SMS fallback method.</param>
        /// <param name="hearbeatUrl">The hearbeat URL.</param>
        /// <param name="hearbeatMethod">The hearbeat method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="statusCallbackMethod">The status callback method.</param>
        /// <param name="hangupCallback">The hangup callback.</param>
        /// <param name="hangupCallbackMethod">The hangup callback method.</param>
        private void SetParamsForCreateOrUpdateApplication(IRestRequest request, string friendlyName, string voiceUrl, HttpMethod voiceMethod,
            string voiceFallbackUrl, HttpMethod voiceFallbackMethod, bool voiceCallerIdLookup, string smsUrl,
            HttpMethod smsMethod, string smsFallbackUrl, HttpMethod smsFallbackMethod, string hearbeatUrl,
            HttpMethod hearbeatMethod, string statusCallback, HttpMethod statusCallbackMethod, string hangupCallback,
            HttpMethod hangupCallbackMethod)
        {
            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
            if (voiceUrl.HasValue()) request.AddParameter("VoiceUrl", voiceUrl);
            request.AddParameter("VoiceMethod", voiceMethod.ToString().ToUpper());
            if (voiceFallbackUrl.HasValue()) request.AddParameter("VoiceFallbackUrl", voiceFallbackUrl);
            request.AddParameter("VoiceFallbackMethod", voiceFallbackMethod.ToString().ToUpper());
            request.AddParameter("VoiceCallerIdLookup", voiceCallerIdLookup.ToString());
            if (smsUrl.HasValue()) request.AddParameter("SmsUrl", smsUrl);
            request.AddParameter("SmsMethod", smsMethod.ToString().ToUpper());
            if (smsFallbackUrl.HasValue()) request.AddParameter("SmsFallbackUrl", smsFallbackUrl);
            request.AddParameter("SmsFallbackMethod", smsFallbackMethod.ToString().ToUpper());
            if (hearbeatUrl.HasValue()) request.AddParameter("HeartbeatUrl", hearbeatUrl);
            request.AddParameter("HeartbeatMethod", hearbeatMethod.ToString().ToUpper());
            if (statusCallback.HasValue()) request.AddParameter("StatusCallback", statusCallback);
            request.AddParameter("StatusCallbackMethod", statusCallbackMethod.ToString().ToUpper());
            if (hangupCallback.HasValue()) request.AddParameter("HangupCallback", hangupCallback);
            request.AddParameter("HangupCallbackMethod", hangupCallbackMethod.ToString().ToUpper());
        }

        /// <summary>
        /// Sets the parameters for list applications.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListApplications(IRestRequest request, string friendlyName, int? page, int? pageSize)
        {
            if (friendlyName.HasValue()) request.AddQueryParameter("FriendlyName", friendlyName);
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }
    }
}
