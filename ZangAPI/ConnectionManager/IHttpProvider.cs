using RestSharp;
using AvayaCPaaS.Configuration;

namespace AvayaCPaaS.ConnectionManager
{
    /// <summary>
    /// Http provider interface
    /// </summary>
    public interface IHttpProvider
    {
        /// <summary>
        /// Gets the rest client.
        /// </summary>
        /// <returns>Returns rest client</returns>
        IRestClient GetHttpClient();

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns>Returns configuration</returns>
        IAPIConfiguration GetConfiguration();
    }
}
