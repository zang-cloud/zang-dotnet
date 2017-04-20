using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZangAPI.InboundXml.Enums;

namespace ZangAPI.InboundXml
{
    /// <summary>
    /// Say node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ZangAPI.InboundXml.ANode" />
    public class SayNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Say";

        /// <summary>
        /// The gender of the voice that will speak the text. Allowed values are "man" or "woman". The default value is "woman".
        /// </summary>
        /// <value>
        /// The voice.
        /// </value>
        public VoiceEnum Voice
        {
            get
            {
                VoiceEnum value = VoiceEnum.woman;
                Enum.TryParse(this.GetAttributeValue("voice"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("voice", value.ToString());
            }
        }

        /// <summary>
        /// The language used to speak the text. Allowed values are "en" (American English), "en-gb" (British English), "es" (Spanish), "fr" (French), "it" (Italian) and "de" (German). The default value is "en"
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public LanguageEnum Language
        {
            get
            {
                LanguageEnum value = LanguageEnum.en;
                Enum.TryParse(this.GetAttributeValue("language"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("language", value.ToString());
            }
        }

        /// <summary>
        /// The amount of times the text should be repeated. Allowed values are any integer greater than or equal to 0. "0" indicates an infinite loop.
        /// </summary>
        /// <value>
        /// The loop.
        /// </value>
        public int? Loop
        {
            get
            {
                int value = -1;
                Int32.TryParse(this.GetAttributeValue("loop"), out value);
                if (value < 0) return null;
                else return value;
            }
            set
            {
                this.SetAttributeValue("loop", value?.ToString());
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SayNode"/> class.
        /// </summary>
        public SayNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the Say node.
    /// </summary>
    public static class SayNodeExtensions
    {
        /// <summary>
        /// Adds the Say node to the response node.
        /// </summary>
        /// <param name="voice">The voice.</param>
        /// <param name="language">The language.</param>
        /// <param name="loop">The loop.</param>
        public static INode<ResponseNode> Say(
            this INode<ResponseNode> responseNode,
            string value = null,
            VoiceEnum? voice = null,
            LanguageEnum? language = null,
            int? loop = null
        )
        {
            // creates new say node
            var sayNode = new SayNode()
            {
                Value = value,
                Loop = loop
            };

            // sets the values
            if (voice.HasValue) sayNode.Voice = voice.Value;
            if (language.HasValue) sayNode.Language = language.Value;

            // adds the say node to the response
            responseNode.CurrentNode.Add(sayNode);

            // returns the response node
            return responseNode;
        }

        /// <summary>
        /// Adds the Say node to the Response.
        /// </summary>
        /// <param name="voice">The voice.</param>
        /// <param name="language">The language.</param>
        /// <param name="loop">The loop.</param>
        public static INodeInner<GatherNode, ResponseNode> Say(
            this INodeInner<GatherNode, ResponseNode> gatherNode,
            string value = null,
            VoiceEnum? voice = null,
            LanguageEnum? language = null,
            int? loop = null
        )
        {
            // creates new say node
            var sayNode = new SayNode()
            {
                Value = value,
                Loop = loop
            };

            // sets the values
            if (voice.HasValue) sayNode.Voice = voice.Value;
            if (language.HasValue) sayNode.Language = language.Value;

            // adds the say node to the gather
            gatherNode.CurrentNode.Add(sayNode);

            // returns the response node
            return gatherNode;
        }
    }
}
