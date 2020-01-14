using System;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;
using AvayaCPaaS.Configuration;

namespace AvayaCPaaS.ConnectionManager
{
    /// <summary>
    /// Http manager
    /// </summary>
    /// <seealso cref="IHttpManager" />
    public class HttpManager : IHttpManager
    {
        /// <summary>
        /// Gets or sets the rest client.
        /// </summary>
        /// <value>
        /// The rest client.
        /// </value>
        private RestClient RestClient { get; set; }
        
        /// <summary>
        /// Gets or sets the API configuration.
        /// </summary>
        /// <value>
        /// The API configuration.
        /// </value>
        private IAPIConfiguration APIConfiguration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpManager"/> class.
        /// </summary>
        public HttpManager(IAPIConfiguration configuration)
        {
            this.APIConfiguration = configuration;
        }

        /// <summary>
        /// Initializes the HTTP client.
        /// </summary>
        /// <returns></returns>
        private IRestClient InitHttpClient()
        {
            var client = new RestClient();

            this.ConfigureRestClient(client);

            this.RestClient = client;
            return client;
        }


        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <returns>
        /// Returns HttpClient instance
        /// </returns>
        public IRestClient GetHttpClient()
        {
            if (this.RestClient == null)
            {
                return this.InitHttpClient();
            }              

            return this.RestClient;
        }

        /// <summary>
        /// Resets the HTTP client.
        /// </summary>
        /// <returns>Returns reseted RestClient instance</returns>
        public IRestClient ResetHttpClient()
        {
            if (this.RestClient != null)
            {
                this.DisposeHttpClient();
            }               

            return this.InitHttpClient();
        }

        /// <summary>
        /// Disposes the HTTP client.
        /// </summary>
        public void DisposeHttpClient()
        {
            this.RestClient = null;
        }

        /// <summary>
        /// Sets the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void SetConfiguration(IAPIConfiguration configuration)
        {
            this.APIConfiguration = configuration;

            if (this.RestClient == null)
            {
                this.InitHttpClient();
            }
            else
            {
                this.ConfigureRestClient(this.RestClient);
            }                    
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns>
        /// Returns configuration
        /// </returns>
        public IAPIConfiguration GetConfiguration()
        {
            return this.APIConfiguration;
        }

        /// <summary>
        /// Configures the rest client.
        /// </summary>
        /// <param name="client">The client.</param>
        private void ConfigureRestClient(IRestClient client)
        {
            client.BaseUrl = new Uri(APIConfiguration.BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(APIConfiguration.AccountSid, APIConfiguration.AuthToken);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12
                                                   | SecurityProtocolType.Ssl3;

            // If useProxy flag is set to true set proxy
            if (APIConfiguration.UseProxy)
            {
                client.Proxy = new WebProxy(APIConfiguration.ProxyHost + ":" + APIConfiguration.ProxyPort);
                client.Proxy.Credentials = new NetworkCredential(APIConfiguration.AccountSid, APIConfiguration.AuthToken);
            }
        }
    }
}
