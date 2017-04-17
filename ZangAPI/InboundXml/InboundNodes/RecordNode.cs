using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class RecordNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Record";

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordNode"/> class.
        /// </summary>
        public RecordNode()
            : base(NODE_NAME)
        {
        }
    }
}
