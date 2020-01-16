using System.Collections.Generic;
using Quintessence.QService.QueryModel.Dom;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    /// <summary>
    /// Interface for the Dictionairy Management data context
    /// </summary>
    public interface IDomQueryContext : IQuintessenceQueryContext
    {
        IEnumerable<DocumentView> ListDocumentsByContact(int contactId);
        IEnumerable<DocumentView> ListDocumentsByProject(int projectId);
        string RetrieveDocumentStoreUrl();
        string SharePointServer { get; }
        IEnumerable<TrainingChecklistView> ListTrainingChecklist(int id);
        IEnumerable<BlogEntryView> ListBlogEntries();
        HelpEntryView RetrieveHelpEntry(string title);
        IEnumerable<BlogEntryView> ListStickyBlogEntries();
    }
}
