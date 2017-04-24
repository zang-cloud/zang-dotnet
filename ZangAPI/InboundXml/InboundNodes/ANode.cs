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
        /// Gets or sets the concatenated text contents of this element.
        /// </summary>
        public new string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (value != null)
                    base.Value = value;
            }
        }

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

        /// <summary>
        /// Sets the attribute value.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void SetAttributeValue(string attributeName, object value)
        {
            // if value is null - returns
            if (value == null) return;

            // calls the overriden method
            base.SetAttributeValue(attributeName, value);
        }
    }
}
