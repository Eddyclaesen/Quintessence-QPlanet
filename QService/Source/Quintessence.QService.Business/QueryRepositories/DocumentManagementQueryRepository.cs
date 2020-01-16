using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Base;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Dom;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class DocumentManagementQueryRepository : RepositoryBase<IDomQueryContext>, IDocumentManagementQueryRepository
    {
        public DocumentManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<DocumentView> ListDocumentsByProject(int projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = Container.Resolve<IDomQueryContext>())
                    {
                        var documents = context.ListDocumentsByProject(projectId).ToList();

                        documents.ForEach(document => document.Link = string.Format("{0}{1}", context.SharePointServer, document.Link));

                        return documents;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public string RetrieveDocumentStoreUrl()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = Container.Resolve<IDomQueryContext>())
                    {
                        return context.RetrieveDocumentStoreUrl();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public TrainingChecklistView RetrieveTrainingChecklist(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = Container.Resolve<IDomQueryContext>())
                    {
                        var trainingChecklists = context.ListTrainingChecklist(id);

                        var trainingChecklist = trainingChecklists.FirstOrDefault(tc => tc.Id == id);

                        trainingChecklist.EditLink = string.Format("{0}/ITWeb/QPlanet/Lists/TrainingChecklist/EditForm.aspx?Id={1}", context.SharePointServer, trainingChecklist.Id);

                        return trainingChecklist;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<BlogEntryView> ListBlogEntries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = Container.Resolve<IDomQueryContext>())
                    {
                        var blogEntries = context.ListBlogEntries().ToList();

                        return blogEntries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public HelpEntryView RetrieveHelpEntry(string title)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = Container.Resolve<IDomQueryContext>())
                    {
                        var helpEntry = context.RetrieveHelpEntry(title);

                        return helpEntry;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<BlogEntryView> ListStickyBlogEntries()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = Container.Resolve<IDomQueryContext>())
                    {
                        var blogEntries = context.ListStickyBlogEntries().ToList();

                        return blogEntries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
    }
}
