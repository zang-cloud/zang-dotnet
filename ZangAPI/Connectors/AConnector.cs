using Newtonsoft.Json;
using RestSharp;
using ZangAPI.ConnectionManager;
using ZangAPI.Exceptions;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Abstract connector
    /// </summary>
    public abstract class AConnector
    {
        /// <summary>
        /// Gets or sets the HTTP provider.
        /// </summary>
        /// <value>
        /// The HTTP provider.
        /// </value>
        public IHttpProvider HttpProvider { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        protected AConnector(IHttpProvider httpProvider)
        {
            this.HttpProvider = httpProvider;
        }

        /// <summary>
        /// Returns the or throw exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns>Returns instance of class T or throws exception</returns>
        public T ReturnOrThrowException<T>(IRestResponse response)
        {
            var status = (int)response.StatusCode;

            if (status >= 400) {
                throw JsonConvert.DeserializeObject<ZangException>(response.Content);
            }
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
