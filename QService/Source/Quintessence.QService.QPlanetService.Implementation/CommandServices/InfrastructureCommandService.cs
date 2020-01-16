using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Inf;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class InfrastructureCommandService : SecuredUnityServiceBase, IInfrastructureCommandService
    {
        public Guid CreateMailTemplate(CreateMailTemplateRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IInfrastructureManagementCommandRepository>();

                var queryService = Container.Resolve<IInfrastructureQueryService>();

                Guid mailTemplateId;

                //Retrieve MailTemplate by code to check if it already exists 
                //(MailTemplate is retrieved without MailTemplateTranslations)
                var mailTemplate = queryService.RetrieveMailTemplateByCode(request.Code);

                if (mailTemplate == null)
                {
                    var newMailTemplate = repository.Prepare<MailTemplate>();
                    Mapper.DynamicMap(request, newMailTemplate);
                    repository.Save(newMailTemplate);

                    var languages = queryService.ListLanguages();

                    var mailTemplateTranslationsToCreate = new List<MailTemplateTranslation>();
                    foreach (var language in languages)
                    {
                        var mailTemplateTranslation = repository.Prepare<MailTemplateTranslation>();
                        mailTemplateTranslation.Subject = "";
                        mailTemplateTranslation.Body = "";
                        mailTemplateTranslation.LanguageId = language.Id;
                        mailTemplateTranslation.MailTemplateId = newMailTemplate.Id;
                        mailTemplateTranslationsToCreate.Add(mailTemplateTranslation);
                    }

                    foreach (var mailTemplateTranslation in mailTemplateTranslationsToCreate)
                    {
                        repository.Save(mailTemplateTranslation);
                    }

                    mailTemplateId = newMailTemplate.Id;
                }
                else
                {
                    //Retrieve MailTemplate (MailTemplateTranslations included)
                    mailTemplate = queryService.RetrieveMailTemplate(mailTemplate.Id);
                    mailTemplateId = mailTemplate.Id;
                }

                return mailTemplateId;
            });
        }

        public Guid CreateMailTemplateTranslation(CreateMailTemplateTranslationRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IInfrastructureManagementCommandRepository>();

                var mailTemplateTranslation = repository.Prepare<MailTemplateTranslation>();

                Mapper.DynamicMap(request, mailTemplateTranslation);

                repository.Save(mailTemplateTranslation);

                return mailTemplateTranslation.Id;
            });
        }

        public void UpdateMailTemplateTranslation(UpdateMailTemplateTranslationRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IInfrastructureManagementCommandRepository>();

                var mailTemplateTranslation = repository.Retrieve<MailTemplateTranslation>(request.Id);

                Mapper.DynamicMap(request, mailTemplateTranslation);

                repository.Save(mailTemplateTranslation);
            });
        }

        public void UpdateMailTemplate(UpdateMailTemplateRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IInfrastructureManagementCommandRepository>();

                var mailTemplate = repository.Retrieve<MailTemplate>(request.Id);

                Mapper.DynamicMap(request, mailTemplate);

                repository.Save(mailTemplate);

                var mailTemplateTranslationsToUpdate = new List<MailTemplateTranslation>();
                foreach (var mailTemplateTranslationRequest in request.MailTemplateTranslations)
                {
                    var mailTemplateTranslation = repository.Retrieve<MailTemplateTranslation>(mailTemplateTranslationRequest.Id);

                    Mapper.DynamicMap(mailTemplateTranslationRequest, mailTemplateTranslation);

                    mailTemplateTranslationsToUpdate.Add(mailTemplateTranslation);
                }

                foreach (var mailTemplateTranslation in mailTemplateTranslationsToUpdate)
                {
                    repository.Save(mailTemplateTranslation);
                }
            });
        }

        public void ScheduleJob(Guid jobDefinitionId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IInfrastructureManagementCommandRepository>();

                repository.ScheduleJob(jobDefinitionId);
            });
        }
    }
}