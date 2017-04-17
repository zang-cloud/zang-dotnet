using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class SipNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Sip";

        /// <summary>
        /// Initializes a new instance of the <see cref="SipNode"/> class.
        /// </summary>
        public SipNode()
            : base(NODE_NAME)
        {
        }
    }
}
