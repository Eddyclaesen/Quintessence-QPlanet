using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.DataModel.Crm;

namespace Quintessence.CulturalFit.Service.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface ICrmService
    {
        #region Retrieve methods
        /// <summary>
        /// Retrieves the project.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [OperationContract]
        Project RetrieveProject(Guid id);
        #endregion

        [OperationContract]
        Associate RetrieveAssociate(int associateId);

        [OperationContract]
        Associate RetrieveAssociateByUserId(Guid userId);

        [OperationContract]
        Contact RetrieveContact(int contactId);
    }
}