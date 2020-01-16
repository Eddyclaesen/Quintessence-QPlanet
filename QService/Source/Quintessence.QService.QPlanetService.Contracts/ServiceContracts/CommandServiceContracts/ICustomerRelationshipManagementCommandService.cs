using System;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface ICustomerRelationshipManagementCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewContactDetail(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateContactDetailModel(UpdateContactDetailRequest updateContactDetailRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        int CreateCandidateInfo(CreateCandidateInfoRequest request);
    }
}
