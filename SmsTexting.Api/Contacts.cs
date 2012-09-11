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
        public Contact GetContact(string contactId)
        {
            var request = new RestRequest();
            request.Resource = "contacts/{contactId}";

            request.AddParameter("contactId", contactId, ParameterType.UrlSegment);

            return Execute<ContactWrapper>(request).Entry;
        }

        public void DeleteContact(string contactId)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "contacts/{contactId}";

            request.AddParameter("contactId", contactId, ParameterType.UrlSegment);

            Execute<ContactWrapper>(request);
        }


        public Contact UpdateContact(Contact contact)
        {
            Require.Argument("contact.ID", contact.ID);

            var request = new RestRequest(Method.POST);
            request.Resource = "contacts/{contactId}";

            request.AddParameter("contactId", contact.ID, ParameterType.UrlSegment);
            contact.PutParams(request);

            return Execute<ContactWrapper>(request).Entry;
        }

        public Contact CreateContact(Contact contact)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "contacts";

            contact.PutParams(request);

            return Execute<ContactWrapper>(request).Entry;
        }

        /// <summary>
        /// Get a list of contacts stored in your Ez Texting contact list.
        /// </summary>
        /// <param name="query">(Optional) Search contacts by first name / last name / phone number</param>
        /// <param name="source">(Optional) Source of contacts. Available values: 'Unknown', 'Manually Added', 'Upload', 'Web Widget', 'API', 'Keyword'</param>
        /// <param name="optout">(Optional) Opted out / opted in contacts. Available values: true, false.</param>
        /// <param name="group">(Optional) Name of the group the contacts belong to</param>
        /// <param name="sortBy">(Optional) Property to sort by. Available values: PhoneNumber, FirstName, LastName, CreatedAt</param>
        /// <param name="sortDir">(Optional) Direction of sorting. Available values: asc, desc</param>
        /// <param name="itemsPerPage">(Optional) Number of results to retrieve. By default, 10 most recently added contacts are retrieved.</param>
        /// <param name="page">(Optional) Page of results to retrieve</param>
        /// <returns></returns>
        public List<Contact> GetContacts(string query, string source, string optout, string group, string sortBy, string sortDir, string itemsPerPage, string page)
        {
            var request = new RestRequest();
            request.Resource = "contacts";

            request.AddParameterIfHasValue("query", query);
            request.AddParameterIfHasValue("source", source);
            request.AddParameterIfHasValue("optout", optout);
            request.AddParameterIfHasValue("group", group);
            request.AddParameterIfHasValue("sortBy", sortBy);
            request.AddParameterIfHasValue("sortDir", sortDir);
            request.AddParameterIfHasValue("itemsPerPage", itemsPerPage);
            request.AddParameterIfHasValue("page", page);

            return Execute<ContactsWrapper>(request).Entries;
        }

    }
}
