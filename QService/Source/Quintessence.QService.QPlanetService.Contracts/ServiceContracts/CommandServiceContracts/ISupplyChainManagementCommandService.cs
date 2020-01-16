using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface ISupplyChainManagementCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewActivity(CreateNewActivityRequest createNewActivityRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewActivityProfile(CreateNewActivityProfileRequest createNewActivityProfileRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivity(UpdateActivityRequest updateActivityRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteActivity(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityProfile(List<UpdateActivityProfileRequest> updateActivityProfileRequests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivities(List<UpdateActivityRequest> updateActivityRequests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteActivityProfile(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProduct(CreateNewProductRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProduct(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProducts(List<UpdateProductRequest> updateProductRequests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityDetailTraining(UpdateActivityDetailTrainingRequest updateActivityDetailTrainingRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityDetailCoaching(UpdateActivityDetailCoachingRequest updateActivityDetailCoachingRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityDetailSupport(UpdateActivityDetailSupportRequest updateActivityDetailSupportRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityDetailConsulting(UpdateActivityDetailConsultingRequest updateActivityDetailConsultingRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityDetailWorkshop(UpdateActivityDetailWorkshopRequest updateActivityDetailWorkshopRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateNewActivityDetailTrainingLanguage(CreateNewActivityDetailTrainingLanguageRequest createNewActivityDetailTrainingLanguageRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateNewActivityDetailWorkshopLanguage(CreateNewActivityDetailWorkshopLanguageRequest createNewActivityDetailWorkshopLanguageRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityTypeProfileRatesByIndex(int index);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewActivityType(CreateNewActivityTypeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteActivityType(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityType(UpdateActivtyTypeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateNewActivityTypeProfile(CreateNewActivityTypeProfileRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityTypeProfile(UpdateActivtyTypeProfileRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteActivityTypeProfile(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewProfile(CreateNewProfileRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProfile(UpdateProfileRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProfile(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateProductType(CreateProductTypeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProductType(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProductType(UpdateProductTypeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CopyActivity(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateActivityDetail(UpdateActivityDetailRequest updateActivityDetailRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProduct(UpdateProductRequest updateProductRequest);
    }
}