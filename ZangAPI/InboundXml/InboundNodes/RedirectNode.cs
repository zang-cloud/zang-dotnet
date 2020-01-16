namespace AvayaCPaaS.InboundXml.InboundNodes
{
    /// <summary>
    /// The Redirect node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ANode" />
    public class RedirectNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Redirect";

        /// <summary>
        /// Method used to request the InboundXML document the call is being redirected to. Default Value: POST. Allowed Value: POST or GET.
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
        /// Initializes a new instance of the <see cref="RedirectNode"/> class.
        /// </summary>
        public RedirectNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Redirect node extensions.
    /// </summary>
    public static class RedirectNodeExtensions
    {
        /// <summary>
        /// Adds the Redirect node to the Response node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="value">The value.</param>
        /// <param name="method">The method.</param>
        public static INode<ResponseNode> Redirect(
            this INode<ResponseNode> responseNode,
            string value = null,
            string method = null
        )
        {
            // creates new redirect node
            var redirectNode = new RedirectNode()
            {
                Value = value,
                Method = method
            };

            // adds the redirect node to the response
            responseNode.CurrentNode.Add(redirectNode);

            // returns the response node
            return responseNode;
        }
    }
}
