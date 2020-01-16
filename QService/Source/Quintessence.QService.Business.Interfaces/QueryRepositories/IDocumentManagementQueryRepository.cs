using System.Collections.Generic;
using Quintessence.QService.Core.Repository;
using Quintessence.QService.QueryModel.Dom;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IDocumentManagementQueryRepository : IRepository
    {
        List<DocumentView> ListDocumentsByProject(int projectId);
        string RetrieveDocumentStoreUrl();
        TrainingChecklistView RetrieveTrainingChecklist(int id);
        List<BlogEntryView> ListBlogEntries();
        HelpEntryView RetrieveHelpEntry(string title);
        List<BlogEntryView> ListStickyBlogEntries();
    }
}
