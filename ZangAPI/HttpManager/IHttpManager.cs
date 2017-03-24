using System.Net.Http;

namespace ZangAPI.HttpManager
{
    /// <summary>
    /// Http manager interface
    /// </summary>
    /// <seealso cref="ZangAPI.HttpManager.IHttpProvider" />
    public interface IHttpManager : IHttpProvider
    {
        /// <summary>
        /// Sets the HTTP client.
        /// </summary>
        /// <returns>Returns HttpClient instance</returns>
        HttpClient ResetHttpClient();

        /// <summary>
        /// Disposes the HTTP client.
        /// </summary>
        void DisposeHttpClient();
    }
}
