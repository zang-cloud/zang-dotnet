using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class PingNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "PingNode";

        /// <summary>
        /// Initializes a new instance of the <see cref="PingNode"/> class.
        /// </summary>
        public PingNode()
            : base(NODE_NAME)
        {
        }
    }
}
