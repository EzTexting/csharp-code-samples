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
        public InboxFolder GetInboxFolder(string inboxFolderId)
        {
            var request = new RestRequest();
            request.Resource = "messages-folders/{inboxFolderId}";

            request.AddParameter("inboxFolderId", inboxFolderId, ParameterType.UrlSegment);

            return Execute<InboxFolderWrapper>(request).Entry;
        }

        public void DeleteInboxFolder(string inboxFolderId)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "messages-folders/{inboxFolderId}";

            request.AddParameter("inboxFolderId", inboxFolderId, ParameterType.UrlSegment);

            Execute<InboxFolderWrapper>(request);
        }


        public void UpdateInboxFolder(InboxFolder inboxFolder)
        {
            Require.Argument("inboxFolder.ID", inboxFolder.ID);

            var request = new RestRequest(Method.POST);
            request.Resource = "messages-folders/{inboxFolderId}";

            request.AddParameter("inboxFolderId", inboxFolder.ID, ParameterType.UrlSegment);
            inboxFolder.PutParams(request);

            Execute<InboxFolderWrapper>(request, true);
        }

        public InboxFolder CreateInboxFolder(InboxFolder inboxFolder)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "messages-folders";

            inboxFolder.PutParams(request);

            return Execute<InboxFolderWrapper>(request).Entry;
        }

        /// <summary>
        /// Get all Folders in your Ez Texting Inbox
        /// </summary>
        public List<InboxFolder> GetInboxFolders()
        {
            var request = new RestRequest();
            request.Resource = "messages-folders";
            return Execute<InboxFoldersWrapper>(request).Entries;
        }

    }
}
