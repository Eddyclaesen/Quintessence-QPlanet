using System;
using System.ServiceModel;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class CustomerRelationshipManagementCommandService : SecuredUnityServiceBase, ICustomerRelationshipManagementCommandService
    {
        public Guid CreateNewContactDetail(int contactId)
        {
            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementCommandRepository>();

                var contactDetail = repository.PrepareContactDetail(contactId);
                repository.Save(contactDetail);

                return contactDetail.Id;
            });
        }

        public void UpdateContactDetailModel(UpdateContactDetailRequest updateContactDetailRequest)
        {
            ExecuteTransaction(() =>
                {
                    var repository = Container.Resolve<ICustomerRelationshipManagementCommandRepository>();

                    var contactDetail = repository.RetrieveContactDetail(updateContactDetailRequest.Id);

                    Mapper.DynamicMap(updateContactDetailRequest, contactDetail);

                    repository.Save(contactDetail);
                });
        }

        public int CreateCandidateInfo(CreateCandidateInfoRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementCommandRepository>();

                var candidateInfoId = repository.CreateCandidateInfo(request.FirstName,request.LastName);

                return candidateInfoId;
            });
        }
    }
}
