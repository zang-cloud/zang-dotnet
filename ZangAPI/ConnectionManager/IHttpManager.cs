using RestSharp;
using AvayaCPaaS.Configuration;

namespace AvayaCPaaS.ConnectionManager
{
    /// <summary>
    /// Http manager interface
    /// </summary>
    /// <seealso cref="IHttpProvider" />
    public interface IHttpManager : IHttpProvider
    {
        /// <summary>
        /// Sets the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void SetConfiguration(IAPIConfiguration configuration);

        /// <summary>
        /// Resets the rest client.
        /// </summary>
        /// <returns>Returns rest client instance</returns>
        IRestClient ResetHttpClient();

        /// <summary>
        /// Disposes the HTTP client.
        /// </summary>
        void DisposeHttpClient();
    }
}
