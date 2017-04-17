using System.Xml.Linq;

namespace ZangAPI.InboundXml
{
    public class InboundXmlBuilder : INode<ResponseNode>
    {
        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <value>
        /// The document.
        /// </value>
        public XDocument Document { get; protected set; }

        /// <summary>
        /// Gets the current node.
        /// </summary>
        /// <value>
        /// The current node.
        /// </value>
        public ResponseNode CurrentNode { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InboundXmlBuilder"/> class.
        /// </summary>
        public InboundXmlBuilder()
        {
            // sets the new document
            this.Document = new XDocument();

            // creates the response node
            this.CurrentNode = new ResponseNode();

            // adds the response node to the document
            this.Document.AddFirst(this.CurrentNode);
        }

        /// <summary>
        /// Returns the indented XML for this node. 
        /// </summary>
        public override string ToString()
        {
            // retruns the xml
            return this.Document.ToString();
        }
    }
}
