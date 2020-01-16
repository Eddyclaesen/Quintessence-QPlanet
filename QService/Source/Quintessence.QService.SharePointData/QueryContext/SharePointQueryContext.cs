using System.Collections.Generic;
using System.Linq;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Dom;
using Quintessence.QService.SharePointData.Base;

namespace Quintessence.QService.SharePointData.QueryContext
{
    public class SharePointQueryContext : ClientContextBase<IDomQueryContext>, IDomQueryContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharePointQueryContext" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public SharePointQueryContext(IConfiguration configuration)
            : base(configuration)
        {
        }

        public IEnumerable<DocumentView> ListDocumentsByProject(int projectId)
        {
            var documents = List<DocumentView>(i => i.CrmProjectId == projectId);

            return documents;
        }

        public string RetrieveDocumentStoreUrl()
        {
            return Url;
        }

        public string SharePointServer
        {
            get { return Url; }
        }

        public IEnumerable<TrainingChecklistView> ListTrainingChecklist(int id)
        {
            var documents = List<TrainingChecklistView>(i => i.Id == id);

            return documents.Where(d => d.Id == id);
        }

        public IEnumerable<BlogEntryView> ListBlogEntries()
        {
            var documents = List<BlogEntryView>();

            return documents.Where(e => !e.IsSticky.GetValueOrDefault());
        }

        public HelpEntryView RetrieveHelpEntry(string title)
        {
            var documents = List<HelpEntryView>(i => i.Title == title);

            return documents.FirstOrDefault(i => i.Title == title);
        }

        public IEnumerable<BlogEntryView> ListStickyBlogEntries()
        {
            var documents = List<BlogEntryView>();

            return documents.Where(e => e.IsSticky.GetValueOrDefault());
        }

        public IEnumerable<DocumentView> ListDocumentsByContact(int contactId)
        {
            var documents = List<DocumentView>(i => i.ContactId == contactId);

            return documents;
        }
    }
}