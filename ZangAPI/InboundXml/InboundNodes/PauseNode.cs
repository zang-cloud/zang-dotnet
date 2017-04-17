using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class PauseNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Pause";

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseNode"/> class.
        /// </summary>
        public PauseNode()
            : base(NODE_NAME)
        {
        }
    }
}
