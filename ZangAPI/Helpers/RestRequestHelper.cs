using RestSharp;

namespace ZangAPI.Helpers
{
    /// <summary>
    /// Rest request helper
    /// </summary>
    public static class RestRequestHelper
    {
        /// <summary>
        /// Creates the rest request.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="url">The URL.</param>
        /// <returns>Returns created request</returns>
        public static RestRequest CreateRestRequest(Method method, string url)
        {
            // Create {method} request
            var request = new RestRequest(method);

            // Set url
            request.Resource = url;

            return request;
        }
    }
}
