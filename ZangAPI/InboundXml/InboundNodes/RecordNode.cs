using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZangAPI.InboundXml.Enums;

namespace ZangAPI.InboundXml
{
    /// <summary>
    /// The Record node for the Inbound XML builder.
    /// </summary>
    /// <seealso cref="ZangAPI.InboundXml.ANode" />
    public class RecordNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "Record";

        /// <summary>
        /// URL where some parameters specific to <Record> will be sent for further processing.
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
        /// Method used to request the action URL. Default value is POST. Available values are POST and GET.
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
        /// The number of seconds <Record> should wait during silence before ending. Default timeout value is 5 seconds. Valid value is integer greater than or equal to 0.
        /// </summary>
        /// <value>
        /// The timeout.
        /// </value>
        public int Timeout
        {
            get
            {
                int value = 5;
                Int32.TryParse(this.GetAttributeValue("timeout"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("timeout", value.ToString());
            }
        }

        /// <summary>
        /// The key a caller can press to end the <Record>. Default value is #. Allowed values are digits from 0 to 9, # or *.
        /// </summary>
        /// <value>
        /// The finish on key.
        /// </value>
        public string FinishOnKey
        {
            get
            {
                return this.GetAttributeValue("finishOnKey");
            }
            set
            {
                this.SetAttributeValue("finishOnKey", value);
            }
        }

        /// <summary>
        /// The maximum length in seconds a recording should be. Default Value: 3600. Allowed Value: integer greater than or equal to 0
        /// </summary>
        /// <value>
        /// The maximum length.
        /// </value>
        public int MaxLength
        {
            get
            {
                int value = 3600;
                Int32.TryParse(this.GetAttributeValue("maxLength"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("maxLength", value.ToString());
            }
        }

        /// <summary>
        /// Boolean value specifying if the recording should be transcribed. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if transcribe; otherwise, <c>false</c>.
        /// </value>
        public bool Transcribe
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("transcribe"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("transcribe", value.ToString());
            }
        }

        /// <summary>
        /// Specifies the quality used to process the transcription. Default Value: autoAllowed Value: auto or hybrid.
        /// </summary>
        /// <value>
        /// The transcribe quality.
        /// </value>
        public TranscribeQualityEnum TranscribeQuality
        {
            get
            {
                TranscribeQualityEnum value = TranscribeQualityEnum.auto;
                Enum.TryParse(this.GetAttributeValue("transcribeQuality"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("transcribeQuality", value.ToString());
            }
        }

        /// <summary>
        /// A URL some parameters regarding the transcription will be passed to once it is completed. Allowed Value: Valid URL.
        /// </summary>
        /// <value>
        /// The transcribe callback.
        /// </value>
        public string TranscribeCallback
        {
            get
            {
                return this.GetAttributeValue("transcribeCallback");
            }
            set
            {
                this.SetAttributeValue("transcribeCallback", value);
            }
        }

        /// <summary>
        /// Boolean value specifying if a beep should be played when the recording begins. Default Value: false. Allowed Value: true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [play beep]; otherwise, <c>false</c>.
        /// </value>
        public bool PlayBeep
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("playBeep"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("playBeep", value.ToString());
            }
        }

        /// <summary>
        /// Specifies which stream of call audio to record. “in” to record the incoming caller audio, “out” to record the out going caller audio, or “both” to record all audio on the call. “out” audio can only be captured if an outbound Dial is performed during the call. “in” blocks any subsequent XML elements until the inbound audio recording is finished (via finishOnKey or timeout). Default Value: both. Allowed Value: in, out, both.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public RecordDirectionEnum Direction
        {
            get
            {
                RecordDirectionEnum value = RecordDirectionEnum.both;
                Enum.TryParse(this.GetAttributeValue("direction"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("direction", value.ToString());
            }
        }

        /// <summary>
        /// The recording file format. Can be mp3 or wav. Default is mp3. Default Value: mp3. Allowed Value: mp3 or wav
        /// </summary>
        /// <value>
        /// The file format.
        /// </value>
        public RecordFileFormatEnum FileFormat
        {
            get
            {
                RecordFileFormatEnum value = RecordFileFormatEnum.mp3;
                Enum.TryParse(this.GetAttributeValue("fileFormat"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("fileFormat", value.ToString());
            }
        }

        /// <summary>
        /// Begin recording the call while continuing the execution of any other present InboundXML in the background (true) or block the execution of subsequent InboundXML until the record element finishes (via finishOnKey or timeout). Note that the timeout, finishOnKey, and playBeep attributes have no effect when the background is set to true. Default Value: true. Allowed Value: true or false
        /// </summary>
        /// <value>
        ///   <c>true</c> if background; otherwise, <c>false</c>.
        /// </value>
        public bool Background
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("background"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("background", value.ToString());
            }
        }

        /// <summary>
        /// Trims all silence from the beginning of the recording. Allowed values are "true" or "false" - any other value will default to "false".
        /// </summary>
        /// <value>
        ///   <c>true</c> if [trim silence]; otherwise, <c>false</c>.
        /// </value>
        public bool TrimSilence
        {
            get
            {
                bool value = false;
                Boolean.TryParse(this.GetAttributeValue("trimSilence"), out value);
                return value;
            }
            set
            {
                this.SetAttributeValue("trimSilence", value.ToString());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordNode"/> class.
        /// </summary>
        public RecordNode()
            : base(NODE_NAME)
        {
        }
    }

    /// <summary>
    /// Extensions for the Record node.
    /// </summary>
    public static class RecordNodeExtensions
    {
        public static INode<ResponseNode> Record(
            this INode<ResponseNode> responseNode,
            string action = null,
            string method = null,
            int? timeout = null,
            string finishOnKey = null,
            int? maxLength = null,
            bool? transcribe = null,
            TranscribeQualityEnum? transcribeQuality = null,
            string transcribeCallback = null,
            bool? playBeep = null,
            RecordDirectionEnum? direction = null,
            RecordFileFormatEnum? fileFormat = null,
            bool? background = null,
            bool? trimSilence = null
        )
        {
            // creates the new record node
            var recordNode = new RecordNode()
            {
                Action = action,
                Method = method,
                FinishOnKey = finishOnKey,
                TranscribeCallback = transcribeCallback
            };

            // sets the values
            if (timeout.HasValue) recordNode.Timeout = timeout.Value;
            if (maxLength.HasValue) recordNode.MaxLength = maxLength.Value;
            if (transcribe.HasValue) recordNode.Transcribe = transcribe.Value;
            if (transcribeQuality.HasValue) recordNode.TranscribeQuality = transcribeQuality.Value;
            if (playBeep.HasValue) recordNode.PlayBeep = playBeep.Value;
            if (direction.HasValue) recordNode.Direction = direction.Value;
            if (fileFormat.HasValue) recordNode.FileFormat = fileFormat.Value;
            if (background.HasValue) recordNode.Background = background.Value;
            if (trimSilence.HasValue) recordNode.TrimSilence = trimSilence.Value;

            // adds the record node to the response
            responseNode.CurrentNode.Add(recordNode);

            // returns the response node
            return responseNode;
        }
    }
}
