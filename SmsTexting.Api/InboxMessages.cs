using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;
using SmsTexting.Extensions;
using SmsTexting.ModelWrappers;


namespace SmsTexting
{
    public partial class SmsTextingRestClient
    {
        /// <summary>
        /// Get a single incoming text messages in your Ez Texting Inbox
        /// </summary>
        /// <param name="inboxMessageId"></param>
        /// <returns></returns>
        public InboxMessage GetInboxMessage(string inboxMessageId)
        {
            var request = new RestRequest();
            request.Resource = "incoming-messages/{inboxMessageId}";

            request.AddParameter("inboxMessageId", inboxMessageId, ParameterType.UrlSegment);

            return Execute<InboxMessageWrapper>(request).Entry;
        }

        /// <summary>
        /// Delete an incoming text message in your Ez Texting Inbox
        /// </summary>
        /// <param name="inboxMessageId"></param>
        public void DeleteInboxMessage(string inboxMessageId)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "incoming-messages/{inboxMessageId}";

            request.AddParameter("inboxMessageId", inboxMessageId, ParameterType.UrlSegment);

            Execute<InboxMessageWrapper>(request);
        }

        /// <summary>
        /// Moves an incoming text message in your Ez Texting Inbox to a specified folder.
        /// </summary>
        /// <param name="inboxMessageId"></param>
        /// <param name="folderId"></param>
        public void MoveInboxMessage(string inboxMessageId, string folderId)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "incoming-messages/";

            request.AddParameter("_method", "move-to-folder");
            request.AddParameter("ID", inboxMessageId);
            request.AddParameter("FolderID", folderId);

            Execute<InboxMessageWrapper>(request, true);
        }

        /// <summary>
        /// Moves an incoming text messages in your Ez Texting Inbox to a specified folder.
        /// </summary>
        /// <param name="inboxMessageIds"></param>
        /// <param name="folderId"></param>
        public void MoveInboxMessages(List<string> inboxMessageIds, string folderId)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "incoming-messages/";

            request.AddParameter("_method", "move-to-folder");
            inboxMessageIds.ForEach( id => request.AddParameter("ID[]", id) );
            request.AddParameter("FolderID", folderId);

            Execute<InboxMessageWrapper>(request, true);
        }

        /// <summary>
        /// Get all incoming text messages in your Ez Texting Inbox
        /// </summary>
        /// <param name="folderId">(Optional) Get messages from the selected folder. If FolderID is not given then request will return messages in your Inbox and all folders.</param>
        /// <param name="search">(Optional) Get messages which contain selected text or which are sent from selected phone number.</param>
        /// <param name="sortBy">(Optional) Property to sort by. Available values: ReceivedOn, PhoneNumber, Message</param>
        /// <param name="sortDir">(Optional) Direction of sorting. Available values: asc, desc</param>
        /// <param name="itemsPerPage">(Optional) Number of results to retrieve. By default, 10 most recent incoming messages are retrieved</param>
        /// <param name="page">(Optional) Page of results to retrieve. 1st page is returned by default</param>
        /// <returns></returns>
        public List<InboxMessage> GetInboxMessages(string folderId, string search, string sortBy, string sortDir, string itemsPerPage, string page)
        {
            var request = new RestRequest();
            request.Resource = "incoming-messages";

            request.AddParameterIfHasValue("FolderID", folderId);
            request.AddParameterIfHasValue("Search", search);
            request.AddParameterIfHasValue("sortBy", sortBy);
            request.AddParameterIfHasValue("sortDir", sortDir);
            request.AddParameterIfHasValue("itemsPerPage", itemsPerPage);
            request.AddParameterIfHasValue("page", page);

            return Execute<InboxMessagesWrapper>(request).Entries;
        }

    }
}
