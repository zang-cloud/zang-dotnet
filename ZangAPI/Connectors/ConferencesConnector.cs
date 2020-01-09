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
    /// Conferences connector - used for all forms of communication with the Conferences endpoint of the Avaya CPaaS REST API
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
        /// Shows information on some conference
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <returns>Returns conference</returns>
        public Conference ViewConference(string accountSid, string conferenceSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Conferences/{conferenceSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Conference>(response);
        }

        /// <summary>
        /// Shows information on some conference. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <returns>Returns conference</returns>
        public Conference ViewConference(string conferenceSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewConference(accountSid, conferenceSid);
        }

        /// <summary>
        /// Shows information on all conferences associated with some account
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="friendlyName">Filters conferences by the given FriendlyName.</param>
        /// <param name="status">Filters conferences by the given status. Allowed values are "init", "in-progress", or "completed".</param>
        /// <param name="dateCreatedGte">Filter by date created greater or equal than.</param>
        /// <param name="dateCreatedLt">Filter by date created less than.</param>
        /// <param name="dateUpdatedGte">Filter by date updated greater or equal than.</param>
        /// <param name="dateUpdatedLt">Filter by date updated less than.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns conference list</returns>
        public ConferencesList ListConferences(string accountSid, string friendlyName = null,
            ConferenceStatus? status = null, DateTime dateCreatedGte = default(DateTime),
            DateTime dateCreatedLt = default(DateTime),
            DateTime dateUpdatedGte = default(DateTime), DateTime dateUpdatedLt = default(DateTime),
            int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET, $"Accounts/{accountSid}/Conferences.json");

            // Add ListConferences query and body parameters
            this.SetParamsForListConferences(request, friendlyName, status, dateCreatedGte, dateCreatedLt,
                dateUpdatedGte, dateUpdatedLt,
                page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ConferencesList>(response);
        }

        /// <summary>
        /// Shows information on all conferences associated with some account. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="friendlyName">Filters conferences by the given FriendlyName.</param>
        /// <param name="status">Filters conferences by the given status. Allowed values are "init", "in-progress", or "completed".</param>
        /// <param name="dateCreatedGte">Filter by date created greater or equal than.</param>
        /// <param name="dateCreatedLt">Filter by date created less than.</param>
        /// <param name="dateUpdatedGte">Filter by date updated greater or equal than.</param>
        /// <param name="dateUpdatedLt">Filter by date updated less than.</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns conference list</returns>
        public ConferencesList ListConferences(string friendlyName = null, ConferenceStatus? status = null,
            DateTime dateCreatedGte = default(DateTime), DateTime dateCreatedLt = default(DateTime),
            DateTime dateUpdatedGte = default(DateTime), DateTime dateUpdatedLt = default(DateTime),
            int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListConferences(accountSid, friendlyName, status, dateCreatedGte, dateCreatedLt, dateUpdatedGte,
                dateUpdatedLt,
                page, pageSize);
        }

        /// <summary>
        /// Shows info on some conference participant
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="participantSid">Participant SID.</param>
        /// <returns>Returns participant</returns>
        public Participant ViewParticipant(string accountSid, string conferenceSid, string participantSid)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Participant>(response);
        }

        /// <summary>
        /// Shows info on some conference participant. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="participantSid">Participant SID.</param>
        /// <returns>Returns participant</returns>
        public Participant ViewParticipant(string conferenceSid, string participantSid)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ViewParticipant(accountSid, conferenceSid, participantSid);
        }

        /// <summary>
        /// Options include filtering by muted or deaf.
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="muted">Filter by participants that are muted. Allowed values are "true" or "false".</param>
        /// <param name="deaf">Filter by participants that are deaf. Allowed values are "true" or "false".</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns participant list</returns>
        public ParticipantsList ListParticipants(string accountSid, string conferenceSid,
            bool muted = false, bool deaf = false, int? page = null, int? pageSize = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create GET request
            var request = RestRequestHelper.CreateRestRequest(Method.GET,
                $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants.json");

            // Add ListParticipants query and body parameters
            this.SetParamsForListParticipants(request, muted, deaf, page, pageSize);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<ParticipantsList>(response);
        }

        /// <summary>
        /// Options include filtering by muted or deaf. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="muted">Filter by participants that are muted. Allowed values are "true" or "false".</param>
        /// <param name="deaf">Filter by participants that are deaf. Allowed values are "true" or "false".</param>
        /// <param name="page">Used to return a particular page within the list.</param>
        /// <param name="pageSize">Used to specify the amount of list items to return per page.</param>
        /// <returns>Returns participant list</returns>
        public ParticipantsList ListParticipants(string conferenceSid,
            bool muted = false, bool deaf = false, int? page = null, int? pageSize = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.ListParticipants(accountSid, conferenceSid, muted, deaf, page, pageSize);
        }

        /// <summary>
        /// Sets participant in conference to mute or deaf
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="participantSid">Participant SID.</param>
        /// <param name="muted">Specifies whether the participant should be muted. Allowed values are "true" and "false".</param>
        /// <param name="deaf">Specifies whether the participant should be deaf. Allowed values are "true" and "false".</param>
        /// <returns>Returns participant</returns>
        public Participant MuteOrDeafParticipant(string accountSid, string conferenceSid, string participantSid,
            bool muted = false, bool deaf = false)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json");

            // Add MuteOrDeafParticipant query and body parameters
            this.SetParamsForMuteOrDeafParticipant(request, muted, deaf);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Participant>(response);
        }

        /// <summary>
        /// Sets participant in conference to mute or deaf. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="participantSid">Participant SID.</param>
        /// <param name="muted">Specifies whether the participant should be muted. Allowed values are "true" and "false".</param>
        /// <param name="deaf">Specifies whether the participant should be deaf. Allowed values are "true" and "false".</param>
        /// <returns>Returns participant</returns>
        public Participant MuteOrDeafParticipant(string conferenceSid, string participantSid,
            bool muted = false, bool deaf = false)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.MuteOrDeafParticipant(accountSid, conferenceSid, participantSid, muted, deaf);
        }

        /// <summary>
        /// Plays an audio file to a conference participant
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="participantSid">Participant SID.</param>
        /// <param name="audioUrl">A URL to the audio file that will be played.Mutliple AudioUrl parameters may be passed to play more than one file.</param>
        /// <returns>Returns participant</returns>
        public Participant PlayAudioToParticipant(string accountSid, string conferenceSid, string participantSid,
            string audioUrl = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create POST request
            var request = RestRequestHelper.CreateRestRequest(Method.POST,
                $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}/Play.json");

            // Add body parameter
            if (audioUrl.HasValue()) request.AddParameter("AudioUrl", audioUrl);

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Participant>(response);
        }

        /// <summary>
        /// Plays an audio file to a conference participant. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="participantSid">Participant SID.</param>
        /// <param name="audioUrl">A URL to the audio file that will be played.Mutliple AudioUrl parameters may be passed to play more than one file.</param>
        /// <returns>Returns participant</returns>
        public Participant PlayAudioToParticipant(string conferenceSid, string participantSid, string audioUrl = null)
        {
            // Get account sid from configuration
            var accountSid = HttpProvider.GetConfiguration().AccountSid;

            return this.PlayAudioToParticipant(accountSid, conferenceSid, participantSid, audioUrl);
        }

        /// <summary>
        /// Hangs up a conference participant
        /// </summary>
        /// <param name="accountSid">The account sid.</param>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="participantSid">Participant SID.</param>
        /// <returns>Returns participant</returns>
        public Participant HangupParticipant(string accountSid, string conferenceSid, string participantSid = null)
        {
            // Get client to make request
            var client = HttpProvider.GetHttpClient();

            // Create DELETE request
            var request = RestRequestHelper.CreateRestRequest(Method.DELETE,
                $"Accounts/{accountSid}/Conferences/{conferenceSid}/Participants/{participantSid}.json");

            // Send request
            var response = client.Execute(request);

            return this.ReturnOrThrowException<Participant>(response);
        }

        /// <summary>
        /// Hangs up a conference participant. Uses {accountSid} from configuration in HttpProvider
        /// </summary>
        /// <param name="conferenceSid">Conference SID.</param>
        /// <param name="participantSid">Participant SID.</param>
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
        private void SetParamsForListConferences(IRestRequest request, string friendlyName, ConferenceStatus? status,
            DateTime dateCreatedGte, DateTime dateCreatedLt, DateTime dateUpdatedGte, DateTime dateUpdatedLt,
            int? page, int? pageSize)
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