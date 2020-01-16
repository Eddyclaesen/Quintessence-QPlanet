using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models;
using System.Globalization;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi
{
    public class SuperOfficeAccess : WebApiBase, ISuperOfficeAccess
    {
        public void Initialize(string ticketServiceUri, string ticketServiceApiKey, string superOfficeBaseUri, string superOfficeAppToken)
        {
            _ticketServiceUri = ticketServiceUri;
            _ticketServiceApiKey = ticketServiceApiKey;
            _superOfficeBaseUri = superOfficeBaseUri;
            if (!_superOfficeBaseUri.EndsWith("/"))
                _superOfficeBaseUri = _superOfficeBaseUri + "/";

            _superOfficeAppToken = superOfficeAppToken;

            RequestAPiAuthenticationTicket().Wait();

            _initialized = _superOfficeTicket != null && !String.IsNullOrEmpty(_superOfficeTicket.Ticket);
        }

        public async Task<bool> TestAccess()
        {
            var result = await DoWebGetRequest("Contact/2");
            SuperOfficeTest contact = JsonConvert.DeserializeObject<SuperOfficeTest>(result);
            return contact != null && contact.Name.Length > 0;
        }

        public async Task<IEnumerable<SuperOfficeUser>> RetrieveUsers()
        {
            var rslt = await DoWebGetRequest("User?$select=personId,contactId,firstName,lastName,userName,title,isActive,userGroup,otherGroups");            

            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);
            var results = JsonConvert.DeserializeObject<List<SuperOfficeUser>>(apiResponse.Value.ToString());
            results.ForEach(r => r.HtmlDecodeFields());
            return results;
        }

        public async Task<IEnumerable<SuperOfficeProject>> RetrieveProjects(int pageSize, int pageNumber)
        {
            var relativeUrl = String.Format("Project?$select=projectId,name,projectAssociate/fullName,projectAssociate/personId,type,Status,registeredDate,endDate,projectUdef/SuperOffice:1,nextMilestone,completed,updatedDate,updatedBy&$top={0}&$skip={1}", pageSize, pageSize * pageNumber);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<SuperOfficeProject> results = JsonConvert.DeserializeObject<List<SuperOfficeProject>>(apiResponse.Value.ToString());
            return results;
        }

        public async Task<IEnumerable<SuperOfficePerson>> RetrievePersons(int pageSize, int pageNumber)
        {
            var relativeUrl = String.Format("Person?$select=personId,contactId,firstName,lastName,mrMrs,title,email/emailAddress,phone/formattedNumber,retired,personUdef/SuperOffice:1,personUdef/SuperOffice:2,personUdef/SuperOffice:3&$top={0}&$skip={1}", pageSize, pageSize * pageNumber);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<SuperOfficePerson> results = JsonConvert.DeserializeObject<List<SuperOfficePerson>>(apiResponse.Value.ToString());
            return results;
        }

        public async Task<IEnumerable<SuperOfficeContact>> RetrieveContacts(int pageSize, int pageNumber)
        {
            var relativeUrl = String.Format("Contact?$select=contactId,name,streetAddress/line1,postAddress/line1,postAddress/zip,postAddress/city,country,contactPhone/formattedNumber,contactFax/formattedNumber,url/URLAddress,email/emailAddress,contactAssociate/fullName,contactAssociate/personId,category,business,number,orgnr,UpdatedDate,updatedBy,contactUdef/SuperOffice:2,contactUdef/SuperOffice:3,contactUdef/SuperOffice:4&$top={0}&$skip={1}", pageSize, pageSize * pageNumber);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<SuperOfficeContact> results = JsonConvert.DeserializeObject<List<SuperOfficeContact>>(apiResponse.Value.ToString());
            return results;
        }

        public async Task<IEnumerable<SuperOfficeAppointment>> RetrieveAppointments(int pageSize, int pageNumber)
        {
            var relativeUrl = String.Format("Appointment?$select=appointmentId,type,associateId,associate/fullName,associate/personId,contactId,personId,projectId,saleId,date,endTime,location,description,completed,text,appointmentUdef/SuperOffice:1,appointmentUdef/SuperOffice:2,appointmentUdef/SuperOffice:3,appointmentUdef/SuperOffice:4,appointmentUdef/SuperOffice:5,appointmentUdef/SuperOffice:6,appointmentUdef/SuperOffice:7,appointmentUdef/SuperOffice:8&$top={0}&$skip={1}", pageSize, pageSize * pageNumber);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<SuperOfficeAppointment> results = JsonConvert.DeserializeObject<List<SuperOfficeAppointment>>(apiResponse.Value.ToString());
            return results;
        }

        public async Task<IEnumerable<SuperOfficeSale>> RetrieveSales(int pageSize, int pageNumber)
        {
            var relativeUrl = String.Format("Sale?$select=saleId,contactId,projectId,text,personId,associateId,type,stage,saleStatus,date,source,amount,quote/version/alternative/quoteline/totalCost,earning,SaleText,completed,updatedDate,updatedBy,associate/fullName&$top={0}&$skip={1}", pageSize, pageSize * pageNumber);

            var rslt = await DoWebGetRequest(relativeUrl);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<SuperOfficeSale> results = JsonConvert.DeserializeObject<List<SuperOfficeSale>>(apiResponse.Value.ToString());
            return results;
        }

        public async Task<IEnumerable<SuperOfficeAppointmentDetail>> RetrieveAppointmentDetailsByIds(IEnumerable<int> ids)
        {
            var relativeUrl = "Agents/Appointment/GetAppointmentList?$Select=appointmentId,MotherId";

            var rslt = await DoWebPostRequest(relativeUrl, ids);
            //ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rslt);

            List<SuperOfficeAppointmentDetail> results = JsonConvert.DeserializeObject<List<SuperOfficeAppointmentDetail>>(rslt.ToString());
            return results;

        }

        public async Task<IEnumerable<SuperOfficeProjectMember>> RetrieveProjectMembersByProjectId(int projectId)
        {
            var relativeUrl = "Agents/Project/GetProjectMembers";

            var rslt = await DoWebPostRequest(relativeUrl, new ByProjectId(projectId));

            List<SuperOfficeProjectMember> results = JsonConvert.DeserializeObject<List<SuperOfficeProjectMember>>(rslt.ToString());
            return results;
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
