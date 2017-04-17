using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class NumberNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Number";

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberNode"/> class.
        /// </summary>
        public NumberNode()
            : base(NODE_NAME)
        {
        }
    }
}
