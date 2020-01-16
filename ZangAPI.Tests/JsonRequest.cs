using System.Collections.Generic;

namespace AvayaCPaaS.Tests
{
    /// <summary>
    /// Request defined in json file
    /// </summary>
    public class JsonRequest
    {
        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the body parameters.
        /// </summary>
        /// <value>
        /// The body parameters.
        /// </value>
        public List<Parameter> BodyParams { get; set; }

        /// <summary>
        /// Gets or sets the query parameters.
        /// </summary>
        /// <value>
        /// The query parameters.
        /// </value>
        public List<Parameter> QueryParams { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public string Response { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRequest"/> class.
        /// </summary>
        public JsonRequest()
        {
            this.BodyParams = new List<Parameter>();
            this.QueryParams = new List<Parameter>();
        }      
    }
}