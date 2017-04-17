using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class RejectNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Reject";

        /// <summary>
        /// Initializes a new instance of the <see cref="RejectNode"/> class.
        /// </summary>
        public RejectNode()
            : base(NODE_NAME)
        {
        }
    }
}
