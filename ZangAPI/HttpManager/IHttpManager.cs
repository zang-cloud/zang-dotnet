using RestSharp;
using ZangAPI.Configuration;

namespace ZangAPI.HttpManager
{
    /// <summary>
    /// Http manager interface
    /// </summary>
    /// <seealso cref="ZangAPI.HttpManager.IHttpProvider" />
    public interface IHttpManager : IHttpProvider
    {
        /// <summary>
        /// Sets the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void SetConfiguration(IZangConfiguration configuration);

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
