using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface ISupplyChainManagementQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ActivityTypeProfileView> ListActivityTypeProfiles(ListActivityTypeProfilesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ActivityView> ListActivities(ListActivitiesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ActivityTypeView> ListActivityTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ActivityProfileView RetrieveActivityProfile(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProductView> ListProducts(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProductTypeView> ListProductTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ActivityView RetrieveActivity(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ActivityDetailView RetrieveActivityDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<TrainingTypeView> ListTrainingTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ActivityTypeView> ListActivityTypeDetails();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ActivityTypeView RetrieveActivityType(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProfileView> ListProfiles();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ActivityTypeProfileView RetrieveActivityTypeProfile(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProfileView RetrieveProfile(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProductTypeView RetrieveProductType(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProductView RetrieveProduct(Guid id);
    }
}