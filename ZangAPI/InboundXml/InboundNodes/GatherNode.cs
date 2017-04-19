using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    public class GatherNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Gather";

        /// <summary>
        /// URL where the flow of the call and the gathered digits will be forwarded to (if digits are input).
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action
        {
            get
            {
                return this.GetAttributeValue("action");
            }
            set
            {
                this.SetAttributeValue("action", value);
            }
        }

        /// <summary>
        /// Method used to request the action URL. Default value is POST. Available methods are GET and POST.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method
        {
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
        /// The number of seconds<Gather> should wait for digits to be entered before requesting the action URL.Timeout resets with each new digit input.Default value is 5 seconds.Timeout accepts any integer greater than or equal to 0.
        /// </summary>
        /// <value>
        /// The timeout.
        /// </value>
        public int Timeout
        {
            get
            {
                int value = 14400;
                Int32.TryParse(this.GetAttributeValue("timeout"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("timeout", value.ToString());
            }
        }

        /// <summary>
        /// The key a caller can press to end the <Gather>. Default value is #. Acceptable values are digits from 0 to 9, # or *
        /// </summary>
        /// <value>
        /// The finish on key.
        /// </value>
        public string FinishOnKey
        {
            get
            {
                // returns the attribute value
                return this.GetAttributeValue("finishOnKey");
            }
            set
            {
                // sets the attribute value
                this.SetAttributeValue("finishOnKey", value);
            }
        }

        /// <summary>
        /// The maximum number of digits to <Gather>. Default value is set to no limit. Acceptable value is any integer greater than or equal to 0.
        /// </summary>
        /// <value>
        /// The number digits.
        /// </value>
        public int NumDigits
        {
            get
            {
                int value = 14400;
                Int32.TryParse(this.GetAttributeValue("numDigits"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("numDigits", value.ToString());
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GatherNode"/> class.
        /// </summary>
        public GatherNode()
            : base(NODE_NAME)
        {
        }
    }

    public static class GatherNodeExtensions
    {
        public static INodeCanInner<ResponseNode, GatherNode> Gather(
            this INode<ResponseNode> responseNode,
            string action = null,
            string method = null,
            int? timeout = null,
            string finishOnKey = null,
            int? numDigits = null
        )
        {
            // creates the gather element
            var gather = new GatherNode()
            {
                Action = action,
                Method = method,
                FinishOnKey = finishOnKey
            };

            // sets the values
            if (timeout.HasValue) gather.Timeout = timeout.Value;
            if (numDigits.HasValue) gather.NumDigits = numDigits.Value;

            // adds the gather to the response node
            responseNode.CurrentNode.Add(gather);

            // returns the node
            return new InboundXmlNodeCanInner<ResponseNode, GatherNode>(responseNode.CurrentNode, gather);
        }
    }
}
