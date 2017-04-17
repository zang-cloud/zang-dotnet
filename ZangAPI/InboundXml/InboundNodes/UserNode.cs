using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class UserNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "User";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNode"/> class.
        /// </summary>
        public UserNode()
            : base(NODE_NAME)
        {
        }
    }
}
