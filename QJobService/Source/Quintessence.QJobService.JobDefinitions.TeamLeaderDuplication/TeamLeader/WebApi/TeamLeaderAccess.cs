using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.WebApi;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.WebApi
{
    public class TeamLeaderAccess : WebApiBase, ITeamLeaderAccess
    {
        public static void Initialize(string group, string key, string baseUri)
        {
            _group = group;
            _key = key;
            _baseUri = baseUri;
            if (!_baseUri.EndsWith("/"))
                _baseUri = _baseUri + "/";
            _initialized = true;
        }

        public async Task<bool> TestAccess()
        {            
            var result = await DoWebRequest("helloWorld.php", null);
            return result.Contains("Successful");
        }
        
        public async Task<IEnumerable<CustomField>> RetrieveCustomFieldsByTargetObject(string customFieldTargetObject)
        {
            var relativeUrl = "getCustomFields.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"for", customFieldTargetObject}
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            var results = JsonConvert.DeserializeObject<List<CustomField>>(rslt);

            foreach (var customField in results)
            {
                if ( customField.Id.HasValue &&
                    (customField.ObjectType == TLC.Constants.CustomFieldObjectType.Set || customField.ObjectType == TLC.Constants.CustomFieldObjectType.Enum))
                {
                    CustomField customFieldWithOptions = await RetrieveCustomFieldById(customField.Id.Value);
                    if(customFieldWithOptions != null)
                        customField.Options = customFieldWithOptions.Options;
                }                    
            }
            return results;
        }

        public async Task<CustomField> RetrieveCustomFieldById(int customFieldId)
        {
            var relativeUrl = "getCustomFieldInfo.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"custom_field_id", customFieldId.ToString()}
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            var result = JsonConvert.DeserializeObject<CustomField>(rslt);
            return result;
        }

        public async Task<IEnumerable<TeamLeaderUser>> RetrieveUsers()
        {
            var relativeUrl = "getUsers.php";
            var rslt = await DoWebRequest(relativeUrl, null);

            var results = JsonConvert.DeserializeObject<List<TeamLeaderUser>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<TeamLeaderContact>> RetrieveContacts(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getContacts.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString(CultureInfo.CurrentCulture)},
                {"pageno", pageNumber.ToString(CultureInfo.CurrentCulture)}                 
            };
            if (modifiedSince.HasValue)
                additionalPostData.Add("modifiedsince", TLC.DateTimeHelper.DateTimeToUnixTimestamp(modifiedSince.Value).ToString(CultureInfo.CurrentCulture));

            if (customFields != null)
            {
                string includeCustomFieldIds = string.Join(",", customFields.Select(pcf => pcf.Id.GetValueOrDefault().ToString(CultureInfo.CurrentCulture)));
                if (!String.IsNullOrEmpty(includeCustomFieldIds))
                    additionalPostData.Add("selected_customfields", includeCustomFieldIds);
            }
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            List<TeamLeaderContact> results;
            if (customFields == null)
                results = JsonConvert.DeserializeObject<List<TeamLeaderContact>>(rslt);
            else
            {
                results = new List<TeamLeaderContact>();
                JArray contacts = JArray.Parse(rslt);
                foreach (JObject contactObject in contacts)
                {
                    TeamLeaderContact contact = contactObject.ToObject<TeamLeaderContact>();
                    contact.FillCustomFields(contactObject, customFields);
                    results.Add(contact);
                }
            }
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<TeamLeaderCompany>> RetrieveCompanies(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getCompanies.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString(CultureInfo.CurrentCulture)},
                {"pageno", pageNumber.ToString(CultureInfo.CurrentCulture)}
            };
            if (modifiedSince.HasValue)
                additionalPostData.Add("modifiedsince", TLC.DateTimeHelper.DateTimeToUnixTimestamp(modifiedSince.Value).ToString(CultureInfo.CurrentCulture));

            if (customFields != null)
            {
                string includeCustomFieldIds = string.Join(",", customFields.Select(pcf => pcf.Id.GetValueOrDefault().ToString(CultureInfo.CurrentCulture)));
                if (!String.IsNullOrEmpty(includeCustomFieldIds))
                    additionalPostData.Add("selected_customfields", includeCustomFieldIds);
            }

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderCompany> results;
            if (customFields == null)
                results = JsonConvert.DeserializeObject<List<TeamLeaderCompany>>(rslt);
            else
            {
                results = new List<TeamLeaderCompany>();
                JArray companies = JArray.Parse(rslt);
                foreach (JObject companyObject in companies)
                {
                    TeamLeaderCompany company = companyObject.ToObject<TeamLeaderCompany>();
                    company.FillCustomFields(companyObject, customFields);
                    results.Add(company);
                }
            }
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<TeamLeaderProject>> RetrieveProjects(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getProjects.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString()},
                {"pageno", pageNumber.ToString()}
            };

            if (customFields != null)
            {
                string includeCustomFieldIds = string.Join(",", customFields.Select(pcf => pcf.Id.GetValueOrDefault().ToString()));
                if (!String.IsNullOrEmpty(includeCustomFieldIds))
                    additionalPostData.Add("selected_customfields", includeCustomFieldIds);
            }

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderProject> results;
            if (customFields == null)
                results = JsonConvert.DeserializeObject<List<TeamLeaderProject>>(rslt);
            else
            {
                results = new List<TeamLeaderProject>();
                JArray projects = JArray.Parse(rslt);
                foreach (JObject projectObject in projects)
                {
                    TeamLeaderProject project = projectObject.ToObject<TeamLeaderProject>();
                    project.FillCustomFields(projectObject, customFields);
                    results.Add(project);
                }
            }
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<TeamLeaderTask>> RetrieveTasks(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getTasks.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString()},
                {"pageno", pageNumber.ToString()}
            };

            if (customFields != null)
            {
                string includeCustomFieldIds = string.Join(",", customFields.Select(pcf => pcf.Id.GetValueOrDefault().ToString()));
                if (!String.IsNullOrEmpty(includeCustomFieldIds))
                    additionalPostData.Add("selected_customfields", includeCustomFieldIds);
            }

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderTask> results;
            if (customFields == null)
                results = JsonConvert.DeserializeObject<List<TeamLeaderTask>>(rslt);
            else
            {
                results = new List<TeamLeaderTask>();
                JArray projects = JArray.Parse(rslt);
                foreach (JObject projectObject in projects)
                {
                    TeamLeaderTask task = projectObject.ToObject<TeamLeaderTask>();
                    task.FillCustomFields(projectObject, customFields);
                    results.Add(task);
                }
            }
            results.ForEach(r => r.HtmlDecodeFields());
            return results;       
        }

        public async Task<IEnumerable<TeamLeaderTimeTracking>> RetrieveTimeTrackings(DateTime fromDate, DateTime toDate)
        {
            var relativeUrl = "getTimetracking.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"date_from", fromDate.ToString("dd/MM/yyyy")},
                {"date_to", toDate.ToString("dd/MM/yyyy")}
            };

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderTimeTracking> results = JsonConvert.DeserializeObject<List<TeamLeaderTimeTracking>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;  
        }

        public async Task<IEnumerable<TeamLeaderDeal>> RetrieveDeals(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getDeals.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString()},
                {"pageno", pageNumber.ToString()}
            };

            if (customFields != null)
            {
                string includeCustomFieldIds = string.Join(",", customFields.Select(pcf => pcf.Id.GetValueOrDefault().ToString()));
                if (!String.IsNullOrEmpty(includeCustomFieldIds))
                    additionalPostData.Add("selected_customfields", includeCustomFieldIds);
            }

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderDeal> results;
            if (customFields == null)
                results = JsonConvert.DeserializeObject<List<TeamLeaderDeal>>(rslt);
            else
            {
                results = new List<TeamLeaderDeal>();
                JArray deals = JArray.Parse(rslt);
                foreach (JObject dealObject in deals)
                {
                    TeamLeaderDeal deal = dealObject.ToObject<TeamLeaderDeal>();
                    deal.FillCustomFields(dealObject, customFields);
                    results.Add(deal);
                }
            }
            results.ForEach(r => r.HtmlDecodeFields());
            return results;            
        }

        public async Task<IEnumerable<TeamLeaderCall>> RetrieveCalls(int pageSize, int pageNumber, DateTime? fromDate, DateTime? toDate)
        {
            var relativeUrl = "getCalls.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString()},
                {"pageno", pageNumber.ToString()}
            };

            if (fromDate.HasValue && toDate.HasValue)
            {
                additionalPostData.Add("date_from", fromDate.Value.ToString("dd/MM/yyyy"));
                additionalPostData.Add("date_to", toDate.Value.ToString("dd/MM/yyyy"));  
            }

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderCall> results = JsonConvert.DeserializeObject<List<TeamLeaderCall>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<TeamLeaderMeeting>> RetrieveMeetings(int pageSize, int pageNumber, DateTime? fromDate, DateTime? toDate)
        {
            var relativeUrl = "getMeetings.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString()},
                {"pageno", pageNumber.ToString()}
            };

            if (fromDate.HasValue && toDate.HasValue)
            {
                additionalPostData.Add("date_from", fromDate.Value.ToString("dd/MM/yyyy"));
                additionalPostData.Add("date_to", toDate.Value.ToString("dd/MM/yyyy"));
            }

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderMeeting> results = JsonConvert.DeserializeObject<List<TeamLeaderMeeting>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<TeamLeaderMeetingDetail> RetrieveMeetingDetailByMeetingId(int meetingTeamLeaderId, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getMeeting.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"meeting_id", meetingTeamLeaderId.ToString()}
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            TeamLeaderMeetingDetail meetingDetail = JsonConvert.DeserializeObject<TeamLeaderMeetingDetail>(rslt);
            if (meetingDetail != null)
            {
                JObject meetingDetailObject = JObject.Parse(rslt);
                if (customFields != null)
                {                   
                    meetingDetail.FillCustomFields(meetingDetailObject, customFields);
                }

                meetingDetail.FillAttendants(meetingDetailObject);
                meetingDetail.HtmlDecodeFields();
            }
            return meetingDetail;
        }

        public async Task<TeamLeaderCompanyDetail> RetrieveCompanyDetailByCompanyId(int companyTeamLeaderId)
        {
            var relativeUrl = "getCompany.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"company_id", companyTeamLeaderId.ToString()}
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            TeamLeaderCompanyDetail companyDetail = JsonConvert.DeserializeObject<TeamLeaderCompanyDetail>(rslt);
            if (companyDetail != null)
            {
                companyDetail.HtmlDecodeFields();
            }
            return companyDetail;         
        }

        public async Task<TeamLeaderTask> RetrieveTaskByTaskId(int taskTeamLeaderId, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getTask.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"task_id", taskTeamLeaderId.ToString()}
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            TeamLeaderTask teamLeaderTask = JsonConvert.DeserializeObject<TeamLeaderTask>(rslt);
            if (teamLeaderTask != null)
            {
                teamLeaderTask.Id = taskTeamLeaderId;
                JObject teamLeaderTaskObject = JObject.Parse(rslt);
                if (customFields != null)
                {
                    teamLeaderTask.FillCustomFields(teamLeaderTaskObject, customFields);
                }

                teamLeaderTask.HtmlDecodeFields();
            }
            return teamLeaderTask; 
        }

        public async Task<TeamLeaderTaskDetail> RetrieveTaskDetailByTaskId(int taskTeamLeaderId)
        {
            var relativeUrl = "getTask.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"task_id", taskTeamLeaderId.ToString()}
            };

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            TeamLeaderTaskDetail taskDetail = JsonConvert.DeserializeObject<TeamLeaderTaskDetail>(rslt);
            if (taskDetail != null)
            {
                JObject taskDetailObject = JObject.Parse(rslt);
                taskDetail.FillResponsibleUser(taskDetailObject);

                taskDetail.HtmlDecodeFields();
            }
            return taskDetail;
        }

        public async Task<TeamLeaderCallDetail> RetrieveCallDetailByCallId(int callTeamLeaderId, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getCall.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"call_id", callTeamLeaderId.ToString()}
            };

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            TeamLeaderCallDetail callDetail = JsonConvert.DeserializeObject<TeamLeaderCallDetail>(rslt);
            if (callDetail != null)
            {
                JObject callDetailObject = JObject.Parse(rslt);
                if (customFields != null)
                {                   
                    callDetail.FillCustomFields(callDetailObject, customFields);
                }

                callDetail.HtmlDecodeFields();
            }
            return callDetail;
        }

        public async Task<IEnumerable<TeamLeaderContactCompanyRelation>> RetrieveContactCompanyRelation(int pageSize, int pageNumber)
        {
            var relativeUrl = "getContactCompanyRelations.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString()},
                {"pageno", pageNumber.ToString()}
            };

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderContactCompanyRelation> results = JsonConvert.DeserializeObject<List<TeamLeaderContactCompanyRelation>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;    
        }

        public async Task<IEnumerable<TeamLeaderPlannedTask>> RetrievePlannedTasksByUserTeamLeaderId(int userTeamLeaderId, DateTime fromDate, DateTime toDate)
        {
            var relativeUrl = "getPlannedTasks.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"date_from", fromDate.ToString("dd/MM/yyyy")},
                {"date_to", toDate.ToString("dd/MM/yyyy")},
                {"user_id", userTeamLeaderId.ToString()}
            };

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderPlannedTask> results = JsonConvert.DeserializeObject<List<TeamLeaderPlannedTask>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;   
        }

        public async Task<IEnumerable<TeamLeaderContactProjectRelation>> RetrieveContactProjectRelationsByProjectTeamLeaderId(int projectTeamLeaderId)
        {
            var relativeUrl = "getRelatedPartiesByProject.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"project_id", projectTeamLeaderId.ToString()}
            };

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderContactProjectRelation> results = JsonConvert.DeserializeObject<List<TeamLeaderContactProjectRelation>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;              
        }

        public async Task<IEnumerable<TeamLeaderProduct>> RetrieveProducts(int pageSize, int pageNumber)
        {
            var relativeUrl = "getProducts.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString()},
                {"pageno", pageNumber.ToString()}
            };

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<TeamLeaderProduct> results = JsonConvert.DeserializeObject<List<TeamLeaderProduct>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<TeamLeaderDealPhase>> RetrieveDealPhases()
        {
            var relativeUrl = "getDealPhases.php";

            var rslt = await DoWebRequest(relativeUrl, null);
            List<TeamLeaderDealPhase> results = JsonConvert.DeserializeObject<List<TeamLeaderDealPhase>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<TeamLeaderDealSource>> RetrieveDealSources()
        {
            var relativeUrl = "getDealSources.php";

            var rslt = await DoWebRequest(relativeUrl, null);
            List<TeamLeaderDealSource> results = JsonConvert.DeserializeObject<List<TeamLeaderDealSource>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }
    }
}
