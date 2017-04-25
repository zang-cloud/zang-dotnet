using System;

namespace ZangAPI.InboundXml.InboundNodes
{
    /// <summary>
    ///  The Play node for the Inbound XML.
    /// </summary>
    /// <seealso cref="ANode" />
    public class PlayNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Play";

        /// <summary>
        /// Gets or sets the loop.
        /// </summary>
        /// <value>
        /// The loop.
        /// </value>
        public int Loop
        {
            get
            {
                // retrieves the loop value
                var stringValue = this.GetAttributeValue("loop");

                // parses the string value
                int intValue = 0;
                Int32.TryParse(stringValue, out intValue);

                // retruns the int value
                return intValue;
            }
            set
            {
                // sets the value
                this.SetAttributeValue("loop", value.ToString());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayNode"/> class.
        /// </summary>
        public PlayNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// The extensions for the Play node.
    /// </summary>
    public static class PlayNodeExtensions
    {
        /// <summary>
        /// Adds the Play node to the Response node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="loop">The loop.</param>
        /// <param name="toneStream">The tone stream.</param>
        /// <returns></returns>
        public static INode<ResponseNode> Play(
            this INode<ResponseNode> responseNode,
            string toneStream = null,
            int? loop = null
        )
        {
            // creates the new play node
            var play = new PlayNode()
            {
                Value = toneStream
            };

            // sets the data
            if (loop.HasValue) play.Loop = loop.Value;

            // adds the play to the response
            responseNode.CurrentNode.Add(play);

            // returns the builder node
            return responseNode;
        }

        /// <summary>
        /// Adds the Play node to the Response node.
        /// </summary>
        /// <param name="gatherNode">The gather node.</param>
        /// <param name="loop">The loop.</param>
        /// <param name="toneStream">The tone stream.</param>
        /// <returns></returns>
        public static INodeInner<GatherNode, ResponseNode> Play(
            this INodeInner<GatherNode, ResponseNode> gatherNode,
            string toneStream = null,
            int? loop = null
            )
        {
            // creates the new play node
            var play = new PlayNode()
            {
                Value = toneStream,
            };

            // sets the data
            if (loop.HasValue) play.Loop = loop.Value;

            // adds the play to the gather
            gatherNode.CurrentNode.Add(play);

            // returns the builder node
            return gatherNode;
        }
    }
}
