using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class RedirectNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Redirect";

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectNode"/> class.
        /// </summary>
        public RedirectNode()
            : base(NODE_NAME)
        {
        }
    }
}
