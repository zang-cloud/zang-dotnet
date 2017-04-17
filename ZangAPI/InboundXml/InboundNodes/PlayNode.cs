using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    /// <summary>
    ///  The Play node for the Inbound XML.
    /// </summary>
    /// <seealso cref="ZangAPI.InboundXml.ANode" />
    public class PlayNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Play";

        /// <summary>
        /// Gets or sets the loop.
        /// </summary>
        /// <value>
        /// The loop.
        /// </value>
        public int Loop
        {
            // TODO, vidiš kad atribut nema vrijednost stringa, onda ti možeš postaviti taj izborni oblik,
            // a onda u ovim getterima i setterima transformirati vrijednosti
            get
            {
                // retrieves the loop value
                var stringValue = this.GetAttributeValue("loop");

                // parses the string value
                int intValue = 0;
                Int32.TryParse(stringValue, out intValue);

                // retruns the int value
                return intValue;
            }
            set
            {
                // sets the value
                this.SetAttributeValue("loop", value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the tone stream.
        /// </summary>
        /// <value>
        /// The tone stream.
        /// </value>
        public string ToneStream
        {
            get
            {
                // retruns the value of this node.
                return this.Value;
            }
            set
            {
                // sets the value for the node.
                this.SetValue(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayNode"/> class.
        /// </summary>
        public PlayNode(string toneStream)
            : base(NODE_NAME)
        {
            // sets the tone stream
            this.ToneStream = toneStream;
        }
    }

    /// <summary>
    /// The extensions for the Play node.
    /// </summary>
    public static class PlayNodeExtensions
    {
        /// <summary>
        /// Adds the Play node to the Response node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="loop">The loop.</param>
        /// <param name="toneStream">The tone stream.</param>
        /// <returns></returns>
        public static INode<ResponseNode> Play(
            this INode<ResponseNode> responseNode,
            int loop = 0,
            string toneStream = null
            // TODO također vidi ako treba više išta, ali mislim da ne treba
            )
        {
            // creates the new play node
            var play = new PlayNode(toneStream)
            {
                Loop = loop
            };

            // adds the play to the response
            responseNode.CurrentNode.Add(play);

            // returns the builder node
            return responseNode;
        }

        /// <summary>
        /// Adds the Play node to the Response node.
        /// </summary>
        /// <param name="gatherNode">The gather node.</param>
        /// <param name="loop">The loop.</param>
        /// <param name="toneStream">The tone stream.</param>
        /// <returns></returns>
        public static INodeInner<GatherNode, ResponseNode> Play(
            this INodeInner<GatherNode, ResponseNode> gatherNode,
            int loop = 0,
            string toneStream = null
            // TODO također vidi ako treba više išta, ali mislim da ne treba
            )
        {
            // creates the new play node
            var play = new PlayNode(toneStream)
            {
                Loop = loop
            };

            // adds the play to the gather
            gatherNode.CurrentNode.Add(play);

            // returns the builder node
            return gatherNode;
        }
    }
}
