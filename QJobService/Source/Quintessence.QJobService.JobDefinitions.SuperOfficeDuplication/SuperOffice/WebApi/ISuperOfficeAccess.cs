using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi
{
    public interface ISuperOfficeAccess
    {
        void Initialize(string ticketServiceUri, string ticketServiceApiKey, string superOfficeCustomerStateUri, string superOfficeAppToken);
        Task<bool> TestAccess();

        Task<IEnumerable<SuperOfficeUser>> RetrieveUsers();
        Task<IEnumerable<SuperOfficeProject>> RetrieveProjects(int pageSize, int pageNumber);
        Task<IEnumerable<SuperOfficePerson>> RetrievePersons(int pageSize, int pageNumber);
        Task<IEnumerable<SuperOfficeContact>> RetrieveContacts(int pageSize, int pageNumber);
        Task<IEnumerable<SuperOfficeAppointment>> RetrieveAppointments(int pageSize, int pageNumber);
        Task<IEnumerable<SuperOfficeAppointmentDetail>> RetrieveAppointmentDetailsByIds(IEnumerable<int> ids);
        Task<IEnumerable<SuperOfficeSale>> RetrieveSales(int pageSize, int pageNumber);
        Task<IEnumerable<SuperOfficeProjectMember>> RetrieveProjectMembersByProjectId(int projectId);
    }
}
