using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class HangupNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Hangup";

        /// <summary>
        /// Initializes a new instance of the <see cref="HangupNode"/> class.
        /// </summary>
        public HangupNode()
            : base(NODE_NAME)
        {
        }
    }
}
