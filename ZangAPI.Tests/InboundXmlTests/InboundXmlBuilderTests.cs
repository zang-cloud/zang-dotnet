using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZangAPI.InboundXml;
using System.Text.RegularExpressions;

namespace ZangAPI.Tests.InboundXmlTests
{
    [TestClass]
    public class InboundXmlBuilderTests
    {
        /// <summary>
        /// The exptected XML string for testing,
        /// </summary>
        protected static readonly string EXPTECTED_XML_STRING = "<Response><Dial><Conference /><Number /><Sip /></Dial><Gather><Say /><Play /><Pause /></Gather><Hangup /><PingNode /><Pause /><Play /><PlayLastRecording /><Record /><Redirect /><Reject /><Say /><Sms /></Response>";

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
                .Gather()
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
                .Sms();

            // exports the node
            var stringData = RemoveXmlWhitespace(builder.ToString());

            // does the test
            Assert.AreEqual(EXPTECTED_XML_STRING, stringData);
        }
    }
}
