using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

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
        public string Build()
        {
            var documentString = this.requestNode.Document.ToString();

            if (this.IsXmlValid(documentString))
            {
                return documentString;
            }

            throw new Exception("Xml not valid.");
        }

        /// <summary>
        /// Determines whether [is XML valid] [the specified XML].
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>Returns true if xml is valid, false otherwise</returns>
        private bool IsXmlValid(string xml)
        {
            var document = XDocument.Parse(xml);

            Assembly a = Assembly.GetExecutingAssembly();

            var schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(a.GetManifestResourceStream("ZangAPI.inboundxml.xsd")));

            var valid = true;
            document.Validate(schemas, (o, e) => 
                { valid = false; });

            return valid;
        }
    }
}
