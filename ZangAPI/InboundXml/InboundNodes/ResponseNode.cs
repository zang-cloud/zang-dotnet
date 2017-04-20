using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    public class ResponseNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Response";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseNode"/> class.
        /// </summary>
        public ResponseNode()
            : base(NODE_NAME)
        {
        }
    }
}
