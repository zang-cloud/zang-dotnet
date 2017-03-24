using System.Net.Http;

namespace ZangAPI.HttpManager
{
    /// <summary>
    /// Http provider interface
    /// </summary>
    public interface IHttpProvider
    {
        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <returns>Returns HttpClient instance</returns>
        HttpClient GetHttpClient();
    }
}
