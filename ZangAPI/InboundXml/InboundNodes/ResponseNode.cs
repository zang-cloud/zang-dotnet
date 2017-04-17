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
        /// Gets or sets the test attribute.
        /// </summary>
        /// <value>
        /// The test attribute.
        /// </value>
        public string TestAttribute
        {
            get
            {
                // returns the attribute value
                return this.GetAttributeValue("TestAttribute");
            }
            set
            {
                // sets the attribute value
                this.SetAttributeValue("TestAttribute", value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseNode"/> class.
        /// </summary>
        public ResponseNode()
            : base(NODE_NAME)
        {
        }
    }
}
