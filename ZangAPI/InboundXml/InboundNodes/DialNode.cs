using System;
using AvayaCPaaS.InboundXml.Enums;

namespace AvayaCPaaS.InboundXml.InboundNodes
{
    /// <summary>
    /// The Dial node for the Inbound XML.
    /// </summary>
    /// <seealso cref="ANode" />
    public class DialNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Dial";

        /// <summary>
        /// URL where some parameters specific to <Dial> will be sent for further processing. The calling party can be redirected here upon the hangup of the B leg caller.
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
        /// Method used to request the action URL. Default Value: POST. Allowed Value: POST or GET.
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
        /// The duration in seconds a call made through <Dial> should occur for before ending. Default Value: 14400. Allowed Value: integer greater than or equal to 1.
        /// </summary>
        /// <value>
        /// The time limit.
        /// </value>
        public int TimeLimit
        {
            get
            {
                int value = 14400;
                Int32.TryParse(this.GetAttributeValue("timeLimit"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("timeLimit", value.ToString());
            }
        }

        /// <summary>
        /// Number to display as calling.Defaults to the ID of phone being used.
        /// </summary>
        /// <value>
        /// The caller identifier.
        /// </value>
        public string CallerId
        {
            get
            {
                return this.GetAttributeValue("callerId");
            }
            set
            {
                this.SetAttributeValue("callerId", value);
            }
        }

        /// <summary>
        /// Boolean value specifying if the caller ID should be hidden or not. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hide caller identifier]; otherwise, <c>false</c>.
        /// </value>
        public bool HideCallerId
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("hideCallerId"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("hideCallerId", value.ToString());
            }
        }

        /// <summary>
        /// URL to an InboundXML document to be executed in place of the call ringtone(a<Say> or <Play> would be appropriate in this document).
        /// </summary>
        /// <value>
        /// The dial music.
        /// </value>
        public string DialMusic
        {
            get
            {
                return this.GetAttributeValue("dialMusic");
            }
            set
            {
                this.SetAttributeValue("dialMusic", value);
            }
        }


        /// <summary>
        /// URL requested when the dialed call connects and ends. Note that this URL only receives parameters containing information about the call, the call does not execute XML given as a callbackUrl.
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
        /// The URL that Avaya CPaaS should reach out to when the called party answers. The URL should return InboundXML containing <Play>, <Pause>, and/or <Say> elements only. Any other elements will be ignored.
        /// </summary>
        /// <value>
        /// The confirm sound.
        /// </value>
        public string ConfirmSound
        {
            get
            {
                return this.GetAttributeValue("confirmSound");
            }
            set
            {
                this.SetAttributeValue("confirmSound", value);
            }
        }

        /// <summary>
        /// Specifies digits that Avaya CPaaS should listen for and send to the callbackUrl if a caller inputs them.
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
        /// Boolean value specifying if call should be redirected to voicemail immediately. Note: only works if dialing TO a mobile number. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [straight to vm]; otherwise, <c>false</c>.
        /// </value>
        public bool StraightToVm
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("straightToVm"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("straightToVm", value.ToString());
            }
        }

        /// <summary>
        /// A URL Avaya CPaaS can request every 60 seconds during the call to notify of elapsed time and pass other general information.
        /// </summary>
        /// <value>
        /// The heartbeat URL.
        /// </value>
        public string HeartbeatUrl
        {
            get
            {
                return this.GetAttributeValue("heartbeatUrl");
            }
            set
            {
                this.SetAttributeValue("heartbeatUrl", value);
            }
        }

        /// <summary>
        /// Method used to request the heartbeatUrl. Default Value: POST. Allowed Value: POST or GET.
        /// </summary>
        /// <value>
        /// The heartbeat method.
        /// </value>
        public string heartbeatMethod
        {
            get
            {
                return this.GetAttributeValue("heartbeatMethod");
            }
            set
            {
                this.SetAttributeValue("heartbeatMethod", value);
            }
        }

        /// <summary>
        /// Specifies the number to list the call as forwarded from.
        /// </summary>
        /// <value>
        /// The forwarded from.
        /// </value>
        public string ForwardedFrom
        {
            get
            {
                return this.GetAttributeValue("forwardedFrom");
            }
            set
            {
                this.SetAttributeValue("forwardedFrom", value);
            }
        }

        /// <summary>
        /// Specifies how Avaya CPaaS should handle this dial if the receiving phone number is unanswered and goes to voicemail. “continue” to proceed as normal, “redirect” to redirect the call to the ifMachineUrl, or “hangup” to hangup the call. Please note: ifMachine could detect an answering machine via the tone stream. Therefore, the accuracy is around 90% and may not work in all countries. Default Value: continue. Allowed Value: continue, redirect, hangup.
        /// </summary>
        /// <value>
        /// If machine.
        /// </value>
        public IfMachineEnum IfMachine
        {
            get
            {
                IfMachineEnum value = IfMachineEnum.@continue;
                Enum.TryParse(this.GetAttributeValue("ifMachine"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("ifMachine", value.ToString());
            }
        }

        /// <summary>
        /// The URL Avaya CPaaS will redirect to if a voicemail machine is detected while the ifMachine =“redirect” attribute is set.
        /// </summary>
        /// <value>
        /// If machine URL.
        /// </value>
        public string IfMachineUrl
        {
            get
            {
                return this.GetAttributeValue("ifMachineUrl");
            }
            set
            {
                this.SetAttributeValue("ifMachineUrl", value);
            }
        }

        /// <summary>
        /// The method used to request the ifMachineUrl. Default Value: POST. Allowed Value: POST or GET.
        /// </summary>
        /// <value>
        /// If machine method.
        /// </value>
        public string IfMachineMethod
        {
            get
            {
                return this.GetAttributeValue("ifMachineMethod");
            }
            set
            {
                this.SetAttributeValue("ifMachineMethod", value);
            }
        }

        /// <summary>
        /// Specifies if this call should be recorded.Allowed positive values are "true" - any other value will default to "false".
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
        /// Specifies which stream of call audio to record. “in” to record the incoming caller audio, “out” to record the outgoing caller audio, or “both” to record all audio on the call. “out” audio can only be captured if an outbound <Dial> is performed during the call. “in” blocks any subsequent InboundXML elements until the inbound audio recording is finished (via finishOnKey or timeout). Default Value: both.Allowed Value: in, out, both.
        /// </summary>
        /// <value>
        /// The record direction.
        /// </value>
        public RecordDirectionEnum RecordDirection
        {
            get
            {
                RecordDirectionEnum value = RecordDirectionEnum.both;
                Enum.TryParse(this.GetAttributeValue("recordDirection"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("recordDirection", value.ToString());
            }
        }

        /// <summary>
        /// URL where some parameters specific to the recording will be sent for further processing.
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
        /// Initializes a new instance of the <see cref="ResponseNode"/> class.
        /// </summary>
        public DialNode(string value)
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
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        /// <param name="method">The method.</param>
        /// <param name="timeLimit">The time limit.</param>
        /// <param name="callerId">The caller identifier.</param>
        /// <param name="hideCallerId">The hide caller identifier.</param>
        /// <param name="dialMusic">The dial music.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="callbackMethod">The callback method.</param>
        /// <param name="confirmSound">The confirm sound.</param>
        /// <param name="digitsMatch">The digits match.</param>
        /// <param name="straightToVm">The straight to vm.</param>
        /// <param name="heartbeatUrl">The heartbeat URL.</param>
        /// <param name="heartbeatMethod">The heartbeat method.</param>
        /// <param name="forwardedFrom">The forwarded from.</param>
        /// <param name="ifMachine">If machine.</param>
        /// <param name="ifMachineUrl">If machine URL.</param>
        /// <param name="ifMachineMethod">If machine method.</param>
        /// <param name="record">The record.</param>
        /// <param name="recordDirection">The record direction.</param>
        /// <param name="recordCallbackUrl">The record callback URL.</param>
        /// <returns></returns>
        public static INodeCanInner<ResponseNode, DialNode> Dial(
            this INode<ResponseNode> responseNode,
            string value = null,
            string action = null,
            string method = null,
            int? timeLimit = null,
            string callerId = null,
            bool? hideCallerId = null,
            string dialMusic = null,
            string callbackUrl = null,
            string callbackMethod = null,
            string confirmSound = null,
            string digitsMatch = null,
            bool? straightToVm = null,
            string heartbeatUrl = null,
            string heartbeatMethod = null,
            string forwardedFrom = null,
            IfMachineEnum? ifMachine = null,
            string ifMachineUrl = null,
            string ifMachineMethod = null,
            bool? record = null,
            RecordDirectionEnum? recordDirection = null,
            string recordCallbackUrl = null
        )
        {
            // creates new dial node
            var dialNode = new DialNode(value)
            {
                Action = action,
                Method = method,
                CallerId = callerId,
                DialMusic = dialMusic,
                CallbackUrl = callbackUrl,
                CallbackMethod = callbackMethod,
                ConfirmSound = confirmSound,
                DigitsMatch = digitsMatch,
                HeartbeatUrl = heartbeatUrl,
                heartbeatMethod = heartbeatMethod,
                ForwardedFrom = forwardedFrom,
                IfMachineUrl = ifMachineUrl,
                IfMachineMethod = ifMachineMethod,
                RecordCallbackUrl = recordCallbackUrl
            };

            // sets the values
            if (timeLimit.HasValue) dialNode.TimeLimit = timeLimit.Value;
            if (hideCallerId.HasValue) dialNode.HideCallerId = hideCallerId.Value;
            if (straightToVm.HasValue) dialNode.StraightToVm = straightToVm.Value;
            if (ifMachine.HasValue) dialNode.IfMachine = ifMachine.Value;
            if (record.HasValue) dialNode.Record = record.Value;
            if (recordDirection.HasValue) dialNode.RecordDirection = recordDirection.Value;

            // retrieves the response
            var response = responseNode.CurrentNode;

            // adds the dial node as child to response
            response.Add(dialNode);

            // retruns the node that can have inner
            return new InboundXmlNodeCanInner<ResponseNode, DialNode>(response, dialNode);
        }
    }
}
