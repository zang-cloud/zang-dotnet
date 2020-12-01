using System;
using AvayaCPaaS.Exceptions;
using AvayaCPaaS.InboundXml;
using AvayaCPaaS.InboundXml.Enums;
using AvayaCPaaS.InboundXml.InboundNodes;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Inbound XML builder
    /// </summary>
    public class InboundXmlExamples
    {
        /// <summary>
        /// Example of using Inbound XML builder
        /// </summary>
        public static void Example()
        {
            try
            {
                var builder = new InboundXmlBuilder();
                builder.GetRequestNode()
                .Dial("(555)555-5555", hideCallerId: false, dialMusic: "dial music", confirmSound: "dial confirm sound", digitsMatch: "ww12w3221",
                        record: false, recordDirection: RecordDirectionEnum.@out)
                    .StartInner()
                    .Sip("username@domain.com", "username", "password")
                    .EndInner()
                .Gather(input: GatherInputEnum.speech, language: BCPLanguageEnum.ar_ae)
                    .StartInner()
                    .Say(language: LanguageEnum.en, loop: 3, value: "Welcome to CPaaS!", voice: VoiceEnum.female)
                    .Pause(length: 2)
                    .EndInner()
                .Mms(to: "+123456", from: "+654321", mediaUrl: "https://media.giphy.com/media/zZJzLrxmx5ZFS/giphy.gif")
                .Hangup(schedule: 4, reason: HangupReasonEnum.rejected);

                var result = builder.Build();

                Console.WriteLine(result);

                // creates the connect node
                builder.GetRequestNode()
                    .Connect("sample", "POST")
                        .StartInner()
                        .Agent("1234")
                        .EndInner();

                // exports the node
                var connectStringData = builder.Build();

                Console.WriteLine(connectStringData);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
