using RestSharp;

namespace ZangAPI.HttpManager
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
