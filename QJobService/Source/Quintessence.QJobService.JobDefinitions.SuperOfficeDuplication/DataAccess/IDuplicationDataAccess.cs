using System;
using System.Collections.Generic;

using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.DataAccess
{
    public interface IDuplicationDataAccess
    {
        // Setting
        string RetrieveDuplicationSettingsByKey(string key, string defaultValue);
        IDictionary<string, string> RetrieveDuplicationSettingsByKeys(IEnumerable<string> keys);

        void RegisterDuplicationError(DateTime logDateUtc, string info);

        // JobHistory
        Guid RegisterDuplicationJobHistory(string JobName, DateTime startDate, DateTime? endDate, bool succeeded, string info);
        Guid RegisterDuplicationJobHistoryStart(string jobName);
        void RegisterDuplicationJobHistoryEnd(Guid duplicationJobHistoryId, bool succeeded, string info);

        void TruncateDuplicationTables();
        int RegisterUsers(IEnumerable<SuperOfficeUser> users);
        int RegisterPersons(IEnumerable<SuperOfficePerson> persons);
        int RegisterContacts(IEnumerable<SuperOfficeContact> contacts);
        int RegisterProjects(IEnumerable<SuperOfficeProject> projects);
        int RegisterAppointments(IEnumerable<SuperOfficeAppointment> appointments);
        int RegisterSales(IEnumerable<SuperOfficeSale> sales);
        int RegisterProjectMembers(IEnumerable<SuperOfficeProjectMember> projectMembers);

        IEnumerable<Appointment> RetrieveAppointmentIds(int fromAppointmentId, int batchSize);
        int EnrichAppointments(IEnumerable<Appointment> appointments, IEnumerable<SuperOfficeAppointmentDetail> superOfficeAppointmentDetails);

        IEnumerable<int> RetrieveProjectIds(int fromProjectId, int batchSize);
    }
}
