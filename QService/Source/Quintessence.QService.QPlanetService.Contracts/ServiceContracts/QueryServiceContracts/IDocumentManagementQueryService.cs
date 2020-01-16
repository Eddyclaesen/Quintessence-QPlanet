using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement;
using Quintessence.QService.QueryModel.Dom;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface IDocumentManagementQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListProjectDocumentsResponse ListProjectDocuments(ListProjectDocumentsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        TrainingChecklistView RetrieveTrainingChecklist(int id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<BlogEntryView> ListBlogEntries();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        HelpEntryView RetrieveHelpEntry(string title);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<BlogEntryView> ListStickyBlogEntries();
    }
}
