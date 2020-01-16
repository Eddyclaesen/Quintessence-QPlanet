using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Crm;
using Quintessence.CulturalFit.Infra.Logging;
using Quintessence.CulturalFit.Service.Contracts.ServiceContracts;
using Quintessence.CulturalFit.Service.Implementation.Base;

namespace Quintessence.CulturalFit.Service.Implementation
{
    public class CrmService : UnityServiceBase, ICrmService
    {
        #region Retrieve methods
        
        /// <summary>
        /// Retrieves the project.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        /// <exception cref="System.ServiceModel.FaultException"></exception>
        public Project RetrieveProject(Guid id)
        {
            try
            {
                var repository = Container.Resolve<ICrmRepository>();
                return repository.RetrieveProject(id);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to retrieve project with id {0}.", id);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }

        public Associate RetrieveAssociate(int associateId)
        {
            try
            {
                var repository = Container.Resolve<ICrmRepository>();
                return repository.GetById<Associate>(associateId);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to retrieve associate with id {0}.", associateId);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }

        public Associate RetrieveAssociateByUserId(Guid userId)
        {
            try
            {
                var repository = Container.Resolve<ICrmRepository>();
                return repository.RetrieveAssociateByUserId(userId);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to retrieve associate with user id {0}.", userId);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }

        public Contact RetrieveContact(int contactId)
        {
            try
            {
                var repository = Container.Resolve<ICrmRepository>();
                return repository.GetById<Contact>(contactId);
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to retrieve contact with user id {0}.", contactId);
                LogManager.LogError(message, exception);
                throw new FaultException(message);
            }
        }

        #endregion
    }
}
