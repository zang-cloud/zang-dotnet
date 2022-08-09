using System;

namespace AvayaCPaaS.InboundXml.InboundNodes
{
    /// <summary>
    /// The Refer node for the Inbound XML.
    /// </summary>
    /// <seealso cref="ANode" />
    public class ReferNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Refer";

        /// <summary>
        /// URL where some parameters specific to <Refer> will be sent for further processing. The calling party can be redirected here upon the hangup of the B leg caller.
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
        /// The duration in seconds a call made through <Dial> should occur for before ending. Default Value: 14400. Allowed Value: integer greater than or equal to 1.
        /// </summary>
        /// <value>
        /// The time limit.
        /// </value>
        public int Timeout
        {
            get
            {
                int value = 14400;
                Int32.TryParse(this.GetAttributeValue("timeout"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("timeout", value.ToString());
            }
        }

        /// <summary>
        /// URL requested when the dialed call connects and ends. Note that this URL only receives parameters containing information about the call, the call does not execute XML given as a callbackUrl.
        /// </summary>
        /// <value>
        /// The callback URL.
        /// </value>
        public string CallbackUrl
        {
            get
            {
                return this.GetAttributeValue("callbackUrl");
            }
            set
            {
                this.SetAttributeValue("callbackUrl", value);
            }
        }

        /// <summary>
        /// Method used to request the callback URL. Default Value: POST. Allowed Value: POST or GET.
        /// </summary>
        /// <value>
        /// The callback method.
        /// </value>
        public string CallbackMethod
        {
            get
            {
                return this.GetAttributeValue("callbackMethod");
            }
            set
            {
                this.SetAttributeValue("callbackMethod", value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseNode"/> class.
        /// </summary>
        public ReferNode(string value)
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extension methods for the Dial node.
    /// </summary>
    public static class ReferNodeExensions
    {
        /// <summary>
        /// Adds the Refer node to the Request node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        /// <param name="method">The method.</param>
        /// <param name="timeout">The time limit.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="callbackMethod">The callback method.</param>
        /// <returns></returns>
        public static INodeCanInner<ResponseNode, ReferNode> Refer(
            this INode<ResponseNode> responseNode,
            string value = null,
            string action = null,
            string method = null,
            int? timeout = null,
            string callbackUrl = null,
            string callbackMethod = null
        )
        {
            // creates new refer node
            var referNode = new ReferNode(value)
            {
                Action = action,
                Method = method,
                CallbackUrl = callbackUrl,
                CallbackMethod = callbackMethod,
            };

            // sets the values
            if (timeout.HasValue) referNode.Timeout = timeout.Value;

            // retrieves the response
            var response = responseNode.CurrentNode;

            // adds the refer node as child to response
            response.Add(referNode);

            // retruns the node that can have inner
            return new InboundXmlNodeCanInner<ResponseNode, ReferNode>(response, referNode);
        }
    }
}
