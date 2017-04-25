namespace ZangAPI.InboundXml.InboundNodes
{
    public class ResponseNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Response";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseNode"/> class.
        /// </summary>
        public ResponseNode()
            : base(NODE_NAME)
        {
        }
    }
}
