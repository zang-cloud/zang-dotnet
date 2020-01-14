namespace AvayaCPaaS.InboundXml.InboundNodes
{
    /// <summary>
    /// The PlayLastRecording node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ANode" />
    public class PlayLastRecordingNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "PlayLastRecording";

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayLastRecordingNode"/> class.
        /// </summary>
        public PlayLastRecordingNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the PlayLastRecording node.
    /// </summary>
    public static class PlayLastRecordingNodeExtensions
    {
        /// <summary>
        /// Adds the PlayLastRecording node to the Response node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        public static INode<ResponseNode> PlayLastRecording(this INode<ResponseNode> responseNode)
        {
            // creates the new PlayLastRecording node
            var playLastRecordingNode = new PlayLastRecordingNode();

            // adds the PlayLastRecording node to the response
            responseNode.CurrentNode.Add(playLastRecordingNode);

            // returns the response node
            return responseNode;
        }
    }
}
