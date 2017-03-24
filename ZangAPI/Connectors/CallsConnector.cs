using System.Net;
using Newtonsoft.Json;
using ZangAPI.Configuration;
using ZangAPI.HttpManager;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Calls connector
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class CallsConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallsConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        /// <param name="configuration">The configuration.</param>
        public CallsConnector(IHttpProvider httpProvider, IZangConfiguration configuration) 
            : base(httpProvider, configuration)
        {
        }

        //todo MakeCall

        //todo ViewCall

        public CallList ListCalls(string accountSid)
        {
            //todo maknuti ovo iz Zanga
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | SecurityProtocolType.Ssl3;

            var url = $"{Configuration.BaseUrl}/Accounts/{accountSid}/Calls.json";

            var response = HttpProvider.GetHttpClient().GetAsync(url).Result;

            var responseContent = response.Content;

            // Call .Result to synchronously read the result
            var responseString = responseContent.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<CallList>(responseString);
        }

        //todo InterruptLiveCall

        //todo SendDigitsToLiveCall

        //todo RecordLiveCall

        //todo PlayAudioToLiveCall

        //todo ApplyVoiceEffect
    }
}
