using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface ICustomerRelationshipManagementQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmContactView> ListContacts();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmContactView> SearchContacts(SearchContactRequest searchContactRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmAssociateView> ListProjectManagerAssociates();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmActiveProjectView> ListActiveProjects(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmUnregisteredCandidateAppointmentView> ListCrmUnregisteredCandidateAppointments(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CrmContactView RetrieveContactDetailInformation(int id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ContactDetailView RetrieveContactDetail(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmTimesheetUnregisteredEntryView> ListTimesheetUnregisteredEntries(ListTimesheetUnregisteredEntriesRequest request);
        
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CheckUnregisteredCandidatesForProject(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmAssessorAppointmentView> ListCoAssessors(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CrmAppointmentView RetrieveCrmAppointment(int id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmEmailView> ListCrmEmails();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmEmailView> ListCrmEmailsByContactId(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CrmFormattedAppointmentView RetrieveFormattedCrmAppointment(int crmCandidateAppointmentId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CrmProjectView RetrieveCrmProject(int id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmAppointmentTrainingView> ListProjectTrainingAppointments(Guid projectId);
        
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CrmUnsynchronizedEmployeeView> ListUnsynchronizedEmployees();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CrmEmailView RetrieveCrmEmail(int id);
    }
}
