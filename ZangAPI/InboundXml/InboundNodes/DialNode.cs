using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    /// <summary>
    /// The Dial node for the Inbound XML.
    /// </summary>
    /// <seealso cref="ZangAPI.InboundXml.ANode" />
    public class DialNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Dial";

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method
        {
            // TODO ovako postavljaš atribute koje neki element može imati
            // pročitaš u dokumentaciji kako se koji atribut zove i ovdje iskoristiš njegovo ime
            get
            {
                // returns the attribute value
                return this.GetAttributeValue("method");
            }
            set
            {
                // sets the attribute value
                this.SetAttributeValue("method", value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseNode"/> class.
        /// </summary>
        public DialNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extension methods for the Dial node.
    /// </summary>
    public static class DialNodeExensions
    {
        /// <summary>
        /// Adds the Dial node to the Request node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static INodeCanInner<ResponseNode, DialNode> Dial(
            this INode<ResponseNode> responseNode,
            string method = "POST"
            // TODO ovdje stavi ostale atribute koji se mogu zadati Dialu
            // Stavi ih ono s defaultnim vrijednostima pa da može programer samo neke upisati
            // onako kako si već radila kad ima puno atributa
            // evo ja sam za pokazivanje riješio već ovaj atribut method
            )
        {
            // creates new dial node
            var dialNode = new DialNode()
            {
                Method = method
            };

            // retrieves the response
            var response = responseNode.CurrentNode;

            // adds the dial node as child to response
            response.Add(dialNode);

            // retruns the node that can have inner
            return new InboundXmlNodeCanInner<ResponseNode, DialNode>(response, dialNode);
        }
    }
}
