using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;


namespace Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.WebApi
{
    public class WebApiBase
    {
        protected static string _group = String.Empty;
        protected static string _key = String.Empty;
        protected static string _baseUri = String.Empty;
        protected static bool _initialized;

        private class WebResult
        {
            public string Status { get; set; }
            public string Reason { get; set; }
        }


        protected async Task<string> DoWebRequest(string relUrl, Dictionary<string, string> additionalPostData)
        {
            if (!_initialized)
                throw new Exception("TeamLeader Api not correctly initialized, call Initialize first with correct credentials");

            int retryCount = 3;
            while (retryCount >= 0)
            {
                retryCount--;
                try
                {
                    using (var client = new HttpClient())
                    {
                        var postData = BuildPostData(additionalPostData);
                        var content = new FormUrlEncodedContent(postData);

                        HttpResponseMessage response = await client.PostAsync(_baseUri + relUrl, content);
                        if (response != null && !response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            throw new Exception(BuildExceptionMessage(relUrl, responseContent, additionalPostData));
                        }
                        return await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    if(retryCount == 0 )
                        throw new Exception("Error calling TeamLeader API.", ex);
                    Thread.Sleep(1000);
                }             
            }
            throw new Exception("Error calling TeamLeader API.");
        }

        private static Dictionary<string, string> BuildPostData(Dictionary<string, string> additionalPostData)
        {
            Dictionary < string, string> postData = new Dictionary<string, string>() {{"api_group", _group}, {"api_secret", _key}};
            if (additionalPostData != null && additionalPostData.Any())
            {
                foreach (var additional in additionalPostData)
                    postData.Add(additional.Key, additional.Value);
            }
            return postData;
        }

        private static string BuildExceptionMessage(string url, string content, Dictionary<string, string> additionalPostData)
        {
            string messageBody = "TeamLeader API: {0} : Bad request: {1} PostData: {2}";
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
    }
}
