using System;
using ZangAPI.Exceptions;
using ZangAPI.InboundXml;
using ZangAPI.InboundXml.Enums;

namespace ZangAPI.Examples.Examples
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
                .Dial("(555)555-5555", hideCallerId:false, dialMusic:"dial music", confirmSound:"dial confirm sound", digitsMatch:"ww12w3221",
                        record:false, recordDirection:RecordDirectionEnum.@out)
                    .StartInner()
                    .Sip("username@domain.com", "username", "password")
                    .EndInner()
                .Gather()
                    .StartInner()
                    .Say(language: LanguageEnum.en, loop: 3, value: "Welcome to Zang!", voice: VoiceEnum.female)
                    .Pause(length: 2)
                    .EndInner()
                .Hangup(schedule: 4, reason: HangupReasonEnum.rejected);

                var result = builder.Build();
                           
                Console.WriteLine(result);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        } 
    }
}
