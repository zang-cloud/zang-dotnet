using System.Net;
using Newtonsoft.Json;
using RestSharp;
using ZangAPI.ConnectionManager;
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
        public CallsConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        //todo MakeCall

        //todo ViewCall

        public CallList ListCalls(string accountSid)
        {
            var url = $"Accounts/{accountSid}/Calls.json";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | SecurityProtocolType.Ssl3;



            var client = HttpProvider.GetHttpClient();

            var request = new RestRequest(url, Method.GET);
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<CallList>(response.Content);
        }

        //todo InterruptLiveCall

        //todo SendDigitsToLiveCall

        //todo RecordLiveCall

        //todo PlayAudioToLiveCall

        //todo ApplyVoiceEffect
    }
}
