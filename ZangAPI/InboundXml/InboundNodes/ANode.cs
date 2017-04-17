using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZangAPI.InboundXml
{
    public class ANode : XElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ANode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ANode(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        protected string GetAttributeValue(string attributeName)
        {
            return this.Attribute(attributeName)?.Value;
        }
    }
}
