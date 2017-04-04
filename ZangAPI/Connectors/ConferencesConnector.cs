using System;
using RestSharp;
using RestSharp.Extensions;
using ZangAPI.ConnectionManager;
using ZangAPI.Helpers;
using ZangAPI.Model;
using ZangAPI.Model.Enums;
using ZangAPI.Model.Lists;

namespace ZangAPI.Connectors
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ZangAPI.Connectors.AConnector" />
    public class ConferencesConnector : AConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConferencesConnector"/> class.
        /// </summary>
        /// <param name="httpProvider">The HTTP provider.</param>
        public ConferencesConnector(IHttpProvider httpProvider) 
            : base(httpProvider)
        {
        }

        /// <summary>
        /// Views the conference.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <returns>Returns conference</returns>
        public Conference ViewConference(string accountSid, string conferenceSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Conferences/{conferenceSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Conference>(response);
        }

        /// <summary>
        /// Views the conference. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <returns>Returns conference</returns>
        public Conference ViewConference(string conferenceSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewConference(accountSid, conferenceSid);
        }

        /// <summary>
        /// Lists the conferences.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="status">The status.</param>
        /// <param name="dateCreatedGte">The date created gte.</param>
        /// <param name="dateCreatedLt">The date created lt.</param>
        /// <param name="dateUpdatedGte">The date updated gte.</param>
        /// <param name="dateUpdatedLt">The date updated lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns conference list</returns>
        public ConferenceList ListConferences(string accountSid, string friendlyName = null, ConferenceStatus? status = null,
           DateTime dateCreatedGte = default(DateTime), DateTime dateCreatedLt = default(DateTime),
            DateTime dateUpdatedGte = default(DateTime), DateTime dateUpdatedLt = default(DateTime),
           int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Conferences.json");

            // Add ListConferences query and body parameters
            this.SetParamsForListConferences(request, friendlyName, status, dateCreatedGte, dateCreatedLt, dateUpdatedGte, dateUpdatedLt,
                page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ConferenceList>(response);
        }

        /// <summary>
        /// Lists the conferences. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="status">The status.</param>
        /// <param name="dateCreatedGte">The date created gte.</param>
        /// <param name="dateCreatedLt">The date created lt.</param>
        /// <param name="dateUpdatedGte">The date updated gte.</param>
        /// <param name="dateUpdatedLt">The date updated lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns conference list</returns>
        public ConferenceList ListConferences(string friendlyName = null, ConferenceStatus? status = null,
           DateTime dateCreatedGte = default(DateTime), DateTime dateCreatedLt = default(DateTime),
            DateTime dateUpdatedGte = default(DateTime), DateTime dateUpdatedLt = default(DateTime),
           int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListConferences(accountSid, friendlyName, status, dateCreatedGte, dateCreatedLt, dateUpdatedGte, dateUpdatedLt, 
                page, pageSize);
        }

        /// <summary>
        /// Views the participant.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="participantSid">The participant sid.</param>
        /// <returns>Returns participant</returns>
        public Participant ViewParticipant(string accountSid, string conferenceSid, string participantSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Participant>(response);
        }

        /// <summary>
        /// Views the participant. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="participantSid">The participant sid.</param>
        /// <returns>Returns participant</returns>
        public Participant ViewParticipant(string conferenceSid, string participantSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewParticipant(accountSid, conferenceSid, participantSid);
        }

        /// <summary>
        /// Lists the participants.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="muted">if set to <c>true</c> [muted].</param>
        /// <param name="deaf">if set to <c>true</c> [deaf].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns participant list</returns>
        public ParticipantList ListParticipants(string accountSid, string conferenceSid, 
            bool muted = false, bool deaf = false, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants.json");

            // Add ListParticipants query and body parameters
            this.SetParamsForListParticipants(request, muted, deaf, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ParticipantList>(response);
        }

        /// <summary>
        /// Lists the participants. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="muted">if set to <c>true</c> [muted].</param>
        /// <param name="deaf">if set to <c>true</c> [deaf].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns participant list</returns>
        public ParticipantList ListParticipants(string conferenceSid,
            bool muted = false, bool deaf = false, int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListParticipants(accountSid, conferenceSid, muted, deaf, page, pageSize);
        }

        /// <summary>
        /// Mutes the or deaf participant.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="participantSid">The participant sid.</param>
        /// <param name="muted">if set to <c>true</c> [muted].</param>
        /// <param name="deaf">if set to <c>true</c> [deaf].</param>
        /// <returns>Returns participant</returns>
        public Participant MuteOrDeafParticipant(string accountSid, string conferenceSid, string participantSid,
            bool muted = false, bool deaf = false)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json");

            // Add MuteOrDeafParticipant query and body parameters
            this.SetParamsForMuteOrDeafParticipant(request, muted, deaf);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Participant>(response);
        }

        /// <summary>
        /// Mutes the or deaf participant. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="participantSid">The participant sid.</param>
        /// <param name="muted">if set to <c>true</c> [muted].</param>
        /// <param name="deaf">if set to <c>true</c> [deaf].</param>
        /// <returns>Returns participant</returns>
        public Participant MuteOrDeafParticipant(string conferenceSid, string participantSid,
            bool muted = false, bool deaf = false)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.MuteOrDeafParticipant(accountSid, conferenceSid, participantSid, muted, deaf);
        }

        /// <summary>
        /// Plays the audio to participant.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="participantSid">The participant sid.</param>
        /// <param name="audioUrl">The audio URL.</param>
        /// <returns>Returns participant</returns>
        public Participant PlayAudioToParticipant(string accountSid, string conferenceSid, string participantSid, string audioUrl = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST, $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}/Play.json");

            // Add body parameter
            if (audioUrl.HasValue()) request.AddParameter("AudioUrl", audioUrl);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Participant>(response);
        }

        /// <summary>
        /// Plays the audio to participant. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="participantSid">The participant sid.</param>
        /// <param name="audioUrl">The audio URL.</param>
        /// <returns>Returns participant</returns>
        public Participant PlayAudioToParticipant(string conferenceSid, string participantSid, string audioUrl = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.PlayAudioToParticipant(accountSid, conferenceSid, participantSid, audioUrl);
        }

        /// <summary>
        /// Hangups the participant.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="participantSid">The participant sid.</param>
        /// <returns>Returns participant</returns>
        public Participant HangupParticipant(string accountSid, string conferenceSid, string participantSid = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE, $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Participant>(response);
        }

        /// <summary>
        /// Hangups the participant. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">The conference sid.</param>
        /// <param name="participantSid">The participant sid.</param>
        /// <returns>Returns participant</returns>
        public Participant HangupParticipant(string conferenceSid, string participantSid = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.HangupParticipant(accountSid, conferenceSid, participantSid);
        }

        /// <summary>
        /// Sets the parameters for list conferences.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="status">The status.</param>
        /// <param name="dateCreatedGte">The date created gte.</param>
        /// <param name="dateCreatedLt">The date created lt.</param>
        /// <param name="dateUpdatedGte">The date updated gte.</param>
        /// <param name="dateUpdatedLt">The date updated lt.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListConferences(IRestRequest request, string friendlyName, ConferenceStatus? status, DateTime dateCreatedGte,
            DateTime dateCreatedLt, DateTime dateUpdatedGte, DateTime dateUpdatedLt, int? page, int? pageSize)
        {
            if (friendlyName.HasValue()) request.AddQueryParameter("FriendlyName", friendlyName);
            if (status != null) request.AddQueryParameter("Status", EnumHelper.GetEnumValue(status));
            if (dateCreatedGte != default(DateTime))
                request.AddQueryParameter("DateCreated>", dateCreatedGte.ToString("yyyy-MM-dd"));
            if (dateCreatedLt != default(DateTime))
                request.AddQueryParameter("DateCreated<", dateCreatedLt.ToString("yyyy-MM-dd"));
            if (dateUpdatedGte != default(DateTime))
                request.AddQueryParameter("DateUpdated>", dateUpdatedGte.ToString("yyyy-MM-dd"));
            if (dateUpdatedLt != default(DateTime))
                request.AddQueryParameter("DateUpdated<", dateUpdatedLt.ToString("yyyy-MM-dd"));
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }

        /// <summary>
        /// Sets the parameters for list participants.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="muted">if set to <c>true</c> [muted].</param>
        /// <param name="deaf">if set to <c>true</c> [deaf].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void SetParamsForListParticipants(IRestRequest request, bool muted, bool deaf, int? page, int? pageSize)
        {
            request.AddQueryParameter("Muted", muted.ToString());
            request.AddQueryParameter("Deaf", deaf.ToString());
            if (page != null) request.AddQueryParameter("Page", page.ToString());
            if (pageSize != null) request.AddQueryParameter("PageSize", pageSize.ToString());
        }

        /// <summary>
        /// Sets the parameters for mute or deaf participant.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="muted">if set to <c>true</c> [muted].</param>
        /// <param name="deaf">if set to <c>true</c> [deaf].</param>
        private void SetParamsForMuteOrDeafParticipant(IRestRequest request, bool muted, bool deaf)
        {
            request.AddParameter("Muted", muted.ToString());
            request.AddParameter("Deaf", deaf.ToString());
        }
    }
}
