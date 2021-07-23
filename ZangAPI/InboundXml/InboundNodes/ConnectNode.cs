using System;
using AvayaCPaaS.InboundXml.Enums;

namespace AvayaCPaaS.InboundXml.InboundNodes
{
    /// <summary>
    /// The Connect node for the Inbound XML.
    /// </summary>
    /// <seealso cref="ANode" />
    public class ConnectNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Connect";

        /// <summary>
        /// URL where some parameters specific to <Connect> will be sent for further processing.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action
        {
            get
            {
                return this.GetAttributeValue("action");
            }
            set
            {
                this.SetAttributeValue("action", value);
            }
        }
        
        /// <summary>
        /// Method used to request the action URL. Default Value: POST. Allowed Value: POST or GET.
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
        /// Initializes a new instance of the <see cref="ResponseNode"/> class.
        /// </summary>
        public ConnectNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extension methods for the Connect node.
    /// </summary>
    public static class ConnectNodeExensions
    {
        /// <summary>
        /// Adds the Connect node to the Request node.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static INodeCanInner<ResponseNode, ConnectNode> Connect(
            this INode<ResponseNode> responseNode,
            string action = null,
            string method = null
        )
        {
            // creates new connect node
            var connectNode = new ConnectNode()
            {
                Action = action,
                Method = method
            };

            // retrieves the response
            var response = responseNode.CurrentNode;

            // adds the connect node as child to response
            response.Add(connectNode);

            // retruns the node that can have inner
            return new InboundXmlNodeCanInner<ResponseNode, ConnectNode>(response, connectNode);
        }
    }
}
