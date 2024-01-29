using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi
{
    public class WebApiBase
    {
        protected static string _ticketServiceUri = String.Empty;
        protected static string _ticketServiceApiKey = String.Empty;
        protected static string _superOfficeCustomerStateUri = String.Empty;
        protected static string _superOfficeAppToken = String.Empty;

        protected static TicketResponse _superOfficeTicket = null;
        protected static CustomerState _customerState = null;
        protected static bool _initialized;

        private class WebResult
        {
            public string Status { get; set; }
            public string Reason { get; set; }
        }

        protected async Task<string> DoWebPostRequest(string relUrl, IEnumerable<int> postData)
        {
            if (!_initialized)
                throw new Exception("SuperOffice Api not correctly initialized, call Initialize first with correct settings");

            EnsureCustomerStateIsValid();

            using (var client = new HttpClient())
            {
                AddRequestHeaders(client);

                var serializedPostData = JsonConvert.SerializeObject(postData);
                var content = new StringContent(serializedPostData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(_customerState.SuperOfficeBaseUri + relUrl, content);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error calling SuperOffice API.", ex);
                }

                if (response != null && !response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(BuildExceptionMessage(relUrl, responseContent, content.ToString()));
                }
                return await response.Content.ReadAsStringAsync();
            }
        }

        protected async Task<string> DoWebPostRequest(string relUrl, object postData)
        {
            if (!_initialized)
                throw new Exception("SuperOffice Api not correctly initialized, call Initialize first with correct credentials");
            
            EnsureCustomerStateIsValid();

            using (var client = new HttpClient())
            {
                AddRequestHeaders(client);

                var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(_customerState.SuperOfficeBaseUri + relUrl, content);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error calling SuperOffice API.", ex);
                }

                if (response != null && !response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(BuildExceptionMessage(relUrl, responseContent, content.ToString()));
                }
                return await response.Content.ReadAsStringAsync();
            }
        }

        protected async Task<string> DoWebGetRequest(string relUrl)
        {
            if (!_initialized)
                throw new Exception("SuperOffice Api not correctly initialized, call Initialize first with correct settings");

            EnsureCustomerStateIsValid();

            using (var client = new HttpClient())
            {
                AddRequestHeaders(client);

                HttpResponseMessage response = null;
                try
                {
                    response = await client.GetAsync(_customerState.SuperOfficeBaseUri + relUrl);
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

        protected async Task RequestCustomerState()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    response = await client.GetAsync(_superOfficeCustomerStateUri);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error calling SuperOffice API.", ex);
                }

                if (response != null && !response.IsSuccessStatusCode)
                {
                    throw new Exception(String.Format("SuperOffice, Unable to retrieve Customer State info from Api Url {0}", _superOfficeCustomerStateUri));
                }
                var rslt = await response.Content.ReadAsStringAsync();
                _customerState = JsonConvert.DeserializeObject<CustomerState>(rslt);
            }
        }

        private async Task EnsureCustomerStateIsValid()
        {
            if (_customerState == null || !_customerState.IsValid())
                await RequestCustomerState();
            if (!_customerState.IsValid())
                throw new Exception("Unable to retrieve valid Customer state and the loadbalanced Api endpoint.");
        }

        private static string BuildExceptionMessage(string url, string content, string additionalPostData = null)
        {
            string messageBody = "SuperOffice API: {0} : Bad request: {1} PostData: {2}";
            string reason = "Unknown error";
            string postDataInfo = "None";

            try
            {
                if (additionalPostData != null )
                {
                    postDataInfo = additionalPostData;
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

            client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");
        }
    }
}
