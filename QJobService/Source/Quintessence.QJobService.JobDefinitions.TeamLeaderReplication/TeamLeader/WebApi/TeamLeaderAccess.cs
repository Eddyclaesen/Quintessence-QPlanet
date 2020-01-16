using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.WebApi
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
                    (customField.ObjectType == Constants.CustomFieldObjectType.Set || customField.ObjectType == Constants.CustomFieldObjectType.Enum))
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

        public async Task<IEnumerable<User>> RetrieveUsers()
        {
            var relativeUrl = "getUsers.php";
            var rslt = await DoWebRequest(relativeUrl, null);

            var results = JsonConvert.DeserializeObject<List<User>>(rslt);
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }
       
        public async Task<IEnumerable<Project>> RetrieveProjects(int pageSize, int pageNumber, IEnumerable<CustomField> customFields)
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
            List<Project> results;
            if(customFields== null)
                results = JsonConvert.DeserializeObject<List<Project>>(rslt);
            else
            {
                results= new List<Project>();
                JArray projects = JArray.Parse(rslt);
                foreach (JObject projectObject in projects)
                {
                    Project project = projectObject.ToObject<Project>();
                    project.FillCustomFields(projectObject, customFields);
                    results.Add(project);
                }                                    
            }
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }
        
        public async Task<Project> RetrieveProjectByTeamLeaderId(int teamLeaderProjectId, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getProject.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"project_id", teamLeaderProjectId.ToString()}
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            Project project = JsonConvert.DeserializeObject<Project>(rslt);
            if (project != null)
            {
                if (customFields != null)
                {
                    JObject projectObject = JObject.Parse(rslt);
                    project.FillCustomFields(projectObject, customFields);
                }
                project.HtmlDecodeFields();
            }
            return project;
        }

        public async Task<IEnumerable<ProjectUser>> RetrieveProjectUsers(int teamLeaderProjectId)
        {
            var relativeUrl = "getUsersOnProject.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"project_id", teamLeaderProjectId.ToString()}
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            var results = JsonConvert.DeserializeObject<List<ProjectUser>>(rslt);
            return results;
        }

        public async Task<IEnumerable<Company>> RetrieveCompanies(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getCompanies.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString(CultureInfo.CurrentCulture)},
                {"pageno", pageNumber.ToString(CultureInfo.CurrentCulture)}
            };
            if(modifiedSince.HasValue)
                additionalPostData.Add("modifiedsince", DateTimeHelper.DateTimeToUnixTimestamp(modifiedSince.Value).ToString(CultureInfo.CurrentCulture));

            if (customFields != null)
            {
                string includeCustomFieldIds = string.Join(",", customFields.Select(pcf => pcf.Id.GetValueOrDefault().ToString(CultureInfo.CurrentCulture)));
                if (!String.IsNullOrEmpty(includeCustomFieldIds))
                    additionalPostData.Add("selected_customfields", includeCustomFieldIds);
            }

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            List<Company> results;
            if (customFields == null)
                results = JsonConvert.DeserializeObject<List<Company>>(rslt);
            else
            {
                results = new List<Company>();
                JArray companies = JArray.Parse(rslt);
                foreach (JObject companyObject in companies)
                {
                    Company company = companyObject.ToObject<Company>();
                    company.FillCustomFields(companyObject, customFields);
                    results.Add(company);
                }
            }
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<Company> RetrieveCompanyByTeamLeaderId(int teamLeaderCompanyId, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getCompany.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"company_id", teamLeaderCompanyId.ToString(CultureInfo.CurrentCulture)}
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            Company company = JsonConvert.DeserializeObject<Company>(rslt);
            if (company != null)
            {
                if (customFields != null)
                {
                    JObject companyObject = JObject.Parse(rslt);
                    company.FillCustomFields(companyObject, customFields);
                }
                company.HtmlDecodeFields();
            }
            return company;
        }

        public async Task<IEnumerable<Contact>> RetrieveContacts(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getContacts.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"amount", pageSize.ToString(CultureInfo.CurrentCulture)},
                {"pageno", pageNumber.ToString(CultureInfo.CurrentCulture)}                 
            };
            if (modifiedSince.HasValue)
                additionalPostData.Add("modifiedsince", DateTimeHelper.DateTimeToUnixTimestamp(modifiedSince.Value).ToString(CultureInfo.CurrentCulture));

            if (customFields != null)
            {
                string includeCustomFieldIds = string.Join(",", customFields.Select(pcf => pcf.Id.GetValueOrDefault().ToString(CultureInfo.CurrentCulture)));
                if (!String.IsNullOrEmpty(includeCustomFieldIds))
                    additionalPostData.Add("selected_customfields", includeCustomFieldIds);
            }
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            List<Contact> results;
            if (customFields == null)
                results = JsonConvert.DeserializeObject<List<Contact>>(rslt);
            else
            {
                results = new List<Contact>();
                JArray contacts = JArray.Parse(rslt);
                foreach (JObject contactObject in contacts)
                {
                    Contact contact = contactObject.ToObject<Contact>();
                    contact.FillCustomFields(contactObject, customFields);
                    results.Add(contact);
                }
            }
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<Contact> RetrieveContactByTeamLeaderId(int teamLeaderContactId, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getContact.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"contact_id", teamLeaderContactId.ToString()}                
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            Contact contact = JsonConvert.DeserializeObject<Contact>(rslt);
            if (contact != null)
            {
                if (customFields != null)
                {
                    JObject projectObject = JObject.Parse(rslt);
                    contact.FillCustomFields(projectObject, customFields);
                }
                contact.HtmlDecodeFields();
            }
            return contact;
        }

        public async Task<IEnumerable<CompanyContact>> RetrieveCompanyRelatedContacts(int teamLeaderCompanyId)
        {
            var relativeUrl = "getContactsByCompany.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"company_id", teamLeaderCompanyId.ToString()}                
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            List<CompanyContact> results = JsonConvert.DeserializeObject<List<CompanyContact>>(rslt);
            if(results != null)
                results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<TeamLeaderTask> RetrieveTaskByTeamLeaderId(int teamLeaderTaskId, IEnumerable<CustomField> customFields)
        {
            var relativeUrl = "getTask.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"task_id", teamLeaderTaskId.ToString()}                
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);
            TeamLeaderTask task = JsonConvert.DeserializeObject<TeamLeaderTask>(rslt);
            if (task != null)
            {
                task.Id = teamLeaderTaskId;

                JObject taskObject = JObject.Parse(rslt);
                if(taskObject["responsible_users"]["users"].HasValues )
                    task.ResponsibleUser = taskObject["responsible_users"]["users"].First().ToObject<int?>();
                if (customFields != null)
                {                    
                    task.FillCustomFields(taskObject, customFields);
                }
                task.HtmlDecodeFields();
            }
            return task;
        }

        public async Task<TimeTracking> RetrieveTimeTrackingByTeamLeaderId(int teamLeaderTimeTrackingId)
        {
            var relativeUrl = "getTimetrackingEntry.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"timetracking_id", teamLeaderTimeTrackingId.ToString()}                
            };
            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            List<TimeTracking> results = JsonConvert.DeserializeObject<List<TimeTracking>>(rslt);

            TimeTracking timeTracking = null;
            if (results != null && results.Count == 1)
            {
                timeTracking = results[0];
                timeTracking.HtmlDecodeFields();
            }
            return timeTracking;  
        }

        public async Task<IEnumerable<PlannedTask>> RetrievePlannedTasksByProjectIdAndUserId(DateTime fromDate, DateTime toDate, int projectId, int? userId)
        {
            var relativeUrl = "getPlannedTasks.php";
            var additionalPostData = new Dictionary<string, string>()
            {
                {"date_from", fromDate.ToString("dd/MM/yyyy")} ,
                {"date_to", toDate.ToString("dd/MM/yyyy")} ,
                {"project_id", projectId.ToString()} 
            };
            if (userId.HasValue)
                additionalPostData.Add("user_id", userId.ToString());

            var rslt = await DoWebRequest(relativeUrl, additionalPostData);

            List<PlannedTask> results = JsonConvert.DeserializeObject<List<PlannedTask>>(rslt);
            return results;
        }
    }
}
