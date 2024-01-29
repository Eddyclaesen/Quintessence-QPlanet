using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi
{
    public class SuperOfficeAccess : WebApiBase, ISuperOfficeAccess
    {
        public void Initialize(string ticketServiceUri, string ticketServiceApiKey, string superOfficeCustomerStateUri, string superOfficeAppToken)
        {
            _ticketServiceUri = ticketServiceUri;
            _ticketServiceApiKey = ticketServiceApiKey;
            _superOfficeCustomerStateUri = superOfficeCustomerStateUri;
            _superOfficeAppToken = superOfficeAppToken;

            RequestAPiAuthenticationTicket().Wait();
            RequestCustomerState().Wait();

            _initialized = _superOfficeTicket != null && !String.IsNullOrEmpty(_superOfficeTicket.Ticket);
        }

        public async Task<bool> TestAccess()
        {
            var result = await DoWebGetRequest("Contact/2");
            Contact contact = JsonConvert.DeserializeObject<Contact>(result);
            return contact != null && contact.Name.Length > 0;
        }

        public async Task<IEnumerable<User>> RetrieveUsers()
        {            
            var rslt = await DoWebGetRequest("User");

            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);
            var results = JsonConvert.DeserializeObject<List<User>>(apiResponse.Value.ToString());
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<Company>> RetrieveCompanies(int pageSize, int pageNumber)
        {
            var relativeUrl = String.Format("Contact?$top={0}&$skip={1}", pageSize, pageSize * pageNumber);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<Company> results;
            results = JsonConvert.DeserializeObject<List<Company>>(apiResponse.Value.ToString());
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<PersonOverview>> RetrievePersons(int pageSize, int pageNumber)
        {
            var relativeUrl = String.Format("Person?$top={0}&$skip={1}", pageSize, pageSize * pageNumber);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<PersonOverview> results;
            results = JsonConvert.DeserializeObject<List<PersonOverview>>(apiResponse.Value.ToString());
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<Project>> RetrieveProjects(int pageSize, int pageNumber)
        {
            var relativeUrl = String.Format("Project?$top={0}&$skip={1}", pageSize, pageSize * pageNumber);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<Project> results;
            results = JsonConvert.DeserializeObject<List<Project>>(apiResponse.Value.ToString());            
            return results;
        }

        public async Task<Project> RetrieveProjectBySuperOfficeId(int superOfficeProjectId)
        {
            var relativeUrl = String.Format("project/{0}", superOfficeProjectId);
            Project project = null;

            try
            {
                var rslt = await DoWebGetRequest(relativeUrl);
                project = JsonConvert.DeserializeObject<Project>(rslt);
                if (project != null)
                {
                    project.HtmlDecodeFields();
                }
            }
            catch {}

            return project;
        }

        public async Task<IEnumerable<ProjectMember>> RetrieveProjectMembers(int superOfficeProjectId)
        {
            var relativeUrl = String.Format("project/{0}/Members", superOfficeProjectId);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<ProjectMember> results;
            results = JsonConvert.DeserializeObject<List<ProjectMember>>(apiResponse.Value.ToString());
            return results;
        }

        public async Task<Company> RetrieveCompanyBySuperOfficeId(int superOfficeId)
        {
            var relativeUrl = String.Format("contact/{0}", superOfficeId);
            Company company = null;

            try
            {
                var rslt = await DoWebGetRequest(relativeUrl);
                company = JsonConvert.DeserializeObject<Company>(rslt);
                if (company != null)
                {
                    company.HtmlDecodeFields();
                }
            }
            catch { }

            return company;
        }

        public async Task<Person> RetrievePersonBySuperOfficeId(int superOfficePersonId)
        {
            var relativeUrl = String.Format("person/{0}", superOfficePersonId);
            Person person = null;

            try
            {
                var rslt = await DoWebGetRequest(relativeUrl);
                person = JsonConvert.DeserializeObject<Person>(rslt);
                if (person != null)
                {
                    person.HtmlDecodeFields();
                }
            }
            catch { }

            return person;
        }

        public async Task<Appointment> RetrieveAppointmentBySuperOfficeId(int superOfficeAppointmentId)
        {
            var relativeUrl = String.Format("appointment/{0}", superOfficeAppointmentId);
            Appointment appointment = null;

            try
            {
                var rslt = await DoWebGetRequest(relativeUrl);
                appointment = JsonConvert.DeserializeObject<Appointment>(rslt);
                if (appointment != null)
                {
                    appointment.HtmlDecodeFields();
                }
            }
            catch { }

            return appointment;
        }

        private async Task RequestAPiAuthenticationTicket()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    string requestUri = String.Format("{0}?apiKey={1}", _ticketServiceUri, _ticketServiceApiKey);
                    response = await client.GetAsync(requestUri);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error calling SuperOffice API.", ex);
                }

                if (response != null && !response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(String.Format("SuperOffice, Unable to retrieve ticket from {0}", _ticketServiceUri));
                }
                var result = await response.Content.ReadAsStringAsync();                
                _superOfficeTicket = JsonConvert.DeserializeObject<TicketResponse>(result);
            }
        }
    }
}
