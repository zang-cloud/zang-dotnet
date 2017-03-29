using RestSharp;

namespace ZangAPI.ConnectionManager
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
    }
}
