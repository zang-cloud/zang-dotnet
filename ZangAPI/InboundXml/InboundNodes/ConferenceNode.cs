using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZangAPI.InboundXml.Enums;

namespace ZangAPI.InboundXml
{
    public class ConferenceNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Conference";

        /// <summary>
        /// Boolean value specifying if the conference should be muted. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if muted; otherwise, <c>false</c>.
        /// </value>
        public bool Muted
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("muted"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("muted", value.ToString());
            }
        }

        /// <summary>
        /// Boolean value specifying if a beep should play upon entrance to conference. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if beep; otherwise, <c>false</c>.
        /// </value>
        public bool Beep
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("beep"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("beep", value.ToString());
            }
        }

        /// <summary>
        /// Boolean value specifying if conference should begin upon entrance. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [start conference on enter]; otherwise, <c>false</c>.
        /// </value>
        public bool StartConferenceOnEnter
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("startConferenceOnEnter"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("startConferenceOnEnter", value.ToString());
            }
        }

        /// <summary>
        /// Boolean value specifying if conference should end upon a specific users exit. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [end conference on exit]; otherwise, <c>false</c>.
        /// </value>
        public bool EndConferenceOnExit
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("endConferenceOnExit"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("endConferenceOnExit", value.ToString());
            }
        }

        /// <summary>
        /// The maximum number of participants allowed in the conference call. Default Value: 40. Allowed Value: integer from 1 to 40.
        /// </summary>
        /// <value>
        /// The maximum participants.
        /// </value>
        public int MaxParticipants
        {
            get
            {
                int value = 40;
                Int32.TryParse(this.GetAttributeValue("maxParticipants"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("maxParticipants", value.ToString());
            }
        }

        /// <summary>
        /// URL to inboundXML where conference participants can be sent to while they wait for entrance into the conference. Only <Play> method supported at this time.
        /// </summary>
        /// <value>
        /// The wait sound.
        /// </value>
        public string WaitSound
        {
            get
            {
                return this.GetAttributeValue("waitSound");
            }
            set
            {
                this.SetAttributeValue("waitSound", value);
            }
        }

        /// <summary>
        /// Boolean value specifying if pressing* should end the conference.Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hangup on star]; otherwise, <c>false</c>.
        /// </value>
        public bool HangupOnStar
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("hangupOnStar"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("hangupOnStar", value.ToString());
            }
        }

        /// <summary>
        /// URL where some parameters specific to <Conference> will be sent when participants enter and exit the conference and once it is completed. 
        /// There is only one callbackUrl allowed per conference. The first callbackUrl will be the only one used.
        /// </summary>
        /// <value>
        /// The callback URL.
        /// </value>
        public string CallbackUrl
        {
            get
            {
                return this.GetAttributeValue("callbackUrl");
            }
            set
            {
                this.SetAttributeValue("callbackUrl", value);
            }
        }

        /// <summary>
        /// Method used to request the callback URL. Default Value: POST. Allowed Value: POST or GET.
        /// </summary>
        /// <value>
        /// The callback method.
        /// </value>
        public string CallbackMethod
        {
            get
            {
                return this.GetAttributeValue("callbackMethod");
            }
            set
            {
                this.SetAttributeValue("callbackMethod", value);
            }
        }

        /// <summary>
        /// Specifies digits that Zang should listen for and send to the callbackUrl if a caller inputs them.
        /// Separate additional digits or digit patterns with a comma.
        /// Allowed Value: Pattern made up of the digits 0-9, #, or *.
        /// </summary>
        /// <value>
        /// The digits match.
        /// </value>
        public string DigitsMatch
        {
            get
            {
                return this.GetAttributeValue("digitsMatch");
            }
            set
            {
                this.SetAttributeValue("digitsMatch", value);
            }
        }

        /// <summary>
        /// Boolean value specifying if the caller should stay alone in the conference call. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [stay alone]; otherwise, <c>false</c>.
        /// </value>
        public bool StayAlone
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("stayAlone"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("stayAlone", value.ToString());
            }
        }

        /// <summary>
        /// Boolean value specifying if the conference should be recorded. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if record; otherwise, <c>false</c>.
        /// </value>
        public bool Record
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("record"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("record", value.ToString());
            }
        }

        /// <summary>
        /// URL to sound that the recording will be sent to from the conference.
        /// </summary>
        /// <value>
        /// The record callback URL.
        /// </value>
        public string RecordCallbackUrl
        {
            get
            {
                return this.GetAttributeValue("recordCallbackUrl");
            }
            set
            {
                this.SetAttributeValue("recordCallbackUrl", value);
            }
        }

        /// <summary>
        /// File format in which the recording will be saved in. Default Value: mp3. Allowed Value: mp3 or wav.
        /// </summary>
        /// <value>
        /// The record file format.
        /// </value>
        public RecordFileFormatEnum RecordFileFormat
        {
            get
            {
                RecordFileFormatEnum value = RecordFileFormatEnum.mp3;
                Enum.TryParse(this.GetAttributeValue("recordFileFormat"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("recordFileFormat", value.ToString());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConferenceNode"/> class.
        /// </summary>
        public ConferenceNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the Conference node.
    /// </summary>
    public static class ConferenceExtensions
    {
        /// <summary>
        /// Adds the Conference node to the Dial node.
        /// </summary>
        /// <param name="responseNode">The response node.</param>
        /// <param name="value">The value.</param>
        /// <param name="muted">if set to <c>true</c> [muted].</param>
        /// <param name="beep">if set to <c>true</c> [beep].</param>
        /// <param name="startConferenceOnEnter">if set to <c>true</c> [start conference on enter].</param>
        /// <param name="endConferenceOnExit">if set to <c>true</c> [end conference on exit].</param>
        /// <param name="maxParticipants">The maximum participants.</param>
        /// <param name="waitSound">The wait sound.</param>
        /// <param name="hangupOnStar">if set to <c>true</c> [hangup on star].</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="callbackMethod">The callback method.</param>
        /// <param name="digitsMatch">The digits match.</param>
        /// <param name="stayAlone">if set to <c>true</c> [stay alone].</param>
        /// <param name="record">if set to <c>true</c> [record].</param>
        /// <param name="recordCallbackUrl">The record callback URL.</param>
        /// <param name="recordFileFormat">The record file format.</param>
        /// <returns></returns>
        public static INodeInner<DialNode, ResponseNode> Conference(
            this INodeInner<DialNode, ResponseNode> dialNode,
            string value = null,
            bool? muted = null,
            bool? beep = null,
            bool? startConferenceOnEnter = null,
            bool? endConferenceOnExit = null,
            int? maxParticipants = null,
            string waitSound = null,
            bool? hangupOnStar = null,
            string callbackUrl = null,
            string callbackMethod = null,
            string digitsMatch = null,
            bool? stayAlone = null,
            bool? record = null,
            string recordCallbackUrl = null,
            RecordFileFormatEnum? recordFileFormat = null
        )
        {
            // creates new conference node
            var conferenceNode = new ConferenceNode()
            {
                Value = value,
                WaitSound = waitSound,
                CallbackUrl = callbackUrl,
                CallbackMethod = callbackMethod,
                DigitsMatch = digitsMatch,
                RecordCallbackUrl = recordCallbackUrl,
            };

            // sets the values
            if (muted.HasValue) conferenceNode.Muted = muted.Value;
            if (beep.HasValue) conferenceNode.Beep = beep.Value;
            if (startConferenceOnEnter.HasValue) conferenceNode.StartConferenceOnEnter = startConferenceOnEnter.Value;
            if (endConferenceOnExit.HasValue) conferenceNode.EndConferenceOnExit = endConferenceOnExit.Value;
            if (maxParticipants.HasValue) conferenceNode.MaxParticipants = maxParticipants.Value;
            if (hangupOnStar.HasValue) conferenceNode.HangupOnStar = hangupOnStar.Value;
            if (stayAlone.HasValue) conferenceNode.StayAlone = stayAlone.Value;
            if (record.HasValue) conferenceNode.Record = record.Value;
            if (recordFileFormat.HasValue) conferenceNode.RecordFileFormat = recordFileFormat.Value;

            // adds the conference node to the dial
            dialNode.CurrentNode.Add(conferenceNode);

            // returns the dial node
            return dialNode;
        }
    }
}
