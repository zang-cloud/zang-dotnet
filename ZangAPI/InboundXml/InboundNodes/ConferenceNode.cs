using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class ConferenceNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Conference";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConferenceNode"/> class.
        /// </summary>
        public ConferenceNode()
            : base(NODE_NAME)
        {
        }
    }
}
