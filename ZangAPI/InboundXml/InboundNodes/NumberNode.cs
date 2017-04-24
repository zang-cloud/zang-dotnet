using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    /// <summary>
    /// Number node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ZangAPI.InboundXml.ANode" />
    public class NumberNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Number";
        
        /// <summary>
        /// Specifies which DTMF tones to play to the called party. w indicates a half second pause.
        /// </summary>
        /// <value>
        /// The send digits.
        /// </value>
        public string SendDigits
        {
            get
            {
                return this.GetAttributeValue("sendDigits");
            }
            set
            {
                this.SetAttributeValue("sendDigits", value);
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberNode"/> class.
        /// </summary>
        public NumberNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the number node.
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Adds the Number node to the Dial node.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="sendDigits">The send digits.</param>
        public static INodeInner<DialNode, ResponseNode> Number(
            this INodeInner<DialNode, ResponseNode> dialNode,
            string value = null,
            string sendDigits = null
        )
        {
            // creates new Number node
            var number = new NumberNode()
            {
                Value = value,
                SendDigits = sendDigits
            };

            // adds the number node to the dial
            dialNode.CurrentNode.Add(number);

            // returns the dial node
            return dialNode;
        }
    }
}
