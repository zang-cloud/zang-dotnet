using ZangAPI.ConnectionManager;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// Abstract connector
    /// </summary>
    public abstract class AConnector
    {
        /// <summary>
        /// Gets or sets the HTTP provider.
        /// </summary>
        /// <value>
        /// The HTTP provider.
        /// </value>
        public IHttpProvider HttpProvider { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        protected AConnector(IHttpProvider httpProvider)
        {
            this.HttpProvider = httpProvider;
        }
    }
}
