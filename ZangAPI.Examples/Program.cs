using ZangAPI.Examples.Examples;

namespace ZangAPI.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            InboundXmlExamples.Example();

            var callsConnectorExamples = new CallsConnectorExamples();
            callsConnectorExamples.ListCalls();

            var mmsConnectorExample = new MmsConnectorExamples();
            mmsConnectorExample.SendMms();
        }
    }
}
