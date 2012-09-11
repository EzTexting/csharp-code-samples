using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Extensions;


namespace SmsTexting
{
    public partial class SmsTextingRestClient
    {
        public const string JSON = "json";
        public const string XML = "xml";

        private RestClient _client;
        private string _encoding;

		/// <summary>
		/// Initializes a new client with the specified credentials.
		/// </summary>
		/// <param name="userName">The Username to authenticate with</param>
		/// <param name="password">The password to authenticate with</param>
        /// <param name="encoding">use SmsTextingRestClient.JSON or SmsTextingRestClient.XML</param>
        public SmsTextingRestClient(string userName, string password, string encoding, string baseUrl)
		{
            _encoding = encoding;
			_client = new RestClient();
            _client.BaseUrl = baseUrl;

            _client.AddDefaultParameter("User", userName);
            _client.AddDefaultParameter("Password", password);
            _client.AddDefaultParameter("format", encoding);
        }

        /// <summary>
		/// Initializes a new client with the specified credentials.
		/// </summary>
		/// <param name="userName">The Username to authenticate with</param>
		/// <param name="password">The password to authenticate with</param>
        /// <param name="encoding">use SmsTextingRestClient.JSON or SmsTextingRestClient.XML</param>
        public SmsTextingRestClient(string userName, string password, string encoding)
           : this(userName, password, encoding, "https://app.eztexting.com") {  }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public T Execute<T>(RestRequest request) where T : BaseObject, new()
        {
//            request.DateFormat = "ddd, dd MMM yyyy HH:mm:ss '+0000'";
            if (JSON.Equals(_encoding))
            {
                request.RootElement = "Response";
            }

            var response = _client.Execute<T>(request);

            if (response.ResponseStatus != ResponseStatus.Completed && response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new Exception(response.ErrorMessage);
            }
            if (response.Data != null && response.Data.Code >= 300)
            {
                throw SmsTextingException.Build(response.Data);
            }
            return response.Data;
        }

    }



}
