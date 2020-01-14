using System.Collections.Generic;
using Newtonsoft.Json;

namespace AvayaCPaaS.Model.Lists
{
    /// <summary>
    /// List of API objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectsList<T>
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        /// <summary>
        /// Gets the numpages.
        /// </summary>
        /// <value>
        /// The numpages.
        /// </value>
        [JsonProperty(PropertyName = "num_pages")]
        public int Numpages { get; set; }

        /// <summary>
        /// Gets the pagesize.
        /// </summary>
        /// <value>
        /// The pagesize.
        /// </value>
        [JsonProperty(PropertyName = "page_size")]
        public int Pagesize { get; set; }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }

        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        [JsonProperty(PropertyName = "end")]
        public int End { get; set; }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Gets the first page URI.
        /// </summary>
        /// <value>
        /// The first page URI.
        /// </value>
        [JsonProperty(PropertyName = "first_page_uri")]
        public string FirstPageUri { get; set; }

        /// <summary>
        /// Gets the previous page URI.
        /// </summary>
        /// <value>
        /// The previous page URI.
        /// </value>
        [JsonProperty(PropertyName = "previous_page_uri")]
        public string PreviousPageUri { get; set; }

        /// <summary>
        /// Gets the next page URI.
        /// </summary>
        /// <value>
        /// The next page URI.
        /// </value>
        [JsonProperty(PropertyName = "next_page_uri")]
        public string NextPageUri { get; set; }

        /// <summary>
        /// Gets the last page URI.
        /// </summary>
        /// <value>
        /// The last page URI.
        /// </value>
        [JsonProperty(PropertyName = "last_page_uri")]
        public string LastPageUri { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public virtual ICollection<T> Elements { get; set; }
    }
}
