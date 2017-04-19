using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    /// <summary>
    /// Pause node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ZangAPI.InboundXml.ANode" />
    public class PauseNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Pause";

        /// <summary>
        /// The length in seconds the pause should be. Default Value: 1. Allowed Value: integer greater than 0, less than 99999
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length
        {
            get
            {
                int value = 1;
                Int32.TryParse(this.GetAttributeValue("length"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("length", value.ToString());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseNode"/> class.
        /// </summary>
        public PauseNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the pause node.
    /// </summary>
    public static class PauseNodeExtensions
    {
        /// <summary>
        /// Adds the Pause node to the Response node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="length">The length.</param>
        public static INode<ResponseNode> Pause(
            this INode<ResponseNode> responseNode,
            int? length = null
        )
        {
            // creates new pause node
            var pauseNode = new PauseNode();

            // sets the values
            if (length.HasValue) pauseNode.Length = length.Value;

            // adds the pause node to the response
            responseNode.CurrentNode.Add(pauseNode);

            // returns the response node
            return responseNode;
        }

        /// <summary>
        /// Adds the Pause node to the Gather node.
        /// </summary>
        /// <param name="gatherNode">The gather node.</param>
        /// <param name="length">The length.</param>
        public static INodeInner<GatherNode, ResponseNode> Pause(
            this INodeInner<GatherNode, ResponseNode> gatherNode,
            int? length = null
        )
        {
            // creates new pause node
            var pauseNode = new PauseNode();

            // sets the values
            if (length.HasValue) pauseNode.Length = length.Value;

            // adds the pause node to the gather
            gatherNode.CurrentNode.Add(pauseNode);

            // returns the gather node
            return gatherNode;
        }
    }
}
