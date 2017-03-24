using System;
using System.Net.Http;
using System.Text;
using ZangAPI.Configuration;

namespace ZangAPI.HttpManager
{
    /// <summary>
    /// Http manager
    /// </summary>
    /// <seealso cref="ZangAPI.HttpManager.IHttpManager" />
    public class HttpManager : IHttpManager
    {
        /// <summary>
        /// Gets or sets the HTTP client.
        /// </summary>
        /// <value>
        /// The HTTP client.
        /// </value>
        private HttpClient HttpClient { get; set; }

        /// <summary>
        /// Gets or sets the zang configuration.
        /// </summary>
        /// <value>
        /// The zang configuration.
        /// </value>
        private IZangConfiguration ZangConfiguration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpManager"/> class.
        /// </summary>
        public HttpManager(IZangConfiguration configuration)
        {
            this.ZangConfiguration = configuration;
        }

        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <returns>
        /// Returns HttpClient instance
        /// </returns>
        public HttpClient GetHttpClient()
        {
            // If HttpClient instance already exists return it, create new instance otherwise
            var client = this.HttpClient ?? new HttpClient();

            // Configure HttpClient instance
            this.ConfigureHttpClient(client);

            this.HttpClient = client;
            return this.HttpClient;
        }

        /// <summary>
        /// Sets the HTTP client.
        /// </summary>
        /// <returns>
        /// Returns HttpClient instance
        /// </returns>
        public HttpClient ResetHttpClient()
        {
            // Dispose old HttpClient
            this.HttpClient.Dispose();

            var client = new HttpClient();

            // Configure HttpClient instance
            this.ConfigureHttpClient(client);
            
            // Set HttpClient
            this.HttpClient = client;
            return this.HttpClient;
        }

        /// <summary>
        /// Disposes the HTTP client.
        /// </summary>
        public void DisposeHttpClient()
        {
            this.HttpClient.Dispose();
        }

        /// <summary>
        /// Configures the HTTP client.
        /// </summary>
        /// <param name="client">The client.</param>
        private void ConfigureHttpClient(HttpClient client)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{ZangConfiguration.AccountSid}:{ZangConfiguration.AuthToken}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
