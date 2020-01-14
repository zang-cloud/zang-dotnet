namespace AvayaCPaaS.InboundXml.InboundNodes
{
    /// <summary>
    /// Ping node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ANode" />
    public class PingNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Ping";

        /// <summary>
        /// Method used to request ping url. Default Value: POST. Allowed Value: POST or GET.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method
        {
            get
            {
                // returns the attribute value
                return this.GetAttributeValue("method");
            }
            set
            {
                // sets the attribute value
                this.SetAttributeValue("method", value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PingNode"/> class.
        /// </summary>
        public PingNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the Ping node.
    /// </summary>
    public static class PingNodeExtensions
    {
        /// <summary>
        /// Adds the Ping node to the Response node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="value">The value.</param>
        /// <param name="method">The method.</param>
        public static INode<ResponseNode> Ping(
            this INode<ResponseNode> responseNode,
            string value = null,
            string method = null
        )
        {
            // creates the new ping node
            var pingNode = new PingNode()
            {
                Value = value,
                Method = method
            };

            // adds the ping node to the response
            responseNode.CurrentNode.Add(pingNode);

            // returns response node
            return responseNode;
        }
    }
}
