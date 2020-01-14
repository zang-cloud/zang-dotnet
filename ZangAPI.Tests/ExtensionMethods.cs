using System.Linq;

namespace AvayaCPaaS.Tests
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Bodies the parameter.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>Returns value of body parameter</returns>
        public static string BodyParameter(this JsonRequest request, string parameterName)
        {
            return request.BodyParams.FirstOrDefault(p => p.Name.Equals(parameterName)).Value;
        }

        /// <summary>
        /// Queries the parameter.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>Returns value of query parameter</returns>
        public static string QueryParameter(this JsonRequest request, string parameterName)
        {
            return request.QueryParams.FirstOrDefault(p => p.Name.Equals(parameterName)).Value;
        }
    }
}
