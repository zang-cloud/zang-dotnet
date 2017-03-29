using System;
using RestSharp;
using RestSharp.Authenticators;
using ZangAPI.Configuration;

namespace ZangAPI.ConnectionManager
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
        /// Gets or sets the zang configuration.
        /// </summary>
        /// <value>
        /// The zang configuration.
        /// </value>
        private IZangConfiguration ZangConfiguration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpManager"/> class.
        /// </summary>
        public HttpManager(IZangConfiguration configuration)
        {
            this.ZangConfiguration = configuration;
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
        public void SetConfiguration(IZangConfiguration configuration)
        {
            this.ZangConfiguration = configuration;

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
        /// Configures the rest client.
        /// </summary>
        /// <param name="client">The client.</param>
        private void ConfigureRestClient(IRestClient client)
        {
            client.BaseUrl = new Uri(ZangConfiguration.BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(ZangConfiguration.AccountSid, ZangConfiguration.AuthToken);
        }
    }
}
