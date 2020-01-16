using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;
using AvayaCPaaS.Model.Enums;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with conferences
    /// </summary>
    public class ConferencesConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing conference
        /// </summary>
        public void ViewConference()
        {
            try
            {
                // View conference using conferences connector
                var conference = service.ConferencesConnector.ViewConference("TestConferenceSid");
                Console.WriteLine(conference.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing conferences
        /// </summary>
        public void ListConferences()
        {
            try
            {
                // List conferences using conferences connector
                var conferences = service.ConferencesConnector.ListConferences("TestConference",
                    ConferenceStatus.COMPLETED, dateUpdatedGte: new DateTime(2017, 3, 13),
                    dateUpdatedLt: new DateTime(2017, 3, 16));
                Console.WriteLine(conferences.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of viewing participant
        /// </summary>
        public void ViewParticipant()
        {
            try
            {
                // View participant using conferences connector
                var participant = service.ConferencesConnector.ViewParticipant("TestConferenceSid", "TestParticipantSid");
                Console.WriteLine(participant.Muted);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing participants
        /// </summary>
        public void ListParticipants()
        {
            try
            {
                // List participants using conferences connector
                var participants = service.ConferencesConnector.ListParticipants("TestConferenceSid", true);
                Console.WriteLine(participants.Total);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of mute or deaf participant
        /// </summary>
        public void MuteOrDeafParticipant()
        {
            try
            {
                // Mute or deaf participant using conferences connector
                var participant = service.ConferencesConnector.MuteOrDeafParticipant("TestConferenceSid", "TestParticipantSid", deaf: true);
                Console.WriteLine(participant.Deaf);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of playing audio to participant
        /// </summary>
        public void PlayAudioToParticipant()
        {
            try
            {
                // Play audio to participant using conferences connector
                var participant = service.ConferencesConnector.PlayAudioToParticipant("TestConferenceSid", "TestParticipantSid", "http://mydomain.com/audio.mp3");
                Console.WriteLine(participant.CallerName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of hangup participant
        /// </summary>
        public void HangupParticipant()
        {
            try
            {
                // Hangup participant using conferences connector
                var participant = service.ConferencesConnector.HangupParticipant("TestConferenceSid", "TestParticipantSid");
                Console.WriteLine(participant.CallerName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
