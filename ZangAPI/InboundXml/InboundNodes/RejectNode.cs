using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZangAPI.InboundXml.Enums;

namespace ZangAPI.InboundXml
{
    /// <summary>
    /// The Hangup node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ZangAPI.InboundXml.ANode" />
    public class RejectNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Reject";

        /// <summary>
        /// The reason to list as why the call was rejected. Default Value: rejected. Allowed Value: busy or rejected.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public HangupReasonEnum Reason
        {
            get
            {
                HangupReasonEnum value = HangupReasonEnum.rejected;
                Enum.TryParse(this.GetAttributeValue("reason"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("reason", value.ToString());
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RejectNode"/> class.
        /// </summary>
        public RejectNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Reject node extensions.
    /// </summary>
    public static class RejectExtensions
    {
        /// <summary>
        /// Adds the Reject node to the Response node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="reason">The reason.</param>
        public static INode<ResponseNode> Reject(
            this INode<ResponseNode> responseNode,
            HangupReasonEnum? reason = null
        )
        {
            // creates new reject node
            var rejectNode = new RejectNode();

            // sets the values
            if (reason.HasValue) rejectNode.Reason = reason.Value;

            // adds the reject node to the response
            responseNode.CurrentNode.Add(rejectNode);

            // returns the response node
            return responseNode;
        }
    }
}
