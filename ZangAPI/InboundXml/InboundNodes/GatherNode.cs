using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    public class GatherNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Gather";

        /// <summary>
        /// Initializes a new instance of the <see cref="GatherNode"/> class.
        /// </summary>
        public GatherNode()
            : base(NODE_NAME)
        {
        }
    }

    public static class GatherNodeExtensions
    {
        // TODO ovo je samo za test
        public static INodeCanInner<ResponseNode, GatherNode> Gather(
            this INode<ResponseNode> responseNode
        )
        {
            var gather = new GatherNode();
            responseNode.CurrentNode.Add(gather);
            return new InboundXmlNodeCanInner<ResponseNode, GatherNode>(responseNode.CurrentNode, gather);
        }
    }
}
