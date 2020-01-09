using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;
using ZangAPI.Model.Enums;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with calls
    /// </summary>
    public class CallsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing call
        /// </summary>
        public void ViewCall()
        {
            try
            {
                // View call using calls connector
                var call = service.CallsConnector.ViewCall("TestCallSid");
                Console.WriteLine(call.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing calls
        /// </summary>
        public void ListCalls()
        {
            try
            {
                // List calls using calls connector
                var calls = service.CallsConnector.ListCalls("+123456",
                    "+654321", CallStatus.COMPLETED, new DateTime(2017, 2, 20), new DateTime(2017, 3, 1));
                Console.WriteLine(calls.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of making call
        /// </summary>
        public void MakeCall()
        {
            try
            {
                // Make call using calls connector
                var call = service.CallsConnector.MakeCall("+12345", "+112233", "testUrl", transcribe:true, transcribeCallback:"transcribeCallback");
                Console.WriteLine(call.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of interrupting live call
        /// </summary>
        public void InterruptLiveCall()
        {
            try
            {
                // Interrupt live call using calls connector
                var call = service.CallsConnector.InterruptLiveCall("TestCallSid", "TestUrl", HttpMethod.GET, CallStatus.CANCELED);
                Console.WriteLine(call.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of sending digits to live call
        /// </summary>
        public void SendDigitsToLiveCall()
        {
            try
            {
                // Send digits to live call using calls connector
                var call = service.CallsConnector.SendDigitsToLiveCall("TestCallSid", "ww12w3221", AudioDirection.OUT);
                Console.WriteLine(call.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of recording live call
        /// </summary>
        public void RecordLiveCall()
        {
            try
            {
                // Record live call using calls connector
                var call = service.CallsConnector.RecordLiveCall("TestCallSid", true, RecordingAudioDirection.IN, 12, "CallCallbackUrl",
                    RecordingFileFormat.MP3, true);
                Console.WriteLine(call.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of playing audio to live call
        /// </summary>
        public void PlayAudioToLiveCall()
        {
            try
            {
                // Plaz audio to live call using calls connector
                var call = service.CallsConnector.PlayAudioToLiveCall("TestCallSid", "AudioUrl");
                Console.WriteLine(call.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of applying voice effect
        /// </summary>
        public void ApplyVoiceEffect()
        {
            try
            {
                // Apply voice effect using calls connector
                var call = service.CallsConnector.ApplyVoiceEffect("TestCallSid", AudioDirection.OUT, 2, 3, 2, 3, 2);
                Console.WriteLine(call.Duration);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
