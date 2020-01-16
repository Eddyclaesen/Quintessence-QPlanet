using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Dom;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class DocumentManagementQueryService : SecuredUnityServiceBase, IDocumentManagementQueryService
    {
        public override TResult Execute<TResult>(Func<TResult> func)
        {
            var response = base.Execute(func);

            var documentManagementResponseBase = response as DocumentManagementResponseBase;
            if (documentManagementResponseBase != null)
            {
                var repository = Container.Resolve<IDocumentManagementQueryRepository>();
                documentManagementResponseBase.DocumentStoreUrl = repository.RetrieveDocumentStoreUrl();
            }

            return response;
        }

        public ListProjectDocumentsResponse ListProjectDocuments(ListProjectDocumentsRequest request)
        {
            return Execute(() =>
            {
                ////ValidateAuthorization("DIMSEARCH");

                var projectManagementQueryRepository = Container.Resolve<IProjectManagementQueryRepository>();

                var project = projectManagementQueryRepository.RetrieveProjectWithCrmProjects(request.ProjectId);

                var projectDocumentMetadatas = projectManagementQueryRepository.ListProjectDocumentMetadatas(project.Id);

                var repository = Container.Resolve<IDocumentManagementQueryRepository>();

                var response = new ListProjectDocumentsResponse();

                project.CrmProjects
                    .Select(p => p.Id)
                    .Distinct()
                    .ForEach(projectId => response.Documents.AddRange(repository.ListDocumentsByProject(projectId)));
                
                if (projectDocumentMetadatas.Count > 0)
                    foreach (var document in response.Documents)
                        document.IsImportant = projectDocumentMetadatas.Any(pdm => pdm.DocumentUniqueId == document.UniqueId);

                return response;
            });
        }

        public TrainingChecklistView RetrieveTrainingChecklist(int id)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<IDocumentManagementQueryRepository>();

                var trainingChecklist = repository.RetrieveTrainingChecklist(id);

                return trainingChecklist;
            });
        }

        public List<BlogEntryView> ListBlogEntries()
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<IDocumentManagementQueryRepository>();

                var blogEntries = repository.ListBlogEntries();

                return blogEntries;
            });
        }

        public HelpEntryView RetrieveHelpEntry(string title)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<IDocumentManagementQueryRepository>();

                var helpEntry = repository.RetrieveHelpEntry(title);

                return helpEntry;
            });
        }

        public List<BlogEntryView> ListStickyBlogEntries()
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<IDocumentManagementQueryRepository>();

                var blogEntries = repository.ListStickyBlogEntries();

                return blogEntries;
            });
        }
    }
}
