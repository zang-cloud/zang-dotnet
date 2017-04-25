namespace ZangAPI.InboundXml.InboundNodes
{
    /// <summary>
    /// Sip node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ANode" />
    public class SipNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Sip";

        /// <summary>
        /// If provided, will be passed along as sip authentication username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get
            {
                return this.GetAttributeValue("username");
            }
            set
            {
                this.SetAttributeValue("username", value);
            }
        }

        /// <summary>
        /// If provided, will be passed along as sip authentication password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get
            {
                return this.GetAttributeValue("password");
            }
            set
            {
                this.SetAttributeValue("password", value);
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SipNode"/> class.
        /// </summary>
        public SipNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the sip node.
    /// </summary>
    public static class SipNodeExtensions
    {
        /// <summary>
        /// Adds the Sip node to the Dial node.
        /// </summary>
        /// <param name="dialNode">The dial node</param>
        /// <param name="value">The value.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public static INodeInner<DialNode, ResponseNode> Sip(
            this INodeInner<DialNode, ResponseNode> dialNode,
            string value = null,
            string username = null,
            string password = null
        )
        {
            // creates new Sip node
            var sip = new SipNode()
            {
                Value = value,
                Username = username,
                Password = password
            };

            // adds the Sip node to the dial
            dialNode.CurrentNode.Add(sip);

            // returns the dial node
            return dialNode;
        }
    }
}
