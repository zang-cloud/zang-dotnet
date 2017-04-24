using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;
using ZangAPI.Model.Enums;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Zang service to work with conferences
    /// </summary>
    public class ConferencesConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

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
            catch (ZangException e)
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
            catch (ZangException e)
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
            catch (ZangException e)
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
            catch (ZangException e)
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
            catch (ZangException e)
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
            catch (ZangException e)
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
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
