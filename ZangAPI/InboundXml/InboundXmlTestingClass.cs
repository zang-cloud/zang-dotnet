using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZangAPI.InboundXml
{
    public class InboundXmlTestingClass
    {
        public static void Test()
        {
            // creates the new builder
            var builder = new InboundXmlBuilder();
            builder.Gather()
                .StartInner()
                .Play(toneStream: "tone://")
                .EndInner()
            .ToString();
        }
    }
}
