using System.Xml.Linq;

namespace ZangAPI.InboundXml
{
    public class InboundXmlBuilder
    {
        /// <summary>
        /// The requrest node.
        /// </summary>
        private ResponseNode requestNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="InboundXmlBuilder"/> class.
        /// </summary>
        public InboundXmlBuilder()
        {
            // sets the new document
            XDocument document = new XDocument();

            // creates the response node
            this.requestNode = new ResponseNode();

            // adds the response node to the document
            document.AddFirst(this.requestNode);
        }

        /// <summary>
        /// Gets the request node.
        /// You can start building with this.
        /// </summary>
        /// <returns></returns>
        public INode<ResponseNode> GetRequestNode()
        {
            // returns the request node
            return new InboundXmlNode<ResponseNode>(this.requestNode);
        }

        /// <summary>
        /// Returns the indented XML for this node. 
        /// </summary>
        public override string ToString()
        {
            // retruns the xml
            return this.requestNode.Document.ToString();
        }
    }
}
