using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class SmsNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Sms";

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsNode"/> class.
        /// </summary>
        public SmsNode()
            : base(NODE_NAME)
        {
        }
    }
}
