using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class SayNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Say";

        /// <summary>
        /// Initializes a new instance of the <see cref="SayNode"/> class.
        /// </summary>
        public SayNode()
            : base(NODE_NAME)
        {
        }
    }
}
