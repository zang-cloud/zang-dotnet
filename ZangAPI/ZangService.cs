using System;
using ZangAPI.Configuration;
using ZangAPI.Connectors;
using ZangAPI.HttpManager;

namespace ZangAPI
{
    /// <summary>
    /// Zang service
    /// </summary>
    public class ZangService
    {
        /// <summary>
        /// Gets the zang configuration.
        /// </summary>
        /// <value>
        /// The zang configuration.
        /// </value>
        public ZangConfiguration ZangConfiguration { get; private set; }

        /// <summary>
        /// Gets or sets the HTTP manager.
        /// </summary>
        /// <value>
        /// The HTTP manager.
        /// </value>
        public IHttpManager HttpManager { get; set; }

        /// <summary>
        /// Gets or sets the calls connector.
        /// </summary>
        /// <value>
        /// The calls connector.
        /// </value>
        public CallsConnector CallsConnector { get; set; }

        //todo ostali konektori


        /// <summary>
        /// Initializes a new instance of the <see cref="ZangService"/> class.
        /// </summary>
        public ZangService()
        {           
            this.ZangConfiguration = new ZangConfiguration();
            this.HttpManager = new HttpManager.HttpManager(this.ZangConfiguration);

            this.CallsConnector = new CallsConnector(this.HttpManager, this.ZangConfiguration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZangService"/> class.
        /// </summary>
        /// <param name="httpManager">The HTTP manager.</param>
        public ZangService(IHttpManager httpManager)
        {
            this.HttpManager = httpManager;
            this.ZangConfiguration = new ZangConfiguration();

            this.CallsConnector = new CallsConnector(this.HttpManager, this.ZangConfiguration);
        }

        public void ReadConfig()
        {
            throw new NotImplementedException();
        }
    }
}
