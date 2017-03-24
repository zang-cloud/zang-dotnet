using ZangAPI.Configuration;
using ZangAPI.HttpManager;

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
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IZangConfiguration Configuration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        /// <param name="configuration">The configuration.</param>
        protected AConnector(IHttpProvider httpProvider, IZangConfiguration configuration)
        {
            this.HttpProvider = httpProvider;
            this.Configuration = configuration;
        }
    }
}
