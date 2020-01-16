﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi
{
    public class WebApiBase
    {
        protected static string _ticketServiceUri = String.Empty;
        protected static string _ticketServiceApiKey = String.Empty;
        protected static string _superOfficeBaseUri = String.Empty;
        protected static string _superOfficeAppToken = String.Empty;

        protected static TicketResponse _superOfficeTicket = null;        
        protected static bool _initialized;

        private class WebResult
        {
            public string Status { get; set; }
            public string Reason { get; set; }
        }

        protected async Task<string> DoWebPostRequest(string relUrl, Dictionary<string, string> additionalPostData)
        {
            if (!_initialized)
                throw new Exception("SuperOffice Api not correctly initialized, call Initialize first with correct settings");

            using (var client = new HttpClient())
            {
                AddRequestHeaders(client);

                var postData = BuildPostData(additionalPostData);
                var content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(_superOfficeBaseUri + relUrl, content);
                }
                catch (Exception ex)
                {                    
                    throw new Exception("Error calling SuperOffice API.", ex);
                }

                if (response != null && !response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(BuildExceptionMessage(relUrl, responseContent, additionalPostData));
                }
                return await response.Content.ReadAsStringAsync();
            }
        }

        protected async Task<string> DoWebGetRequest(string relUrl)
        {
            if (!_initialized)
                throw new Exception("SuperOffice Api not correctly initialized, call Initialize first with correct settings");

            using (var client = new HttpClient())
            {
                AddRequestHeaders(client);

                HttpResponseMessage response = null;
                try
                {
                    response = await client.GetAsync(_superOfficeBaseUri + relUrl);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error calling SuperOffice API.", ex);
                }

                if (response != null && !response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(BuildExceptionMessage(relUrl, responseContent));
                }
                return await response.Content.ReadAsStringAsync();
            }
        }

        private static Dictionary<string, string> BuildPostData(Dictionary<string, string> additionalPostData)
        {
            Dictionary < string, string> postData = new Dictionary<string, string>();
            if (additionalPostData != null && additionalPostData.Any())
            {
                foreach (var additional in additionalPostData)
                    postData.Add(additional.Key, additional.Value);
            }
            return postData;
        }

        private static string BuildExceptionMessage(string url, string content, Dictionary<string, string> additionalPostData = null)
        {
            string messageBody = "SuperOffice API: {0} : Bad request: {1} PostData: {2}";
            string reason = "Unknown error";
            string postDataInfo = "None";

            try
            {
                if (additionalPostData != null && additionalPostData.Any())
                {
                    StringBuilder postData = new StringBuilder("{");
                    foreach (var additional in additionalPostData)
                        postData.Append(String.Format("{0}: {1} ;", additional.Key, additional.Value));
                    postData.Append("}");
                    postDataInfo = postData.ToString();
                }

                WebResult webResult = JsonConvert.DeserializeObject<WebResult>(content);
                if (webResult != null && !String.IsNullOrEmpty(webResult.Reason))
                    reason = webResult.Reason;
            }
            catch { }

            return String.Format(messageBody, url, reason, postDataInfo);
        }

        private void AddRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), "SOTICKET " + _superOfficeTicket.Ticket);
            client.DefaultRequestHeaders.Add("SO-AppToken", _superOfficeAppToken);
            client.DefaultRequestHeaders.Add(HttpRequestHeader.Accept.ToString(), "application/json");

            client.DefaultRequestHeaders.Add(HttpRequestHeader.AcceptLanguage.ToString(), "nl");
        }
    }
}
