using System;
using ZangAPI.InboundXml.Enums;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class HangupNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Hangup";

        /// <summary>
        /// Specifies in seconds when a hangup should occur during a call. Allowed Value: integer greater than or equal to 0
        /// </summary>
        /// <value>
        /// The schedule.
        /// </value>
        public int Schedule
        {
            get
            {
                int value = 14400;
                Int32.TryParse(this.GetAttributeValue("schedule"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("schedule", value.ToString());
            }
        }


        public HangupReasonEnum Reason
        {
            get
            {
                HangupReasonEnum value = HangupReasonEnum.none;
                Enum.TryParse(this.GetAttributeValue("reason"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("reason", value.ToString());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HangupNode"/> class.
        /// </summary>
        public HangupNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the hangup node.
    /// </summary>
    public static class HangupExtensions
    {
        /// <summary>
        /// Adds the Hangup node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="schedule">The schedule.</param>
        /// <param name="reason">The reason.</param>
        public static INode<ResponseNode> Hangup(
           this INode<ResponseNode> responseNode,
           int? schedule = null,
           HangupReasonEnum? reason = null
        )
        {
            // creates the hangup node
            var hangup = new HangupNode();

            // sets the values if not null
            if (schedule.HasValue) hangup.Schedule = schedule.Value;
            if (reason.HasValue) hangup.Reason = reason.Value;

            // adds the hangup to the response node
            responseNode.CurrentNode.Add(hangup);

            // returns the response node
            return responseNode;
        }
    }
}
