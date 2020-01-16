namespace AvayaCPaaS.InboundXml.InboundNodes
{
    /// <summary>
    /// Mms node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ANode" />
    public class MmsNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Mms";

        /// <summary>
        /// The phone number that will receive the MMS message. If this parameter is not specified, the MMS will be sent back to the number that made the request to the Avaya CPaaS number’s MMS request URL.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public string To
        {
            get
            {
                // returns the attribute value
                return this.GetAttributeValue("to");
            }
            set
            {
                // sets the attribute value
                this.SetAttributeValue("to", value);
            }
        }

        /// <summary>
        /// The number that will display as sending the MMS message. This should be one of your Avaya CPaaS numbers. If this parameter is not specified, the default from number is the Avaya CPaaS number hosting the MMS request URL.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public string From
        {
            get
            {
                // returns the attribute value
                return this.GetAttributeValue("from");
            }
            set
            {
                // sets the attribute value
                this.SetAttributeValue("from", value);
            }
        }

        /// <summary>
        /// URL to direct Avaya CPaaS to once the <Mms> element is executed. Parameters specific to <Mms> are sent here along with the request.
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
        /// URL where the status of the MMS can be sent.
        /// </summary>
        /// <value>
        /// The status callback.
        /// </value>
        public string StatusCallback
        {
            get
            {
                // returns the attribute value
                return this.GetAttributeValue("statusCallback");
            }
            set
            {
                // sets the attribute value
                this.SetAttributeValue("statusCallback", value);
            }
        }

        /// <summary>
        /// URL of an image to be sent in the message.
        /// </summary>
        /// <value>
        /// The media Url.
        /// </value>
        public string MediaUrl
        {
            get
            {
                // returns the attribute value
                return this.GetAttributeValue("mediaUrl");
            }
            set
            {
                // sets the attribute value
                this.SetAttributeValue("mediaUrl", value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MmsNode"/> class.
        /// </summary>
        public MmsNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the Mms node.
    /// </summary>
    public static class MmsNodeExtensions
    {
        /// <summary>
        /// Adds the Mms node to the Response node.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="responseNode">The response node</param>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="action">The action.</param>
        /// <param name="method">The method.</param>
        /// <param name="statusCallback">The status callback.</param>
        /// <param name="mediaUrl">The URL of an image to be sent.</param>
        public static INode<ResponseNode> Mms(
            this INode<ResponseNode> responseNode,
            string to ,
            string from,
            string value = null,
            string action = null,
            string method = null,
            string statusCallback = null,
            string mediaUrl = null
        )
        {
            // creates new mms node
            var mmsNode = new MmsNode()
            {
                Value = value,
                To = to,
                From = from,
                Action = action,
                Method = method,
                StatusCallback = statusCallback,
                MediaUrl = mediaUrl
            };

            // adds the mms node to the response
            responseNode.CurrentNode.Add(mmsNode);

            // returns the response node
            return responseNode;
        }
    }
}
