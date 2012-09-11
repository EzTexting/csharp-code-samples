using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;
using SmsTexting.Extensions;

namespace SmsTexting
{
    public partial class SmsTextingRestClient
    {
        public Group GetGroup(string groupId)
        {
            var request = new RestRequest();
            request.Resource = "groups/{groupId}";

            request.AddParameter("groupId", groupId, ParameterType.UrlSegment);

            return Execute<GroupWrapper>(request).Entry;
        }

        public void DeleteGroup(string groupId)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "groups/{groupId}";

            request.AddParameter("groupId", groupId, ParameterType.UrlSegment);

            Execute<GroupWrapper>(request);
        }


        public Group UpdateGroup(Group group)
        {
            Require.Argument("group.ID", group.ID);

            var request = new RestRequest(Method.POST);
            request.Resource = "groups/{groupId}";

            request.AddParameter("groupId", group.ID, ParameterType.UrlSegment);
            group.PutParams(request);

            return Execute<GroupWrapper>(request).Entry;
        }

        public Group CreateGroup(Group group)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "groups";

            group.PutParams(request);

            return Execute<GroupWrapper>(request).Entry;
        }

        /// <summary>
        /// Get a list of groups stored in your Ez Texting account.
        /// </summary>
        /// <param name="sortBy">(Optional) Property to sort by. Available values: Name</param>
        /// <param name="sortDir">(Optional) Direction of sorting. Available values: asc, desc</param>
        /// <param name="itemsPerPage">(Optional) Number of results to retrieve. By default, first 10 groups sorted in alphabetical order are retrieved.</param>
        /// <param name="page">(Optional) Page of results to retrieve</param>
        /// <returns></returns>
        public List<Group> GetGroups(string sortBy, string sortDir, string itemsPerPage, string page)
        {
            var request = new RestRequest();
            request.Resource = "groups";

            request.AddParameterIfHasValue("sortBy", sortBy);
            request.AddParameterIfHasValue("sortDir", sortDir);
            request.AddParameterIfHasValue("itemsPerPage", itemsPerPage);
            request.AddParameterIfHasValue("page", page);

            return Execute<GroupsWrapper>(request).Entries;
        }

    }
}
