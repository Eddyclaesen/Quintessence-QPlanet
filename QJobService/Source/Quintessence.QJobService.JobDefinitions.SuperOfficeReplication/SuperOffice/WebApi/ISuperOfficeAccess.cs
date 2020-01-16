using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi
{
    public interface ISuperOfficeAccess
    {
        void Initialize(string ticketServiceUri, string ticketServiceApiKey, string superOfficeBaseUri, string superOfficeAppToken);
        Task<bool> TestAccess();

        Task<IEnumerable<User>> RetrieveUsers();

        Task<IEnumerable<Company>> RetrieveCompanies(int pageSize, int pageNumber);
        Task<IEnumerable<PersonOverview>> RetrievePersons(int pageSize, int pageNumber);

        Task<IEnumerable<Project>> RetrieveProjects(int pageSize, int pageNumber);
        Task<Project> RetrieveProjectBySuperOfficeId(int superOfficeProjectId);
        Task<IEnumerable<ProjectMember>> RetrieveProjectMembers(int superOfficeProjectId);

        Task<Company> RetrieveCompanyBySuperOfficeId(int superOfficeId);

        Task<Person> RetrievePersonBySuperOfficeId(int superOfficePersonId);

        Task<Appointment> RetrieveAppointmentBySuperOfficeId(int superOfficeAppointmentId);
    }
}
