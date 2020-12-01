using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvayaCPaaS.InboundXml;
using System.Text.RegularExpressions;
using AvayaCPaaS.InboundXml.Enums;
using AvayaCPaaS.InboundXml.InboundNodes;

namespace AvayaCPaaS.Tests.InboundXmlTests
{
    [TestClass]
    public class InboundXmlBuilderTests
    {
        /// <summary>
        /// The expected XML string for testing,
        /// </summary>
        protected static readonly string EXPTECTED_XML_STRING = "<Response><Dial><Conference /><Number /><Sip /></Dial><Gather language=\"ar-AE\" input=\"speech\"><Say /><Play /><Pause /></Gather><Hangup /><Ping /><Pause /><Play /><PlayLastRecording /><Record /><Redirect /><Reject /><Say /><Sms to=\"+12345\" from=\"+34567\" /></Response>";

        /// <summary>
        /// The expected XML string for testing connect element.
        /// </summary>
        protected static readonly string EXPECTED_CONNECT_XML_STRING = "<Response><Connect><Agent /></Connect></Response>";

        /// <summary>
        /// The expected XML string for testing connect element with attributes.
        /// </summary>
        protected static readonly string EXPECTED_CONNECT_XML_STRING_ATTR = "<Response><Connect action=\"http://sample\" method=\"POST\"><Agent>1234</Agent></Connect></Response>";

        /// <summary>
        /// Removes the XML whitespace.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>Xml string without whitespace.</returns>
        protected static string RemoveXmlWhitespace(string inputString)
        {
            Regex regex = new Regex(@">\s*<");
            return regex.Replace(inputString, "><");
        }

        [TestMethod]
        public void InboundXmlTest()
        {
            // creates the new builder
            var builder = new InboundXmlBuilder();

            // creates the node
            builder.GetRequestNode()
                .Dial()
                    .StartInner()
                    .Conference()
                    .Number()
                    .Sip()
                    .EndInner()
                .Gather(input: GatherInputEnum.speech, language: BCPLanguageEnum.ar_ae)
                    .StartInner()
                    .Say()
                    .Play()
                    .Pause()
                    .EndInner()
                .Hangup()
                .Ping()
                .Pause()
                .Play()
                .PlayLastRecording()
                .Record()
                .Redirect()
                .Reject()
                .Say()
                .Sms("+12345", "+34567");

            // exports the node
            var stringData = RemoveXmlWhitespace(builder.Build());

            // does the test
            Assert.AreEqual(EXPTECTED_XML_STRING, stringData);
        }

        [TestMethod]
        public void InboundXmlConnectTest()
        {
            // creates the new builder
            var builder = new InboundXmlBuilder();

            // creates the node
            builder.GetRequestNode()
                .Connect()
                    .StartInner()
                    .Agent()
                    .EndInner();

            // exports the node
            var stringData = RemoveXmlWhitespace(builder.Build());

            // does the test
            Assert.AreEqual(EXPECTED_CONNECT_XML_STRING, stringData);
        }

        [TestMethod]
        public void InboundXmlConnectTestWithAttributes()
        {
            // creates the new builder
            var builder = new InboundXmlBuilder();

            // creates the node
            builder.GetRequestNode()
                .Connect("http://sample","POST")
                    .StartInner()
                    .Agent("1234")
                    .EndInner();

            // exports the node
            var stringData = RemoveXmlWhitespace(builder.Build());

            // does the test
            Assert.AreEqual(EXPECTED_CONNECT_XML_STRING_ATTR, stringData);
        }

        [TestMethod]
        public void InboundXmlWithAttributesTest()
        {
            // creates the new builder
            var builder = new InboundXmlBuilder();

            // creates the node
            builder.GetRequestNode()
                .Dial("(555)555-5555", "dial action", "POST", 15, "caller id", false, "dial music", "dial callback url", "POST", "dial confirm sound", "ww12w3221", false, 
                "dial heartbeat url", "POST", "dial forwarded from", IfMachineEnum.@continue, "dial if machine url", "POST", false, RecordDirectionEnum.@out, "dial record callback url")
                    .StartInner()
                    .Conference("CPaaSExampleChat", false, true, true, true, 15, "conference wait sound", false, "conference callback url", "POST", "ww12w3221", false, false, "conference record callback url", RecordFileFormatEnum.wav)
                    .Number("(555)555-5555", "ww12w3221")
                    .Sip("username@domain.com", "username", "password")
                    .EndInner()
                .Gather(input: GatherInputEnum.speech, language: BCPLanguageEnum.ar_ae)
                    .StartInner()
                    .Say("Ready for pause?", loop:6)
                    .Play("play tone stream", loop: 4)
                    .Pause(length:2)
                    .EndInner()
                .Hangup(schedule:4, reason:HangupReasonEnum.rejected)
                .Ping("http://webhookr.com/ping-test", "POST")
                .Pause(length:5)
                .Play("play tone stream", loop:2)
                .PlayLastRecording()
                .Record("record action", "POST", 8, "5", 3000, true, TranscribeQualityEnum.auto, "record transcribe callback", true, RecordDirectionEnum.@in, RecordFileFormatEnum.mp3, 
                        true, false)
                .Redirect("redirect", "POST")
                .Reject(HangupReasonEnum.busy)
                .Say(value:"I want to say sth!", voice:VoiceEnum.female, language:LanguageEnum.en, loop:3)
                .Sms("+12345", "+34567", "Test message from CPaaS", "sms action", "POST", "sms status callback");

            var data = builder.Build();
        }
    }
}
