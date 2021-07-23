namespace AvayaCPaaS.InboundXml.InboundNodes
{
    /// <summary>
    /// Agent node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ANode" />
    public class AgentNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Agent";

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentNode"/> class.
        /// </summary>
        public AgentNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the agent node.
    /// </summary>
    public static class AgentExtensions
    {
        /// <summary>
        /// Adds the agent node to the Connect node.
        /// </summary>
        /// <param name="connectNode">The connect node</param>
        public static INodeInner<ConnectNode, ResponseNode> Agent(
            this INodeInner<ConnectNode, ResponseNode> connectNode,
            string value = null
        )
        {
            // creates new Agent node
            var agent = new AgentNode()
            {
                Value = value
            };

            // adds the agent ID to connect
            connectNode.CurrentNode.Add(agent);

            // returns the connect node
            return connectNode;
        }
    }
}
