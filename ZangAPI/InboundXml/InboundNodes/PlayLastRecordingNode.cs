using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml.InboundNodes
{
    public class PlayLastRecordingNode : ANode
    {
        /// <summary>
        /// The node name.
        /// </summary>
        const string NODE_NAME = "PlayLastRecording";

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayLastRecordingNode"/> class.
        /// </summary>
        public PlayLastRecordingNode()
            : base(NODE_NAME)
        {
        }
    }
}
