using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    /// <summary>
    /// Sms node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ZangAPI.InboundXml.ANode" />
    public class SmsNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Sms";

        /// <summary>
        /// The phone number that will receive the SMS message. If this parameter is not specified, the SMS will be sent back to the number that made the request to the Zang number’s SMS request URL.
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
        /// The number that will display as sending the SMS message. This should be one of your Zang numbers. If this parameter is not specified, the default from number is the Zang number hosting the SMS request URL.
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
        /// URL to direct Zang to once the <Sms> element is executed. Parameters specific to <Sms> are sent here along with the request.
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
        /// URL where the status of the SMS can be sent.
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
        /// Initializes a new instance of the <see cref="SmsNode"/> class.
        /// </summary>
        public SmsNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the Sms node.
    /// </summary>
    public static class SmsNodeExtensions
    {
        /// <summary>
        /// Adds the Sms node to the Response node.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="action">The action.</param>
        /// <param name="method">The method.</param>
        /// <param name="statusCallback">The status callback.</param>
        public static INode<ResponseNode> Sms(
            this INode<ResponseNode> responseNode,
            string to ,
            string from,
            string value = null,
            string action = null,
            string method = null,
            string statusCallback = null
        )
        {
            // creates new sms node
            var smsNode = new SmsNode()
            {
                Value = value,
                To = to,
                From = from,
                Action = action,
                Method = method,
                StatusCallback = statusCallback
            };

            // adds the sms node to the response
            responseNode.CurrentNode.Add(smsNode);

            // returns the response node
            return responseNode;
        }
    }
}
