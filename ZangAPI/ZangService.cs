using System;
using ZangAPI.Configuration;
using ZangAPI.ConnectionManager;
using ZangAPI.Connectors;

namespace ZangAPI
{
    /// <summary>
    /// Zang service
    /// </summary>
    public class ZangService
    {
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

        /// <summary>
        /// Gets or sets the SMS connector.
        /// </summary>
        /// <value>
        /// The SMS connector.
        /// </value>
        public SmsConnector SmsConnector { get; set; }

        /// <summary>
        /// Gets or sets the MMS connector.
        /// </summary>
        /// <value>
        /// The SMS connector.
        /// </value>
        public MmsConnector MmsConnector { get; set; }

        /// <summary>
        /// Gets or sets the usages connector.
        /// </summary>
        /// <value>
        /// The usages connector.
        /// </value>
        public UsagesConnector UsagesConnector { get; set; }

        /// <summary>
        /// Gets or sets the accounts connector.
        /// </summary>
        /// <value>
        /// The accounts connector.
        /// </value>
        public AccountsConnector AccountsConnector { get; set; }

        /// <summary>
        /// Gets or sets the conferences connector.
        /// </summary>
        /// <value>
        /// The conferences connector.
        /// </value>
        public ConferencesConnector ConferencesConnector { get; set; }

        /// <summary>
        /// Gets or sets the applications connector.
        /// </summary>
        /// <value>
        /// The applications connector.
        /// </value>
        public ApplicationsConnector ApplicationsConnector { get; set; }

        /// <summary>
        /// Gets or sets the application clients connector.
        /// </summary>
        /// <value>
        /// The application clients connector.
        /// </value>
        public ApplicationClientsConnector ApplicationClientsConnector { get; set; }

        /// <summary>
        /// Gets or sets the incoming numbers connector.
        /// </summary>
        /// <value>
        /// The incoming numbers connector.
        /// </value>
        public IncomingPhoneNumbersConnector IncomingPhoneNumbersConnector { get; set; }

        /// <summary>
        /// Gets or sets the recordings connector.
        /// </summary>
        /// <value>
        /// The recordings connector.
        /// </value>
        public RecordingsConnector RecordingsConnector { get; set; }

        /// <summary>
        /// Gets or sets the notifications connector.
        /// </summary>
        /// <value>
        /// The notifications connector.
        /// </value>
        public NotificationsConnector NotificationsConnector { get; set; }

        /// <summary>
        /// Gets or sets the available phone numbers connector.
        /// </summary>
        /// <value>
        /// The available phone numbers connector.
        /// </value>
        public AvailablePhoneNumbersConnector AvailablePhoneNumbersConnector { get; set; }

        /// <summary>
        /// Gets or sets the transcriptions connector.
        /// </summary>
        /// <value>
        /// The transcriptions connector.
        /// </value>
        public TranscriptionsConnector TranscriptionsConnector { get; set; }

        /// <summary>
        /// Gets or sets the sip domains connector.
        /// </summary>
        /// <value>
        /// The sip domains connector.
        /// </value>
        public SipDomainsConnector SipDomainsConnector { get; set; }

        /// <summary>
        /// Gets or sets the sip credentials connector.
        /// </summary>
        /// <value>
        /// The sip credentials connector.
        /// </value>
        public SipCredentialsConnector SipCredentialsConnector { get; set; }

        /// <summary>
        /// Gets or sets the sip ip access control lists connector.
        /// </summary>
        /// <value>
        /// The sip ip access control lists connector.
        /// </value>
        public SipIpAccessControlListsConnector SipIpAccessControlListsConnector { get; set; }

        /// <summary>
        /// Gets or sets the carrier services connector.
        /// </summary>
        /// <value>
        /// The carrier services connector.
        /// </value>
        public CarrierServicesConnector CarrierServicesConnector { get; set; }

        /// <summary>
        /// Gets or sets the fraud control connector.
        /// </summary>
        /// <value>
        /// The fraud control connector.
        /// </value>
        public FraudControlConnector FraudControlConnector { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZangService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ZangService(IZangConfiguration configuration)
            : this(new HttpManager(configuration))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZangService"/> class.
        /// </summary>
        /// <param name="httpManager">The HTTP manager.</param>
        public ZangService(IHttpManager httpManager)
        {
            this.HttpManager = httpManager;
            this.InitConnectors();
        }

        /// <summary>
        /// Initializes the connectors.
        /// </summary>
        private void InitConnectors()
        {
            this.CallsConnector = new CallsConnector(this.HttpManager);
            this.SmsConnector = new SmsConnector(this.HttpManager);
            this.MmsConnector = new MmsConnector(this.HttpManager);
            this.UsagesConnector = new UsagesConnector(this.HttpManager);
            this.AccountsConnector = new AccountsConnector(this.HttpManager);
            this.ConferencesConnector = new ConferencesConnector(this.HttpManager);
            this.ApplicationsConnector = new ApplicationsConnector(this.HttpManager);
            this.ApplicationClientsConnector = new ApplicationClientsConnector(this.HttpManager);
            this.IncomingPhoneNumbersConnector = new IncomingPhoneNumbersConnector(this.HttpManager);
            this.RecordingsConnector = new RecordingsConnector(this.HttpManager);
            this.NotificationsConnector = new NotificationsConnector(this.HttpManager);
            this.AvailablePhoneNumbersConnector = new AvailablePhoneNumbersConnector(this.HttpManager);
            this.TranscriptionsConnector = new TranscriptionsConnector(this.HttpManager);
            this.SipDomainsConnector = new SipDomainsConnector(this.HttpManager);
            this.SipCredentialsConnector = new SipCredentialsConnector(this.HttpManager);
            this.SipIpAccessControlListsConnector = new SipIpAccessControlListsConnector(this.HttpManager);
            this.CarrierServicesConnector = new CarrierServicesConnector(this.HttpManager);
            this.FraudControlConnector = new FraudControlConnector(this.HttpManager);
        }

        /// <summary>
        /// Initializes from configuration.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void InitFromConfig()
        {
            // Read configuration from application settings
            var accountSid = Properties.Settings.Default.accountSid;
            var authToken = Properties.Settings.Default.authToken;
            var baseUrl = Properties.Settings.Default.baseUrl;
            var proxyHost = Properties.Settings.Default.proxyHost;
            var proxyPort = Properties.Settings.Default.proxyPort;
            var useProxy = Properties.Settings.Default.useProxy;

            var configuration = new ZangConfiguration(accountSid, authToken)
            {
                BaseUrl = baseUrl, ProxyHost = proxyHost, ProxyPort = proxyPort, 
                UseProxy = Convert.ToBoolean(useProxy)

            };

            this.HttpManager = new HttpManager(configuration);
            this.InitConnectors();
        }
    }
}